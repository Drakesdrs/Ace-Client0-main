using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ace_client.Main.ActionSection
{
    public class Actions : List<Action>
    {
        public void run()
        {
            foreach (Action action in this)
            {
                action.run();
            }

        }
    }
}
