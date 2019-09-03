using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl_Net
{
    class GameManager
    {
        public List<string> players = new List<string>();
        int playerTurn = -1;

        public void NextTurn(NetworkManager NM, int setPlayerTurn = -1)
        {
            if (setPlayerTurn > 0 && setPlayerTurn < players.Count)
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

            foreach (string p in players)
            {
                if (p != players[playerTurn])
                {
                    NM.Send(p, "Player " + playerTurn + " Turn.");
                } else
                {
                    NM.Send(players[playerTurn], "PLAY");
                }
            }
        }

        public void Play(NetworkManager NM, string hostIP)
        {
            Console.WriteLine("Your Turn.");
            Console.WriteLine("Contunue [Any]");
            Console.ReadKey();
            NM.Send(hostIP, "NEXTTURN");
        }
    }
}
