using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackSlash
{
    public class ItemBox
    {
        public Item Reward { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }

        public ItemBox(Item reward, int x = 0, int y = 0)
        {
            Reward = reward;
            XCoord = x;
            YCoord = y;
        }
    }
}
