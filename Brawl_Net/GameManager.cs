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
                foreach (Player p in players)
                {
                    NM.Send(p.ip, "Player " + players[playerTurn].ip + " Turn.");

                }
                NM.Send(players[playerTurn].ip, "PLAY");
            }

            
        }

        public void Play(NetworkManager NM)
        {
            if (lan)
            {
                Console.WriteLine("Your Turn.");
                NM.Send(hostIP, "GETCHARACTER");
            }

            if (!lan)
            {
                Console.WriteLine("Player " + (playerTurn + 1) + " Turn.");
                Console.WriteLine(players[playerTurn].character.WriteStats());
            }

            Console.WriteLine("Contunue [Any]");
            Console.ReadKey();

            if (lan)
            {
                NM.Send(hostIP, "NEXTTURN");
            }

            if (!lan)
            {
                NextTurn(NM);
            }
        }
    }
}
