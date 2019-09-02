using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl__Net_
{
    class Item
    {
        string name;
        string description;

        public Item(string setName)
        {
            name = setName;
        }
    }

    class Weapon : Item
    {
        public int strenghtmod = 0;
        public int psycmod = 0;
        public int endurancemod = 0;
        public int agilitymod = 0;
        public int charismamod = 0;
        public int luckmod = 0;

        public Weapon(string setName, int s, int p, int e, int a, int c, int l) : base(setName)
        {

        }
    }
}
