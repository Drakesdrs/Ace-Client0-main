using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ace_client.Main.CategorySection;
using Ace_client.Main.ImportantSection;
using Ace_client.Main.ModuleSection;
using Ace_client.Memory;

using static Ace_client.Main.UI.TabGUI;
using static Ace_client.Main.UI.SettingsGUI;

namespace Ace_client.Main.UI
{
    public class OverlayMgr : Form
    {
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsert, int X, int Y, int cx, int cy, uint uFlags);
        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr voidProcessId);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll")]
        public static extern UInt64 GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        public static extern UInt64 SetWindowLong(IntPtr hWnd, int nIndex, UInt64 dwNewLong);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetForegroundWindow(IntPtr hwnd);
        [DllImport("user32.dll")]
        private static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);
        private struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }
        const uint SW_HIDE = 0;
        const uint SW_SHOWNORMAL = 1;
        const uint SW_NORMAL = 1;
        const uint SW_SHOWMINIMIZED = 2;
        const uint SW_SHOWMAXIMIZED = 3;
        const uint SW_MAXIMIZE = 3;
        const uint SW_SHOWNOACTIVATE = 4;
        const uint SW_SHOW = 5;
        const uint SW_MINIMIZE = 6;
        const uint SW_SHOWMINNOACTIVE = 7;
        const uint SW_SHOWNA = 8;

        public static event EventHandler postOverlayLoad;
        public delegate void fixSizeDel();

        public float rainbowProg = 0f;

        WinEventDelegate overDel;

        IntPtr hWnd;
        public int x = 0;
        public int y = 0;
        public int width = 0;
        public int height = 0;
        public int fullScOff = 0;
        public int[] fadeRGB = new int[3];

        public static SolidBrush background = new SolidBrush(Color.FromArgb(10,10,10));
        public static SolidBrush lightbackground = new SolidBrush(Color.FromArgb(20,20,20));
        public static SolidBrush lightborder = new SolidBrush(Color.FromArgb(40,40,40));
        public static SolidBrush white = new SolidBrush(Color.White);
        public static SolidBrush selectedbackground = new SolidBrush(Color.FromArgb(191, 143, 243));

        public static Pen selectedbackground_pen = new Pen(Color.FromArgb(191, 143, 243), 4);
        
        private System.ComponentModel.IContainer components;
       

        public SolidBrush rainbow
        {
            get
            {
                return new SolidBrush(Rainbow(rainbowProg));
            }
        }

        public static Font font       = new Font("Calibri", 20, FontStyle.Regular);
        public static Font font_small = new Font("Calibri", 17, FontStyle.Regular);
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams parms = base.CreateParams;
                parms.ExStyle |= 0x20;  // Turn on WS_EX_TRANSPARENT;
                return parms;
            }
        }

        public OverlayMgr()
        {
            this.AutoScaleMode = AutoScaleMode.None;
            this.TopMost = true;

            Console.WriteLine("Starting GUIs...");

            this.FormBorderStyle = FormBorderStyle.None;
            this.TransparencyKey = Color.FromArgb(77, 77, 77);
            this.BackColor = this.TransparencyKey;
            this.Location = new Point(0, 0);
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            hWnd = this.Handle;
            overDel = new WinEventDelegate(adjustOverlay);

            SetWinEventHook((uint)SWEH_Events.EVENT_OBJECT_LOCATIONCHANGE, (uint)SWEH_Events.EVENT_OBJECT_LOCATIONCHANGE, IntPtr.Zero, overDel, (uint)AceMCM.mcWindowProcessId, GetWindowThreadProcessId(AceMCM.mcWindowHandle, IntPtr.Zero), (uint)SWEH_dwFlags.WINEVENT_OUTOFCONTEXT | (uint)SWEH_dwFlags.WINEVENT_SKIPOWNPROCESS | (uint)SWEH_dwFlags.WINEVENT_SKIPOWNTHREAD);
            SetWinEventHook((uint)SWEH_Events.EVENT_SYSTEM_FOREGROUND, (uint)SWEH_Events.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, overDel, 0, 0, (uint)SWEH_dwFlags.WINEVENT_OUTOFCONTEXT | (uint)SWEH_dwFlags.WINEVENT_SKIPOWNPROCESS | (uint)SWEH_dwFlags.WINEVENT_SKIPOWNTHREAD);


            UInt64 initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            if (postOverlayLoad != null)
            {
                postOverlayLoad.Invoke(this, new EventArgs());
            }
            Paint += OverlayMgr_Paint;

            var versionNameWidth = TextRenderer.MeasureText(Program.version, font).Width;

            foreach (Category category in CategoryHandler.registry.categories)
            {
                var categoryNameWidth = TextRenderer.MeasureText(category.name, font).Width;
                if (categoryNameWidth > recordCategoryNameWidth)
                    recordCategoryNameWidth = categoryNameWidth;

                foreach (Module mod in category.modules)
                {
                    if (mod.nameWidth > category.recordModuleNameWidth)
                        category.recordModuleNameWidth = mod.nameWidth;
                }
            }

            TabGUI.tabGUI = new TabGUI();
        }
        public static int recordCategoryNameWidth;

        public static void FillRoundRectangle(PaintEventArgs e, Brush p, Rectangle rect, byte radius_0, byte radius_1, byte radius_2, byte radius_3)
        {
            GraphicsPath path = new GraphicsPath();

	        path.AddLine(rect.X + radius_0, rect.Y, rect.X + rect.Width - (radius_0 * 2), rect.Y);
            if(radius_0 > 0)
	            path.AddArc(rect.X + rect.Width - (radius_0 * 2), rect.Y, radius_0 * 2, radius_0 * 2, 270f, 90f);
	        path.AddLine(rect.X + rect.Width, rect.Y + radius_1, rect.X + rect.Width, rect.Y + rect.Height - (radius_1 * 2));
            if(radius_1 > 0)
	            path.AddArc(rect.X + rect.Width - (radius_1 * 2), rect.Y + rect.Height - (radius_1 * 2), radius_1 * 2, radius_1 * 2, 0f, 90f);
	        path.AddLine(rect.X + rect.Width - (radius_2 * 2), rect.Y + rect.Height, rect.X + radius_2, rect.Y + rect.Height);
            if(radius_2 > 0)
	            path.AddArc(rect.X, rect.Y + rect.Height - (radius_2 * 2), radius_2 * 2, radius_2 * 2, 90f, 90f);
	        path.AddLine(rect.X, rect.Y + rect.Height - (radius_3 * 2), rect.X, rect.Y + radius_3);
            if(radius_3 > 0)
	            path.AddArc(rect.X, rect.Y, radius_3 * 2, radius_3 * 2, 180f, 9f);
    
            path.CloseFigure();

            e.Graphics.FillPath(p, path);
        }

        private void OverlayMgr_Paint(object sender, PaintEventArgs e)
        {
            Font = font;

            /* Gradients */
            LinearGradientBrush tabGuiGradient = new LinearGradientBrush(new Rectangle(0, 0, Width, 800), Color.White, Color.Black, LinearGradientMode.Vertical);
            LinearGradientBrush arrayListGradient = new LinearGradientBrush(new Rectangle(1, 1, Width, (ModuleMgr.registry.activeModules.Count() * 60) + 5), (Color.FromArgb(191, 143, 243)), Color.Gray, LinearGradientMode.Vertical);

            tabGUI.draw(tabGuiGradient, sender, e);
            settingsGUI.draw(tabGuiGradient, sender, e);

            #region ArrayList

                for (int i = 0; i < ModuleMgr.registry.activeModules.Count(); i++)
                {
                   
                    e.Graphics.DrawString(ModuleMgr.registry.activeModules[i].name, font,
                    arrayListGradient, width - ModuleMgr.registry.activeModules[i].nameWidth, (i * 30) - 4);
                }

            #endregion
        }

            public void adjustOverlay(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
            {
                WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                GetWindowPlacement(AceMCM.mcWindowHandle, ref placement);
                if (placement.showCmd == SW_MAXIMIZE)
                {
                    fullScOff = 8;
                    TopMost = true;
                    WindowState = FormWindowState.Maximized;
                }
                else
                    fullScOff = 0;
               
                AceMCM.FOURSIDE mcRect = AceMCM.getMcRect();
                x = mcRect.Left + 6;
                y = mcRect.Top + 33 + fullScOff;
                width = mcRect.Right - mcRect.Left - 10;
                height = mcRect.Bottom - mcRect.Top - 40 - fullScOff;
                SetWindowPos(hWnd, AceMCM.isMcFocused1(), x, y, width, height, 0x0040);
            }

            public static Color Rainbow(float progress)
            {
                float div = (Math.Abs(progress % 1) * 6);
                int ascending = (int)((div % 1) * 255);
                int descending = 255 - ascending;

                switch ((int)div)
                {
                    case 0:
                        return Color.FromArgb(255, 255, ascending, 0);
                    case 1:
                        return Color.FromArgb(255, descending, 255, 0);
                    case 2:
                        return Color.FromArgb(255, 0, 255, ascending);
                    case 3:
                        return Color.FromArgb(255, 0, descending, 255);
                    case 4:
                        return Color.FromArgb(255, ascending, 0, 255);
                    default: // case 5:
                        return Color.FromArgb(255, 255, 0, descending);
                }
            }

       

            private void InitializeComponent()
            {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OverlayMgr));
            
            this.SuspendLayout();
            
            
            // 
            // OverlayMgr
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OverlayMgr";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OverlayMgr_Load);
            this.ResumeLayout(false);

            }

        private void OverlayMgr_Load(object sender, EventArgs e)
        {

        }
        
    }

}