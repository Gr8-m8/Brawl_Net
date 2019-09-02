using System;

namespace Brawl__Net_
{
    class Program
    {
        static int Main()
        {
            NetworkManager NM = new NetworkManager();
            GameManager GM = new GameManager();
            string hostIP = "127.0.0.1";
            bool host = true;

            Console.WriteLine(NM.getIP());
            //Console.WriteLine(Console.ReadKey().KeyChar.ToString().ToUpper());

            bool programLoop = true;
            while (programLoop)
            {
                Console.Clear();
                Console.Title = "SetUp";
                Console.WriteLine("Host: [Q] / Host IP ([Any]) / Exit: [E]");
                switch (Console.ReadKey().KeyChar.ToString().ToUpper())
                {
                    case "Q":
                        GM.players.Add(hostIP);
                        break;

                    case "E":
                        return 0;
                        break;

                    default:
                        host = false;
                        Console.Write("Host IP: ");
                        hostIP = Console.ReadLine();
                        
                        NM.Send(hostIP, NM.getIP());
                        break;
                }

                Console.Title = "Lobby";
                if (host)
                {
                    Console.Title += " Host";
                }

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

                        Console.WriteLine("Wait for players. [Y]es ([Any -N]) / [N]o");
                        switch (Console.ReadKey().KeyChar.ToString().ToUpper())
                        {
                            case "N":
                                lobby = false;

                                foreach (string p in GM.players)
                                {
                                    if (p != hostIP)
                                    {
                                        NM.Send(p, "ExitLobby");
                                    }
                                }
                                
                                break;

                            case"Y":
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (NM.Recive())
                        {
                            case "ExitLobby":
                                lobby = false;
                                break;

                            default:
                                Console.WriteLine(NM.Recive());
                                break;
                        }
                    }
                }

                Console.Title = "Game";
                bool gameLoop = true;
                while (gameLoop)
                {
                    Console.Clear();

                    GM.NextTurn(NM);

                    switch (NM.Recive())
                    {
                        case "PLAY":
                            break;

                        default:
                            Console.WriteLine("RAYMAN, YEAAAEEAEAAAA");
                            break;
                    }
                }
            }

            Console.Write("-- END --");
            Console.ReadKey();
            return 0;
        }
    }
}
