using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl_Net
{
    class GameManager
    {
        public string hostIP = "127.0.0.1";
        public bool host = true;
        public bool lan = true;

        public List<Player> players = new List<Player>();
        public int playerTurn = -1;

        public void NextTurn(NetworkManager NM, int setPlayerTurn = -1)
        {
            if (setPlayerTurn >= 0 && setPlayerTurn < players.Count)
            {
                playerTurn = setPlayerTurn;
            }
            else
            {
                playerTurn++;
                if (playerTurn >= players.Count)
                {
                    playerTurn = 0;
                }
            }

            if (lan)
            {
                if (host)
                {
                    if (players[playerTurn].ip != hostIP)
                    {
                        NM.Send(players[playerTurn].ip, "PLAY");
                    }
                    else
                    {
                        Play(NM);
                    }
                }
                if (!host)
                {
                    NM.Send(hostIP, "NEXTTURN");
                }
            }

            if (!lan)
            {
                Play(NM);
            }
        }

        public void Play(NetworkManager NM)
        {
            Console.Clear();
            if (lan)
            {
                Console.WriteLine("Your Turn.");
                if (!host)
                {
                    NM.Send(hostIP, "GETCHARACTER");
                    Console.WriteLine(NM.Recive());
                } else if (host)
                {
                    Console.WriteLine(this.players[this.playerTurn].character.WriteStats());
                }
            }
            if (!lan)
            {
                Console.WriteLine("Player " + (playerTurn + 1) + " Turn.");
                Console.WriteLine(players[playerTurn].character.WriteStats());
            }


            Console.WriteLine("\n" + "Attack [Q], Skip [Any]" + "\n");
            switch (Console.ReadKey().KeyChar.ToString().ToUpper())
            {
                case "Q":
                    PlayerAttack();
                    break;

                default:
                    break;
            }

            if (lan)
            {
                if (!host)
                {
                    NM.Send(hostIP, "NEXTTURN");
                }
                if (host)
                {
                    NextTurn(NM);
                }
            }
            if (!lan)
            {
                NextTurn(NM);
            }

            Console.WriteLine("\n" + "--END-OF-TURN--" + "\n");
        }

        void PlayerAttack()
        {
            if (lan)
            {

            }
            if (!lan)
            {
                Console.Write("\n" + "Target(s) " + "\n");
                int i = 0;
                foreach (Player p in players)
                {
                    i++;
                    Console.Write("Player " + i + " [" + i + "] ");

                    if (p != players[players.Count - 1])
                    {
                        Console.Write(" : ");
                    }
                }

                string target = Console.ReadLine().ToString().ToUpper();
                if (int.TryParse(target, out int n))
                {
                    if (0 < n && n <= players.Count)
                    {
                        int damage = players[playerTurn].character.Attack();
                        Console.WriteLine("Dealt " + damage + " Damage reduced to " + players[n - 1].character.Damage(damage) + " to Player " + n);
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
