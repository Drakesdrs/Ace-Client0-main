using Ace_client.AceSDK;
using Ace_client.Main.ImportantSection;
using Ace_client.Main.KeySection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class BoostHit : TickingModule
    {
        public BoostHit() : base(/*System.Windows.Forms.Keys.F13, */"Combat")
        {

        }

        public override void onKeyDown()
        {
            Logger.debug((int)Minecraft.clientInstance.localPlayer.swingData);
        }

        public override void onTick()
        {
            UInt64 facingEnt = Minecraft.clientInstance.localPlayer.level.lookingEntity.address;
            if(facingEnt > 0 && KeyInputMgr.mouseIsDown)
            {
                Utils.Vec3f directionalVec = Utils.directionalVector((Minecraft.clientInstance.localPlayer.yaw + 90) * (float)Math.PI / 180, (float)Math.PI / 180);
                Minecraft.clientInstance.localPlayer.Velocity.X = 1F * directionalVec.x;
                Minecraft.clientInstance.localPlayer.Velocity.Z = 1F * directionalVec.z;
            }
        }
    }
}
