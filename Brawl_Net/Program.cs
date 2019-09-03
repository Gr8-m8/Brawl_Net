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
            string hostIP = "127.0.0.1";
            bool host = true;
            bool lan = true;

            bool programLoop = true;
            while (programLoop)
            {
                Setup(r, NM, GM, lan, hostIP, host);
                Lobby(r, NM, GM, lan, hostIP, host);
                Game (r, NM, GM, lan, hostIP, host);
            }
        }

        //SETUP
        static void Setup(Random r, NetworkManager NM, GameManager GM, bool lan, string hostIP, bool host)
        {
            Console.Clear();
            Console.Title = "SetUp";

            Console.WriteLine("Host: [Q] / Host IP ([Any]) / Offline [W] / Exit: [E]");
            switch (Console.ReadKey().KeyChar.ToString().ToUpper())
            {
                case "Q":
                    GM.players.Add(hostIP);
                    break;

                case "E":
                    System.Environment.Exit(0);
                    break;

                case "W":
                    lan = false;
                    break;

                default:
                    host = false;
                    Console.Write("Host IP: ");
                    hostIP = Console.ReadLine();

                    NM.Send(hostIP, NM.getIP());
                    break;
            }
        }

        //LOBBY
        static void Lobby(Random r, NetworkManager NM, GameManager GM, bool lan, string hostIP, bool host)
        {
            if (!lan)
            {
                Console.Title = "Lobby";
            }
            if (host) { Console.Title += " Host"; }
            bool lobby = true;
            while (lobby)
            {
                Console.Clear();
                Console.WriteLine("Players:");

                if (host)
                {
                    string playerList = "";
                    foreach (string p in GM.players)
                    {
                        foreach (string s in GM.players)
                        {
                            playerList += s + " \n";
                        }

                        if (p != hostIP)
                        {
                            NM.Send(p, playerList);
                        }
                    }
                    Console.WriteLine(playerList);
                    GM.players.Add(NM.Recive());

                    Console.WriteLine("Wait for players. Yes [Any] / No [Q]");
                    switch (Console.ReadKey().KeyChar.ToString().ToUpper())
                    {
                        case "Q":
                            lobby = false;

                            foreach (string p in GM.players)
                            {
                                if (p != hostIP)
                                {
                                    NM.Send(p, "EXITLOBBY");
                                }
                            }

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
        }

        //GAME
        static void Game(Random r, NetworkManager NM, GameManager GM, bool lan, string hostIP, bool host)
        {
            Console.Title = "Game";
            if (host)
            {
                GM.NextTurn(NM, r.Next(0, GM.players.Count - 1));
            }

            bool gameLoop = true;
            while (gameLoop)
            {
                Console.Clear();

                string recive = NM.Recive();
                switch (recive)
                {
                    case "PLAY":
                        GM.Play(NM, hostIP);
                        break;

                    case "NEXTTURN":
                        GM.NextTurn(NM);
                        break;

                    default:
                        Console.WriteLine(recive);
                        break;
                }
            }
        }


    }
}
