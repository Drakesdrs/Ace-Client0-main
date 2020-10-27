using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Input;
using System.Drawing.Drawing2D;

using Ace_client.Main.ModuleSection;
using Ace_client.Main.KeySection;
using Ace_client.Main.CategorySection;

using static Ace_client.Main.UI.OverlayMgr;
using System.Globalization;
using Ace_client.Main.ImportantSection;
using Ace_client.Memory;

namespace Ace_client.Main.UI
{
    public class SettingsGUI : VModule
    {
        public static SettingsGUI settingsGUI = new SettingsGUI();

        public SettingsGUI()
            : base(Keys.F12)
        { }

        public bool isKeyChanging;

        public override void draw(LinearGradientBrush gradient, object sender, PaintEventArgs e)
        {
            if (this.enabled) 
            {
                OverlayMgr.FillRoundRectangle(e, selectedbackground, new Rectangle(Program.UI.Width/2 - 200, 0, 400, 200), 0, 20, 20, 0);
                OverlayMgr.FillRoundRectangle(e, background, new Rectangle(Program.UI.Width/2 - 196, 0, 392, 196), 0, 20, 20, 0);
                e.Graphics.DrawString(CategoryHandler.registry.selectedCategory.name, font_small, selectedbackground, Program.UI.Width/2 - 196, 0);

                
                e.Graphics.DrawString("Settings", font, selectedbackground, Program.UI.Width/2 - TextRenderer.MeasureText("Settings", font).Width/2, 15);
                
                e.Graphics.DrawLine(selectedbackground_pen, Program.UI.Width/2 - 200, 32, Program.UI.Width/2 - TextRenderer.MeasureText("Settings", font).Width/2 - 10, 32);
                e.Graphics.DrawLine(selectedbackground_pen, Program.UI.Width/2 + 200, 32, Program.UI.Width/2 + TextRenderer.MeasureText("Settings", font).Width/2 + 10, 32);

                if (ModuleMgr.registry.selectedModule != null)
                {
                    e.Graphics.DrawString(ModuleMgr.registry.selectedModule.name, font_small, selectedbackground, Program.UI.Width / 2 + 196 - TextRenderer.MeasureText(ModuleMgr.registry.selectedModule.name, font_small).Width, 0);

                    e.Graphics.DrawString("Key", font_small, gradient, Program.UI.Width / 2 - 196, 36);
                    OverlayMgr.FillRoundRectangle(e, lightborder, new Rectangle(Program.UI.Width / 2 - 190, 68, 150, 30), 5, 5, 5, 5);
                    OverlayMgr.FillRoundRectangle(e, lightbackground, new Rectangle(Program.UI.Width / 2 - 188, 70, 146, 26), 5, 5, 5, 5); //s

                    var t = isKeyChanging ? "..." : ModuleMgr.registry.selectedModule.key.ToString().Replace("Key", "");
                    if (t.Contains("Oem"))
                        t = "" + (char)AceMCM.MapVirtualKeyA((uint)ModuleMgr.registry.selectedModule.key, 2);
                    if (t.Length == 2 && t.Contains("D"))
                        t = t.Replace("D", "");

                    e.Graphics.DrawString(t, font_small, gradient, Program.UI.Width / 2 - 113 - TextRenderer.MeasureText(t, font_small).Width/2, 68);
                }
            }
        }
    }
}