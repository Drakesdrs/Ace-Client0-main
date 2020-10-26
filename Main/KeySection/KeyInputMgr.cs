using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;

using Ace_client.Memory;
using System.Threading;
using Ace_client.Main.ImportantSection;
using Ace_client.Main.CategorySection;
using Ace_client.Main.ModuleSection;
using Ace_client.Main.ModuleSection.Modules;
using Ace_client.Main.UI;

using static Ace_client.Main.UI.TabGUI;

namespace Ace_client.Main.KeySection
{
    public class KeyInputMgr
    {
        public static Dictionary<Keys, IKeyInputHandler> keybinds = new Dictionary<Keys, IKeyInputHandler>();

        public static KeysConverter KeysConverter = new KeysConverter();
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        public static Thread keyInputHookThread;

        public static void Init()
        {
            Logger.debug("Starting key capture thread...");
         

            keyInputHookThread = new Thread(() =>
            {
                _hookID = SetHook(_proc);
                Application.Run();
                UnhookWindowsHookEx(_hookID);
            });
            keyInputHookThread.Start();

            Logger.debug("Success!");


            
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        static Keys inPureHookCallback_K;
        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if(wParam == (IntPtr)WM_KEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    inPureHookCallback_K = (Keys) vkCode;

                    if(tabGUI.enabled)
                        switch(inPureHookCallback_K)
                        {
                            case Keys.Up:

                                if (CategoryHandler.registry.isSelectedCategoryActive)
                                    ModuleMgr.registry.selectPreviousModule();
                                else
                                    CategoryHandler.registry.selectPreviousCategory();

                                break;
                            case Keys.Down:

                                if (CategoryHandler.registry.isSelectedCategoryActive)
                                    ModuleMgr.registry.selectNextModule();
                                else
                                    CategoryHandler.registry.selectNextCategory();

                                break;
                            case Keys.Right:
                                if (CategoryHandler.registry.isSelectedCategoryActive)
                                    ModuleMgr.registry.selectedModule.toggle();
                                else
                                {
                                    CategoryHandler.registry.isSelectedCategoryActive = true;
                                    if (CategoryHandler.registry.selectedCategory.modules.Count() > 0)
                                        ModuleMgr.registry.selectedModule = CategoryHandler.registry.selectedCategory.modules[0];
                                    else
                                        CategoryHandler.registry.isSelectedCategoryActive = false;
                                }
                                break;
                            case Keys.Left:
                                ModuleMgr.registry.selectedModule = null;
                                CategoryHandler.registry.isSelectedCategoryActive = false;
                                break;
                    }

                    var cb = keybinds.ContainsKey(inPureHookCallback_K) ? keybinds[inPureHookCallback_K] : null;
                    if (cb != null)
                    {
                        if (cb.key == inPureHookCallback_K)
                        {
                            cb.onKeyDown();
                        }
                        else
                        {
                            Logger.writeLine("An error has occurred with one or more keybinds and their keys have been reset.");
                            cb.key = Keys.None;
                            keybinds[inPureHookCallback_K] = null;
                        }
                    }

                    Program.UI.Invoke((System.Action)(() =>
                    {
                        Program.UI.Refresh();
                    }));
                }
                else if(wParam == (IntPtr)WM_KEYUP)
                {
                    var cb = keybinds.ContainsKey(inPureHookCallback_K) ? keybinds[inPureHookCallback_K] : null;
                    if (cb != null)
                    {
                        if (cb.key == inPureHookCallback_K)
                        {
                            cb.onKeyUp();
                        }
                        else
                        {
                            Logger.writeLine("An error has occurred with one or more keybinds and their keys have been reset.");
                            cb.key = Keys.None;
                            keybinds[inPureHookCallback_K] = null;
                        }
                    }

                    Program.UI.Invoke((System.Action)(() =>
                    {
                        Program.UI.Refresh();
                    }));
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
