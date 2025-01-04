CrackTrendnet

I had purchased a TV-IP410PI IP camera off eBay which came with a non-default password.  Additionally, doing a factory reset did not help to get into it.

So I used Wireshark to reverse engineer how the network protocol worked, and wrote a tool which uses a wordlist to attempt to reset the configuration and discover the password.

This requires WinPCap to work, which is accessed via the PCap.Net library.
