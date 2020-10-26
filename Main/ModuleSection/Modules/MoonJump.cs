using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ace_client.AceSDK;
using Ace_client.Memory;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class MoonJump : SparinglyTickingModule
    {
        public MoonJump() : base("Movement")
        {

        }

        public override void onEnable()
        {
            Statics.Fasts.AirJumpOn();
        }

        public override void onDisable()
        {
            Statics.Fasts.AirJumpOff();
        }

        public override void onSpareTick()
        {
            Minecraft.clientInstance.localPlayer.canFly ^= true;
            Minecraft.clientInstance.localPlayer.isFlying ^= true;
        }
    }
}
