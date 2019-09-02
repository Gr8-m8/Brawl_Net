using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brawl__Net_
{
    class GameManager
    {
        public List<string> players = new List<string>();
        int playerTurn = 0;

        public void NextTurn(NetworkManager NM)
        {
            playerTurn++;
            if (playerTurn >= players.Count)
            {
                playerTurn = 0;
            }

            NM.Send(players[playerTurn], "PLAY");
        }
    }
}
