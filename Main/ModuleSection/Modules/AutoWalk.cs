using Ace_client.AceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class AutoWalk : TickingModule
    {
        public AutoWalk() : base("Movement")
        {

        }

        public override void onTick()
        {
            float calcYaw = (Minecraft.clientInstance.localPlayer.yaw + 90F) * ((float)Math.PI / 180F);
            float mult = (Minecraft.clientInstance.localPlayer.onGround ? 0.2F : 0.16F) * (AutoSprint.instance.enabled ? 1.3F : 1.0F);
            Minecraft.clientInstance.localPlayer.Velocity.X = (float)Math.Cos(calcYaw) * mult;
            Minecraft.clientInstance.localPlayer.Velocity.Z = (float)Math.Sin(calcYaw) * mult;
        }
    }

    public class AutoSprint : TickingModule
    {
        public static AutoSprint instance;

        public AutoSprint() : base("Movement")
        {
            instance = this;
        }

        public override void onTick()
        {
            Minecraft.clientInstance.vanillaMoveInputHandler.isHoldingSprint ^= true;
        }

        public override void onDisable()
        {
            Minecraft.clientInstance.vanillaMoveInputHandler.isHoldingSprint ^= false;
        }
    }
}
