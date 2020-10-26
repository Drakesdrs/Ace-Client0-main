using Ace_client.AceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class BounceFly : TickingModule
    {
        public BounceFly() : base("Movement")
        {

        }

        public override void onTick()
        {
            if (Minecraft.clientInstance.localPlayer.Velocity.Y <= -0.3F) Minecraft.clientInstance.localPlayer.Velocity.Y = 0.3F;
            
        }
    }
}
