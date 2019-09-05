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
        Random r = new Random();

        bool dead = false;

        string name = "";

        int hpMax = 100;
        int hp = 100;

        int staminaMax = 100;
        int stamina = 100;

        int strengh = 10;
        int psyce = 10;
        int endurance = 10;
        int agility = 10;
        int charisma = 10;
        int luck = 10;

        public Character()
        {
            GenerateCharacter(50);
        }

        public void GenerateCharacter(int powerLevel = 0)
        {
            

            if (powerLevel <= 0)
            {
                strengh = r.Next(3, 18);
                psyce = r.Next(3, 18);
                endurance = r.Next(3, 18);
                agility = r.Next(3, 18);
                charisma = r.Next(3, 18);
                luck = r.Next(3, 18);

                hpMax = hp = r.Next(endurance, endurance * 10);
                staminaMax = stamina = r.Next(agility, agility * 10);
            } else //66
            {
                int[] tempstats = new int[6] { 3, 3, 3, 3, 3, 3};
                while (powerLevel > 0)
                {
                    tempstats[r.Next(0, 6)] += 1;
                    powerLevel--;
                }

                strengh = tempstats[0];
                psyce = tempstats[1];
                endurance = tempstats[2];
                agility = tempstats[3];
                charisma = tempstats[4];
                luck = tempstats[5];

                hpMax = hp = endurance * 10;
                staminaMax = staminaMax = agility * 10;
            }

            
        }

        public virtual int Attack()
        {
            int crit = 1;
            if (r.Next(100) < luck)
            {
                crit = 2;
            }
            return strengh * crit;
        }

        public virtual int Damage(int damageIn)
        {
            int crit = 1;
            if (r.Next(100) < luck)
            {
                crit = 2;
            }
            int damageTotal = damageIn * 10 / (endurance * crit);
            hp -= damageTotal;

            if (hp <= 0)
            {
                dead = true;
            }

            return damageTotal;
        }

        public virtual void Action()
        {

        }

        public string WriteStats()
        {
            return "" +
            "Health   : " + hp + "/" + hpMax + "\n" +
            "Stamina  : " + stamina + "/" + staminaMax + "\n" +
            "Strenght : " + strengh + "\n" +
            "Psyce    : " + psyce + "\n" +
            "Endurance: " + endurance + "\n" +
            "Agility  : " + agility + "\n" +
            "Charisma : " + charisma + "\n" +
            "Luck     : " + luck;
        }
    }
}
