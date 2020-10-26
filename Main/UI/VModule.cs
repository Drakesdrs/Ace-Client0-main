using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ace_client.Main.ImportantSection;
using Ace_client.Main.KeySection;

namespace Ace_client.Main.UI
{
    public abstract class VModule : IKeyInputHandler
    {
        public string name { get => this.GetType().Name; }

        public bool enabled = true;

        public abstract void draw(LinearGradientBrush gradient, object sender, PaintEventArgs e);

        public VModule(Keys key)
        {
            this.key = key;
            
            if (key != Keys.None)
                KeyInputMgr.keybinds[key] = this;
        }
        
        public void toggle()
        {
            this.enabled = !this.enabled;
            Logger.debug(this.name + " is now " + (this.enabled ? "enabled" : "disabled") + ".");
        }

        public override void onKeyDown()
        {
            this.toggle();
        }

        public override void onKeyUp()
        {

        }
    }
}
