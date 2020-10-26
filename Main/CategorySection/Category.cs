using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ace_client.Main.ModuleSection;

namespace Ace_client.Main.CategorySection
{
    public class Category
    {
        public string name { get; private set; }
        public int recordModuleNameWidth = 0;

        public List<Module> modules = new List<Module>();
        public List<TickingModule> tickingModules = new List<TickingModule>();
        public List<SparinglyTickingModule> sparinglyTickingModules = new List<SparinglyTickingModule>();

        public Category(string name)
        {
            this.name = name;
        }

        public void selectNextModule()
        {
            var index = modules.IndexOf(ModuleMgr.registry.selectedModule);
            if (index == modules.Count())
                index = -1;
            ModuleMgr.registry.selectedModule = modules[index + 1];
        }

        public void selectPreviousModule()
        {
            var index = modules.IndexOf(ModuleMgr.registry.selectedModule);
            if (index == 0)
                index = modules.Count() + 1;
            ModuleMgr.registry.selectedModule = modules[index - 1];
        }
    }
}
