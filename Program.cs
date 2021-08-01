using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCSharp
{
    class main
    {
        static void Main(string[] args)
        {
            int mode = 1;

            if (mode == 1)
            {
                DungeonGenerator gd = new DungeonGenerator(15, 15);
                gd.Init();
                gd.Output();
            }
            else if (mode == 2)
            {
                DungeonDrawer dd = new DungeonDrawer(5, 5);
                dd.Draw();
            }
        }
    }
}