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
            Minecraft.clientInstance.localPlayer.Velocity.X = (float)Math.Cos(calcYaw) * 0.2F;
            Minecraft.clientInstance.localPlayer.Velocity.Z = (float)Math.Sin(calcYaw) * 0.2F;

        }
    }
}
