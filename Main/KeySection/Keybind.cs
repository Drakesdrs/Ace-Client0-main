
using Ace_client.Main.ActionSection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ace_client.Main.KeySection
{
    public class Keybind : IKeyInputHandler
    {
        public Actions keyDownActions = new Actions();
        public Actions keyUpActions = new Actions();

        public override void onKeyDown()
        {
            keyDownActions.run();
        }

        public override void onKeyUp()
        {
            keyUpActions.run();
        }
    }
}
