using Ace_client.AceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class Tower : TickingModule
    {
        public Tower() : base("Exploits")
        {

        }

        public override void onTick()
        {
            if (Minecraft.clientInstance.localPlayer.heldItemCount > 0 )
            {
                Minecraft.clientInstance.localPlayer.level.lookingBlockSide = 1;

                Minecraft.clientInstance.localPlayer.Velocity.Y = 0.5F;
            }


        }
    }
}
