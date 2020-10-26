using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ace_client.Main.ModuleSection;
using Ace_client.Main.ModuleSection.Modules;
using Ace_client.Main.CategorySection;
using Ace_client.Main.UI;
using Ace_client.Memory;
using Ace_client.Main.ImportantSection;
using Ace_client.Main.KeySection;
using static Ace_client.Memory.AceMCM;
using Ace_client.AceSDK;

namespace Ace_client.Main
{
    public class Program
    {
        public static string version = "Ace v1.9";

        public static int threadSleep = 1;
        public static Thread mainLoop;
        public bool limitCpu = false;
        public static OverlayMgr UI;
        public static TabGUI GUI;
        public static Thread ApplicationUiThread;
        public static bool isFastOrSafe = true;

        public static readonly object lock1 = new object();

        static void Main(string[] args)
        {
            Logger.writeLine("Welcome to Ace Client");
            Logger.writeLine("Loading...");

            Process.Start("minecraft://");

            try
            {
                AceMCM.openMc();
                AceMCM.openWindowH();

                new SaveFile();
                new CategoryHandler();
                new ModuleMgr();

                ApplicationUiThread = new Thread(() => 
                {
                    GUI = new TabGUI();
                    UI = new OverlayMgr();
                    Application.Run(UI);
                });

                ApplicationUiThread.Start();

                KeyInputMgr.Init();

                Logger.writeLine("\nAce has succeeded in starting. You may now use the client!");

                //Minecraft.clientInstance.localPlayer.Velocity.Y += 100;
                //Minecraft.clientInstance.localPlayer.Velocity ^= new Vec3f { x = 10, y = 10, z = 10 };
            }
            catch (Exception ex)
            {
                Logger.log
                (
                    "An error has occured! Please contact a developer.", 
                    "Message: " + ex.Message
                );
            }
            
            Console.ReadLine();
        }
    }
}
