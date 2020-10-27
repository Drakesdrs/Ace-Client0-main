using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Ace_client.Main.CategorySection;
using Ace_client.Main.ImportantSection;
using Ace_client.Main.ModuleSection.Modules;
using Ace_client.Main.UI;
using Ace_client.Memory;
using static Ace_client.Memory.AceMCM;

namespace Ace_client.Main.ModuleSection
{
    public class ModuleMgr
    {
        public static ModuleMgr registry;

        public ModuleMgr()
        {
            registry = this;

            Logger.debug("Module registry begin...");

            /* Combat */
            new BoostHit();
            new Reach();
            //new Killaura();
            //new Hitbox();

            /* Render */
            new NoFire();


            /* Movement */
            //new AirAcceleration();
            new AirJump();
            new Control();
            new AutoSneak();
            new AutoWalk();
            new AutoSprint();
            new BounceFly();
            new BunnyHop();
            new Freeze();
            new Fly();
            new HighJump();
            new NoFall();
            new NoWeb();
            new NoSwing();
            new NoClip();
            new Phase();
            new MoonJump();
            new IceWalk();
            new Jesus();
            new Jetpack();
            new SafeWalk();
            new Speed();

            /* Player */
           
            new Headless();

            /* Exploits */
            new Instabreak();
            //new Tower();





            
          
          
           
           
           
           
          
           
           
            //new PlayerHitbox();
            //new ShowCoords();

            Logger.debug("Success!");

            startModuleThread();
        }

        public void tickModuleThread()
        {
            foreach (Category category in CategoryHandler.registry.categories)
                foreach (TickingModule module in category.tickingModules)
                {
                    if(module.enabled)
                        module.onTick();
                }
        }

        public void spareTickModuleThread()
        {
            foreach (Category category in CategoryHandler.registry.categories)
                foreach (SparinglyTickingModule module in category.sparinglyTickingModules)
                {
                    if (module.enabled)
                        module.onSpareTick();
                }
        }

        public void startModuleThread()
        {
            Logger.debug("Starting module tick thread...");
            Program.mainLoop = new Thread(() =>
            {
                for (; ; )
                {
                    tickModuleThread();
                    Thread.Sleep(10);
                    tickModuleThread();
                    Thread.Sleep(10);
                    tickModuleThread();
                    Thread.Sleep(10);
                    tickModuleThread();
                    Thread.Sleep(10);
                    tickModuleThread();
                    Thread.Sleep(10);
                    tickModuleThread();
                    Thread.Sleep(10);
                    tickModuleThread();
                    Thread.Sleep(10);
                    tickModuleThread();
                    Thread.Sleep(10);
                    tickModuleThread();
                    Thread.Sleep(10);
                    tickModuleThread();
                    spareTickModuleThread();
                    Thread.Sleep(10);
                }
            });
            Program.mainLoop.Start();
            Logger.debug("Success!");
        }

        public void RunAsync(Task task)
        {
            task.ContinueWith(t =>
            {
                Console.WriteLine("Error, {0}", t.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        public Module selectedModule;

        public List<Module> activeModules = new List<Module>();

        public void selectNextModule()
        {
            var index = CategoryHandler.registry.selectedCategory.modules.IndexOf(selectedModule);
            if (index == CategoryHandler.registry.selectedCategory.modules.Count() - 1)
                index = -1;
            selectedModule = CategoryHandler.registry.selectedCategory.modules[index + 1];
        }
        public void selectPreviousModule()
        {
            var index = CategoryHandler.registry.selectedCategory.modules.IndexOf(selectedModule);
            if (index == 0)
                index = CategoryHandler.registry.selectedCategory.modules.Count();
            selectedModule = CategoryHandler.registry.selectedCategory.modules[index - 1];
        }
    }
}
