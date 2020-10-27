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
using static Ace_client.Main.UI.SettingsGUI;
using static Ace_client.Memory.AceMCM;

namespace Ace_client.Main.KeySection
{
    public class KeyInputMgr
    {
        public static Dictionary<Keys, IKeyInputHandler> keybinds = new Dictionary<Keys, IKeyInputHandler>();

        public static KeysConverter KeysConverter = new KeysConverter();
        private const int WH_KEYBOARD_LL = 13;
        private const int WH_MOUSE_LL = 14;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONUP = 0x0202;
        private static HOOKPROC _kp = KeyboardHookCallback;
        private static HOOKPROC _mp = MouseHookCallback;
        private static IntPtr _hookID1 = IntPtr.Zero;
        private static IntPtr _hookID2 = IntPtr.Zero;

        public static Thread keyInputHookThread;

        public static void Init()
        {
            Logger.debug("Starting key capture thread...");

            keyInputHookThread = new Thread(() =>
            {
                _hookID1 = SetHook(WH_KEYBOARD_LL, _kp);
                _hookID2 = SetHook(WH_MOUSE_LL, _mp);
                Application.Run();
                UnhookWindowsHookEx(_hookID1);
                UnhookWindowsHookEx(_hookID2);
            });
            keyInputHookThread.Start();

            Logger.debug("Success!");
        }

        private static IntPtr SetHook(int idHook, HOOKPROC proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(idHook, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);

        static Keys inPureHookCallback_K;
        private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    int vkCode = Marshal.ReadInt32(lParam);
                    inPureHookCallback_K = (Keys)vkCode;

                    if (tabGUI.enabled)
                        switch (inPureHookCallback_K)
                        {
                            case Keys.Up:

                                if (CategoryHandler.registry.isSelectedCategoryActive)
                                    ModuleMgr.registry.selectPreviousModule();
                                else
                                    CategoryHandler.registry.selectPreviousCategory();

                                goto quit;
                            case Keys.Down:

                                if (CategoryHandler.registry.isSelectedCategoryActive)
                                    ModuleMgr.registry.selectNextModule();
                                else
                                    CategoryHandler.registry.selectNextCategory();

                                goto quit;
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
                                goto quit;
                            case Keys.Left:
                                ModuleMgr.registry.selectedModule = null;
                                CategoryHandler.registry.isSelectedCategoryActive = false;
                                goto quit;
                        }
                    
                    if(settingsGUI.enabled && inPureHookCallback_K != settingsGUI.key)
                    {
                        if(settingsGUI.isKeyChanging)
                        {
                            if (inPureHookCallback_K == Keys.Escape)
                            {
                                keybinds.Remove(ModuleMgr.registry.selectedModule.key);
                                ModuleMgr.registry.selectedModule.key = Keys.None;
                            }
                            else
                            {
                                if (keybinds.ContainsKey(inPureHookCallback_K))
                                    keybinds[inPureHookCallback_K].key = Keys.None;
                                keybinds.Remove(ModuleMgr.registry.selectedModule.key);
                                ModuleMgr.registry.selectedModule.key = inPureHookCallback_K;
                                keybinds[inPureHookCallback_K] = ModuleMgr.registry.selectedModule;
                            }
                            settingsGUI.isKeyChanging = false;
                        }
                    }
                    else
                    {
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
                                keybinds.Remove(inPureHookCallback_K);
                            }
                        }
                    }

                    Program.UI.Invoke((System.Action)(() =>
                    {
                        Program.UI.Refresh();
                    }));
                }
                else if (wParam == (IntPtr)WM_KEYUP)
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
                            keybinds.Remove(inPureHookCallback_K);
                        }
                    }

                    Program.UI.Invoke((System.Action)(() =>
                    {
                        Program.UI.Refresh();
                    }));
                }
            }

            quit:
            return CallNextHookEx(_hookID1, nCode, wParam, lParam);
        }

        public static bool mouseIsDown;

        public static Keys LeftMouseButtonKey = Keys.F13;
        public static Keys MouseMoveKey = Keys.F15;

        private static IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                var cb = keybinds.ContainsKey(LeftMouseButtonKey) ? keybinds[LeftMouseButtonKey] : null;
                if (cb != null)
                {
                    if (cb.key == LeftMouseButtonKey)
                    {
                        cb.onKeyDown();
                    }
                    else
                    {
                        Logger.writeLine("An error has occurred with one or more keybinds and their keys have been reset.");
                        cb.key = Keys.None;
                        keybinds.Remove(LeftMouseButtonKey);
                    }
                }
                mouseIsDown = true;

                if(settingsGUI.enabled)
                {
                    FOURSIDE thing = AceMCM.getMcRect();
                    if (Cursor.Position.X > thing.Left + Program.UI.Width / 2 - 190 && Cursor.Position.X < thing.Right - Program.UI.Width / 2
                    &&  Cursor.Position.Y > thing.Top  + 100                        && Cursor.Position.Y < thing.Top   + 130)
                    {
                        settingsGUI.isKeyChanging = true;

                        Program.UI.Invoke((System.Action)(() =>
                        {
                            Program.UI.Refresh();
                        }));
                    }
                }
            }
            else if (wParam == (IntPtr)WM_LBUTTONUP)
            {
                var cb = keybinds.ContainsKey(LeftMouseButtonKey) ? keybinds[LeftMouseButtonKey] : null;
                if (cb != null)
                {
                    if (cb.key == LeftMouseButtonKey)
                    {
                        cb.onKeyUp();
                    }
                    else
                    {
                        Logger.writeLine("An error has occurred with one or more keybinds and their keys have been reset.");
                        cb.key = Keys.None;
                        keybinds.Remove(LeftMouseButtonKey);
                    }
                }
                mouseIsDown = false;
            }
            else if (wParam == (IntPtr)WM_MOUSEMOVE)
            {
                var cb = keybinds.ContainsKey(MouseMoveKey) ? keybinds[MouseMoveKey] : null;
                if (cb != null)
                {
                    if (cb.key == MouseMoveKey)
                    {
                        cb.onKeyDown();
                    }
                    else
                    {
                        Logger.writeLine("An error has occurred with one or more keybinds and their keys have been reset.");
                        cb.key = Keys.None;
                        keybinds.Remove(MouseMoveKey);
                    }
                }
                if (mouseIsDown)
                {
                    //Logger.writeLine("gay ass bitchy titty ficky biddy nigger figger");

                    
                }
            }


            return CallNextHookEx(_hookID2, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
