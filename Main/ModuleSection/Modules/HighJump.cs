using Ace_client.AceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class HighJump : TickingModule
    {
        public HighJump() : base("Movement")
        {
            
        }

        public override void onTick()
        {
            bool Jumping = Minecraft.clientInstance.vanillaMoveInputHandler.isJumping;

            if(Jumping)
            {
                if(Minecraft.clientInstance.localPlayer.onGround1 > 0)
                {
                   Minecraft.clientInstance.localPlayer.Velocity.Y = 1F;
                }
                
            }

        }

        public override void onDisable()
        {

        }
    }
}
