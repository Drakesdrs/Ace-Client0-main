using Ace_client.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class NoSwing : Module
    {
        public NoSwing()
            : base("Render")
        { }

        public override void onEnable()
        {
            Statics.Fasts.NoSwingOn();
        }

        public override void onDisable()
        {
            Statics.Fasts.NoSwingOff();
        }
    }
}
