using System;

namespace Brawl_Net
{
    class Program
    {

        static void Main()
        {
            Random r = new Random();
            NetworkManager NM = new NetworkManager();
            GameManager GM = new GameManager();
            

            bool programLoop = true;
            while (programLoop)
            {
                Setup(r, NM, GM);
                Lobby(r, NM, GM);
                Game (r, NM, GM);

            }
        }

        //SETUP
        static void Setup(Random r, NetworkManager NM, GameManager GM)
        {
            Console.Clear();
            Console.Title = "SetUp";

            Console.WriteLine("Host: [Q] / Host IP ([Any]) / Offline [W] / Exit: [E]");
            switch (Console.ReadKey().KeyChar.ToString().ToUpper())
            {
                case "Q":
                    GM.players.Add(new Player(GM.hostIP));
                    break;

                case "E":
                    System.Environment.Exit(0);
                    break;

                case "W":
                    GM.lan = false;
                    break;

                default:
                    GM.host = false;
                    Console.Write("Host IP: ");
                    GM.hostIP = Console.ReadLine();

                    NM.Send(GM.hostIP, NM.getIP());
                    break;
            }
        }

        //LOBBY
        static void Lobby(Random r, NetworkManager NM, GameManager GM)
        {
            Console.Title = "Lobby";
            if (GM.host) { Console.Title += " Host"; } else { Console.Title += "Client";  }
            if (!GM.lan) { Console.Title += " Offline"; } else { Console.Title += "Online"; }
            bool lobby = true;
            while (lobby && GM.lan)
            {
                Console.Clear();
                Console.WriteLine("Players:");

                if (GM.host)
                {
                    string playerList = "";
                    foreach (Player p in GM.players)
                    {
                        foreach (Player s in GM.players)
                        {
                            playerList += s.ip + " \n";
                        }

                        if (p.ip != GM.hostIP)
                        {
                            NM.Send(p.ip, playerList);
                        }
                    }
                    Console.WriteLine(playerList);
                    GM.players.Add(new Player(NM.Recive()));

                    Console.WriteLine("Wait for players. Yes [Any] / No [Q]");
                    switch (Console.ReadKey().KeyChar.ToString().ToUpper())
                    {
                        case "Q":
                            foreach (Player p in GM.players)
                            {
                                if (p.ip != GM.hostIP)
                                {
                                    NM.Send(p.ip, "EXITLOBBY");
                                }
                            }

                            lobby = false;
                            break;

                        case "Y":
                        default:
                            break;
                    }
                }
                else
                {
                    switch (NM.Recive())
                    {
                        case "EXITLOBBY":
                            lobby = false;
                            break;

                        default:
                            Console.WriteLine(NM.Recive());
                            break;
                    }
                }
            }

            while(lobby && !GM.lan)
            {
                Console.Clear();
                Console.Write("Players: " + GM.players.Count);
                Console.WriteLine( "\n" + "Add Player [+], Remove Player [-], Game [Any]" + "\n");
                switch (Console.ReadKey().KeyChar.ToString().ToUpper())
                {
                    case "+":
                        GM.players.Add(new Player("127.0.0.1"));
                        break;

                    case "-":
                        GM.players.Remove(GM.players[GM.players.Count - 1]);
                        break;

                    default:
                        lobby = false;
                        break;
                }
            }
        }

        //GAME
        static void Game(Random r, NetworkManager NM, GameManager GM)
        {
            Console.Title = "Game ";
            if (GM.host)
            {
                Console.Title += "Host";
                GM.NextTurn(NM, r.Next(0, GM.players.Count - 1));
            }
            else
            {
                Console.Title += "Client";
            }

            string recive = "";

            bool gameLoop = true;
            while (gameLoop)
            {
                if (GM.lan)
                {
                    switch (NM.Recive())
                    {
                        case "PLAY":
                            GM.Play(NM);
                            break;

                        case "NEXTTURN":
                            GM.NextTurn(NM);
                            break;

                        case "GETCHARACTER":
                            NM.Send(GM.players[GM.playerTurn].ip, GM.players[GM.playerTurn].character.WriteStats());
                            break;

                        default:
                            Console.WriteLine(recive);
                            break;
                    }
                }
            }
        }


    }
}
