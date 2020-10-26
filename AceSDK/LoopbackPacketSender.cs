using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ace_client.Memory;

namespace Ace_client.AceSDK
{
    public class LoopbackPacketSender : SDKHandler
    {
        public NetworkHandler networkHandler;

        public LoopbackPacketSender(ulong addr) : base(addr)
        {
            networkHandler = new NetworkHandler(AceMCM.Read<ulong>(address + 0x10));
        }
    }
}
