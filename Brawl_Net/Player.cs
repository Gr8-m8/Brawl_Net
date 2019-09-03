using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl_Net
{
    class Player
    {
        public string ip = "";
        public Character character = new Character();

        public Player(string setip)
        {
            ip = setip;
        }
    }

    class Character
    {
        string name = "";

        int hpMax = 100;
        int hp = 100;

        int strengh = 10;
        int psyce = 10;
        int endurance = 10;
        int agility = 10;
        int charisma = 10;
        int luck = 10;

        public Character()
        {
            GenerateCharacter();
        }

        public void GenerateCharacter()
        {
            Random r = new Random();

            strengh = r.Next(3, 18);
            psyce = r.Next(3, 18);
            endurance = r.Next(3, 18);
            agility = r.Next(3, 18);
            charisma = r.Next(3, 18);
            luck = r.Next(3, 18);

            hpMax = hp = r.Next(endurance, endurance * 10);
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

        public string WriteStats()
        {
            return "" +
            "Health   : " + hp + "/" + hpMax + "\n" +
            "Strenght : " + strengh + "\n" +
            "Psyce    : " + psyce + "\n" +
            "Endurance: " + endurance + "\n" +
            "Agility  : " + agility + "\n" +
            "Charisma : " + charisma + "\n" +
            "Luck     : " + luck;
        }
    }
}
