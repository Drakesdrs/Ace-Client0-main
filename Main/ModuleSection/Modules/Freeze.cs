using Ace_client.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ModuleSection.Modules
{
    public class Freeze : Module
    {
        public Freeze() : base("Movement")
        {

        }

        public override void onEnable()
        {
            base.onEnable();
            Statics.Fasts.ConstantWebOn();
        }

        public override void onDisable()
        {
            base.onDisable();
            Statics.Fasts.ConstantWebOff();
        }
    }
}
