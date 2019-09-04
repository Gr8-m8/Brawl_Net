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
            Console.WriteLine("_NextTurn()");

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
                Console.WriteLine("_NextTurn(): is lan");
                /*
                foreach (Player p in players)
                {
                    if (p.ip != hostIP)
                    {
                        NM.Send(p.ip, "Player " + players[playerTurn].ip + " Turn.");
                    }
                    else if (host)
                    {
                        Console.WriteLine("Player " + players[playerTurn].ip + " Turn.");
                    }

                }
                */

                if (host)
                {
                    Console.WriteLine("_NextTurn(): host");
                    if (players[playerTurn].ip != hostIP)
                    {
                        Console.WriteLine("_NextTurn(): host: p.ip != hostIP");
                        NM.Send(players[playerTurn].ip, "PLAY");
                    }
                    else
                    {
                        Console.WriteLine("_NextTurn(): host: host");
                        Play(NM);
                    }
                }
                if (!host)
                {
                    Console.WriteLine("_NextTurn(): !host");
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
            Console.WriteLine("_Play()");
            if (lan)
            {
                Console.WriteLine("Your Turn.");
                if (!host)
                {
                    Console.WriteLine("_Play(): !host");
                    NM.Send(hostIP, "GETCHARACTER");
                    Console.WriteLine(NM.Recive());
                } else if (host)
                {
                    Console.WriteLine("_Play(): host");
                    Console.WriteLine(this.players[this.playerTurn].character.WriteStats());
                }
            }
            if (!lan)
            {
                Console.WriteLine("Player " + (playerTurn + 1) + " Turn.");
                Console.WriteLine(players[playerTurn].character.WriteStats());
            }

            Console.WriteLine("_Play(): Action");
            Console.WriteLine("Contunue [Any], Attack [Q]");
            switch (Console.ReadKey().KeyChar.ToString().ToUpper())
            {
                case "Q":
                    Console.Clear();
                    if (lan)
                    {

                    }
                    if (!lan)
                    {
                        Console.Write("Target ");
                        int i = 0;
                        foreach (Player p in players)
                        {
                            i++;
                            Console.Write("Player " + i + "[" + i + "] ");
                        }

                        
                    }
                    break;
            }

            if (lan)
            {
                if (!host)
                {
                    Console.WriteLine("_Play() NT: !host");
                    NM.Send(hostIP, "NEXTTURN");
                }
                if (host)
                {
                    Console.WriteLine("_Play() NT: host");
                    NextTurn(NM);
                }
            }
            if (!lan)
            {
                NextTurn(NM);
            }
        }
    }
}
