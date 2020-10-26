using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ace_client.Main.KeySection
{
    public abstract class IKeyInputHandler
    {
        public abstract void onKeyDown();
        public abstract void onKeyUp();
        public Keys key;
    }
}
