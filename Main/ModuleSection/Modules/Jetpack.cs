using Ace_client.AceSDK;
using Ace_client.Main.ActionSection;
using Ace_client.Main.KeySection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class Jetpack : TickingModule
    {
        public override void registerKeybind()
        {
            this.keybind = new KeyHeldAction(this);
            this.keybind.key = Keys.F;
            KeyInputMgr.keybinds[key] = this.keybind;
        }

        public Jetpack() : base(Keys.F, "Movement")
        {
            
        }

        public override void onTick()
        {
            Utils.Vec3f directionalVec = Minecraft.clientInstance.localPlayer.lookingVec;
            Minecraft.clientInstance.localPlayer.Velocity.X = 2.0f * directionalVec.x;
            Minecraft.clientInstance.localPlayer.Velocity.Y = 2.0f * -directionalVec.y;
            Minecraft.clientInstance.localPlayer.Velocity.Z = 2.0f * directionalVec.z;
        }
    }
}