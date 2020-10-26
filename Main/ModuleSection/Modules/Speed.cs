using Ace_client.AceSDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class Speed : Module
    {
        float backupSpeed;
        public float multiplier = 1.0f;

        public unsafe Speed() : base("Movement")
        {
            fixed (float* m = &multiplier)
            {
                RegisterFloatSliderSetting("Speed", 0.2F, m, 5F);
            }
        }

        public override void onEnable()
        {
           //savedSpeed = Minecraft.clientInstance.
        }

        public override void onDisable()
        {
            //Minecraft.clientInstance.localPlayer.
        }
    }
}
