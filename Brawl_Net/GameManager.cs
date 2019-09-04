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
            Console.WriteLine("_NextTurn");
            Console.ReadKey();

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
                foreach (Player p in players)
                {
                    if (p.ip != hostIP)
                    {
                        NM.Send(p.ip, "Player " + players[playerTurn].ip + " Turn.");
                    }
                    else
                    {
                        Console.WriteLine("Player " + players[playerTurn].ip + " Turn.");
                    }

                }

                if (players[playerTurn].ip != hostIP)
                {
                    NM.Send(players[playerTurn].ip, "PLAY");
                }
                else
                {
                    Play(NM);
                }
            }

            if (!lan)
            {
                Play(NM);
            }
        }

        public void Play(NetworkManager NM)
        {
            Console.WriteLine("_Play");
            Console.ReadKey();
            if (lan)
            {
                Console.WriteLine("Your Turn.");
                if (players[playerTurn].ip != hostIP)
                {
                    NM.Send(hostIP, "GETCHARACTER");
                    Console.WriteLine(NM.Recive());
                } else
                {
                    Console.WriteLine(this.players[this.playerTurn].character.WriteStats());
                }
            }
            if (!lan)
            {
                Console.WriteLine("Player " + (playerTurn + 1) + " Turn.");
                Console.WriteLine(players[playerTurn].character.WriteStats());
            }

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
                if (players[playerTurn].ip != hostIP)
                {
                    NM.Send(hostIP, "NEXTTURN");
                } else
                {
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
