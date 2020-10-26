using Ace_client.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.AceSDK
{
    public class Game : SDKHandler
    {
        public GameGraphics gameGraphics;

        public Game(ulong address) : base(address)
        {
            gameGraphics = new GameGraphics(AceMCM.Read<ulong>(address + 0x88));
        }
    }
}
