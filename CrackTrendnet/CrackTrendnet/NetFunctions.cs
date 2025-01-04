using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Packets.Dns;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets;
using PcapDotNet.Core;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CrackTrendnet
{
    public partial class RecoverPass : Form
    {
        private bool bReceivedPacket = false;
        private bool bUpdateSuccess = false;

        List<string> localIPs =  new List<string>();
        /// <summary>
        /// This function build a DNS over UDP over IPv4 over Ethernet packet.
        /// </summary>
        public Packet BuildBroadcastPacket(string DestIP, ushort DestPort, string payload, string SourceMac = "", string SourceIP = "", string DestMac = "01:00:5e:7f:ff:fa")
        {
            //if SourceMac == string.empty, grab the MAC from the first active network adapter
            //if sourceIp == string.empty, grab the first IP of the first active network adapter

            EthernetLayer ethernetLayer =
                new EthernetLayer
                {
                    Source = new MacAddress(SourceMac),
                    Destination = new MacAddress(DestMac),
                    EtherType = EthernetType.None, // Will be filled automatically.
                };

            IpV4Layer ipV4Layer =
                new IpV4Layer
                {
                    Source = new IpV4Address(SourceIP),
                    CurrentDestination = new IpV4Address(DestIP),
                    Fragmentation = IpV4Fragmentation.None,
                    HeaderChecksum = null, // Will be filled automatically.
                    Identification = 123,
                    Options = IpV4Options.None,
                    Protocol = null, // Will be filled automatically.
                    Ttl = 100,
                    TypeOfService = 0,
                };

            UdpLayer udpLayer =
                new UdpLayer
                {
                    SourcePort = DestPort, //use same, why not?  Trendnet tool does it too.
                    DestinationPort = DestPort,
                    Checksum = null, // Will be filled automatically.
                    CalculateChecksumValue = true,
                };
 
            PayloadLayer payloadLayer =
                new PayloadLayer
                {
                    Data = new Datagram(Encoding.UTF8.GetBytes(payload) )
                };

            PacketBuilder builder = new PacketBuilder(ethernetLayer, ipV4Layer, udpLayer, payloadLayer);

            return builder.Build(DateTime.Now);
        }

        public bool testUDPPacket(Packet packet, string uuid)
        {
            string xml = sendUDPPacket(packet, uuid);
            XDocument xmlDoc = XDocument.Parse(xml);
            XElement resultElem = xmlDoc.XPathSelectElement("/ProbeMatch/Result");
            if (xml.Length > 0) { return true; }
            else { return false; }
        }

        public bool testPassword(Packet packet, string uuid)
        {
            string xml = sendUDPPacket(packet, uuid);
            XDocument xmlDoc = XDocument.Parse(xml);
            XElement resultElem = xmlDoc.XPathSelectElement("/ProbeMatch/Result");
            if (resultElem.Value == "failed")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string sendUDPPacket(Packet packet, string uuid)
        {
            string xml = string.Empty;
            communicator.NonBlocking = true;
            Packet receivedPacket;
            BerkeleyPacketFilter filter = communicator.CreateFilter(string.Format("udp port {1} and dst host {0}", broadcastIP, portNum));
            communicator.SetFilter(filter);

            Thread WaitOnResponse = new Thread(() =>
            {
                bool done = false;
                do
                {
                    PacketCommunicatorReceiveResult result = communicator.ReceivePacket(out receivedPacket);

                    switch (result)
                    {
                        case PacketCommunicatorReceiveResult.Timeout:
                            // Timeout elapsed
                            continue;
                        case PacketCommunicatorReceiveResult.Ok:

                            if(receivedPacket.Ethernet.IpV4.Protocol == IpV4Protocol.Udp && !localIPs.Contains(receivedPacket.Ethernet.IpV4.Source.ToString()))
                            {
                                if(receivedPacket.Ethernet.IpV4.Udp.DestinationPort == portNum && receivedPacket.Ethernet.IpV4.Destination.ToString() == broadcastIP)
                                {
                                    foreach(byte b in receivedPacket.Ethernet.IpV4.Udp.Payload.ToList<byte>())
                                    {
                                        xml += (char)b;
                                    }
                                    
                                    if(xml.Contains(uuid))
                                    {
                                        done = true;
                                        bReceivedPacket = true;
                                    }
                                    else
                                    {
                                        xml = string.Empty;
                                    }
                                }
                            }
                                
                            break;
                        default:
                            throw new InvalidOperationException("The result " + result + " should never be reached here");
                    }
                } while (done == false);
            });

            WaitOnResponse.Start();

            communicator.SendPacket(packet);  // This one needs a thread and a separate communicator

            while(bReceivedPacket == false || WaitOnResponse.ThreadState == System.Threading.ThreadState.Running)
            {
                Thread.Sleep(10); // We should have response within 20-30ms.
            }
            return xml;
        }

    }
}
