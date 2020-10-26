using Ace_client.AceSDK;
using Ace_client.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class NoFall : TickingModule
    {
        public NoFall() : base("Movement")
        {

        }

        public override void onTick()
        {
            Minecraft.clientInstance.localPlayer.fallDistance ^= 0;
        }
    }
}
