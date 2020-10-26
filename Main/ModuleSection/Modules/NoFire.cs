using Ace_client.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class NoFire : Module
    {
        public NoFire() : base("Render")
        {

        }

        public override void onEnable()
        {
            base.onEnable();
            Statics.Fasts.NoFireOn();
        }

        public override void onDisable()
        {
            base.onDisable();
            Statics.Fasts.NoFireOff();
        }
    }
}
