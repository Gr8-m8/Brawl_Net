using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl__Net_
{
    class Player
    {
        string name = "";

        int hp = 100;

        int strengh = 1;
        int psyce = 1;
        int endurance = 1;
        int agility = 1;
        int charisma = 1;
        int luck = 1;

        public Player()
        {

        }

        public void GeneratePlayer()
        {
            Random r = new Random();

            strengh = r.Next(1, 10);
            psyce = r.Next(1, 10);
            endurance = r.Next(1, 10);
            agility = r.Next(1, 10);
            charisma = r.Next(1, 10);
            luck = r.Next(1, 10);

            hp = r.Next(endurance, endurance*10);
        }

        public virtual int Attack()
        {
            return 0;
        }

        public virtual bool Dodge()
        {
            return false;
        }

        public virtual void Action()
        {

        }
    }
}
