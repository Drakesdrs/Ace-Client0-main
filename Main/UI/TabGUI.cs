using Ace_client.Main.CategorySection;
using Ace_client.Main.ImportantSection;
using Ace_client.Main.ModuleSection;
using Ace_client.Memory;
using Ace_client.Main.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

using static Ace_client.Main.UI.OverlayMgr;

namespace Ace_client.Main.UI
{
    public class TabGUI : VModule
    {
        public static TabGUI tabGUI;

        public TabGUI()
            : base(Keys.Insert)
        { }

        public override void draw(LinearGradientBrush gradient, object sender, PaintEventArgs e)
        {
            if(this.enabled)
            {
                OverlayMgr.FillRoundRectangle(e, background, new Rectangle(0, 0, recordCategoryNameWidth + 4, CategoryHandler.registry.categories.Count() * 36 + 4), 0, 20, 0, 0);

                for (int i = 0; i < CategoryHandler.registry.categories.Count(); i++)
                {
                    if (CategoryHandler.registry.categories[i] == CategoryHandler.registry.selectedCategory)
                    {
                        if(i < CategoryHandler.registry.categories.Count() - 1)
                            e.Graphics.FillRectangle(selectedbackground, 0, (i * 30) + 30, recordCategoryNameWidth + 4, 34);
                        else
                            OverlayMgr.FillRoundRectangle(e, selectedbackground, new Rectangle(0, (i * 30) + 30, recordCategoryNameWidth + 4, 34), 0, 20, 0, 0);

                        e.Graphics.DrawString("━ Ace ━", font, selectedbackground, 5, 0);
                        if (CategoryHandler.registry.isSelectedCategoryActive)
                           for (int j = 0; j < CategoryHandler.registry.categories[i].modules.Count(); j++)
                                CategoryHandler.registry.categories[i].modules[j].draw(recordCategoryNameWidth + 4, ((i + j) * 30) + 30, CategoryHandler.registry.categories[i].recordModuleNameWidth, e.Graphics);
                    }

                    e.Graphics.DrawString(CategoryHandler.registry.categories[i].name, font, gradient, 2, (i + 1) * 30);
                } 
                
                OverlayMgr.FillRoundRectangle(e, background, new Rectangle(0,  CategoryHandler.registry.categories.Count() * 36 + 10, recordCategoryNameWidth + 4, 34), 20, 20, 0, 0);
                e.Graphics.DrawString("FPS: 144", font, selectedbackground, 5, CategoryHandler.registry.categories.Count() * 36 + 10);
            }
        }
    }
}
