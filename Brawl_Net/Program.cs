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

                            case"Y":
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

                Console.Title = "Game";
                bool gameLoop = true;
                while (gameLoop)
                {
                    Console.Clear();

                    string recive = NM.Recive();
                    switch (recive)
                    {
                        case "PLAY":
                            Console.WriteLine("Your Turn");
                            Console.ReadKey();
                            NM.Send(hostIP, "NEXTTURN");
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

            Console.Write("-- END --");
            Console.ReadKey();
            return 0;
        }
    }
}
