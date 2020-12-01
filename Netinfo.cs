using System;
using System.Net;
using System.Net.Sockets;

using DotNetstat.Enums;
using DotNetstat.Helpers;

namespace DotNetstat
{


    public class NetInfo
    {
        public ProtocolType ProtocolType;
        public IPEndPoint LocalAddress;
        public IPEndPoint ExternalAddress;
        public SocketStatus SocketStatus;
        public int PID;

        ///TCP 0.0.0.0:49670 0.0.0.0:0 LISTENING 896
        public NetInfo(string netstatRow)
        {
            string[] netinfos = netstatRow.Split(' ');

            this.ProtocolType = netinfos[1] == "TCP" ? ProtocolType.Tcp : ProtocolType.Udp;
            this.LocalAddress = IPEndPointHelper.Parse(netinfos[2]);
            this.ExternalAddress = this.ProtocolType == ProtocolType.Tcp ? IPEndPointHelper.Parse(netinfos[3]) : new IPEndPoint(IPAddress.Any, 0);

            if (this.ProtocolType == ProtocolType.Tcp)
            {

                switch (netinfos[4])
                {
                    case "ESTABLISHED":
                        {
                            this.SocketStatus = SocketStatus.ESTABLISHED;
                            break;
                        }
                    case "SYN_SENT":
                        {
                            this.SocketStatus = SocketStatus.SYN_SENT;
                            break;
                        }
                    case "SYN_RECV":
                        {
                            this.SocketStatus = SocketStatus.SYN_RECV;
                            break;
                        }
                    case "LISTENING":
                        {
                            this.SocketStatus = SocketStatus.LISTEN;
                            break;
                        }
                    case "TIME_WAIT":
                        {
                            this.SocketStatus = SocketStatus.TIME_WAIT;
                            break;
                        }
                    case "FIN_WAIT1":
                        {
                            this.SocketStatus = SocketStatus.FIN_WAIT1;
                            break;
                        }
                    case "FIN_WAIT2":
                        {
                            this.SocketStatus = SocketStatus.FIN_WAIT2;
                            break;
                        }
                    case "CLOSE":
                        {
                            this.SocketStatus = SocketStatus.CLOSE;
                            break;
                        }
                    case "CLOSE_WAIT":
                        {
                            this.SocketStatus = SocketStatus.CLOSE_WAIT;
                            break;
                        }
                    case "LAST_ACK":
                        {
                            this.SocketStatus = SocketStatus.LAST_ACK;
                            break;
                        }
                    case "CLOSING":
                        {
                            this.SocketStatus = SocketStatus.CLOSING;
                            break;
                        }
                    default:
                        {
                            this.SocketStatus = SocketStatus.UNKNOWN;
                            break;
                        }
                }
                this.PID = Convert.ToInt32(netinfos[5]);
            }
            else
            {
                this.PID = Convert.ToInt32(netinfos[4]);
            }
        }
    }
}
