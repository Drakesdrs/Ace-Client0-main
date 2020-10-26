using Ace_client.Main.ModuleSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ActionSection
{
    public class EnableModuleAction : Action
    {
        public Module module;

        public void run()
        {
            module.enabled = true;
        }

        public EnableModuleAction(Module module)
        {
            this.module = module;
        }
    }
}
