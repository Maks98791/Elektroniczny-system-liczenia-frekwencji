using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;


// PROSTY PROGRAM DO WYSYŁANIA PAKIETÓW TCP

namespace WysylaniePakietow
{
    class Program
    {

        public static string ip, tekst;
        public static int port;

        static string ReadData(NetworkStream network)
        {
            string Output = string.Empty;
            byte[] bReads = new byte[2048];
            int ReadAmount = 0;

            while (network.DataAvailable)
            {
                ReadAmount = network.Read(bReads, 0, bReads.Length);

                Output += string.Format("{0}", Encoding.UTF8.GetString(
                        bReads, 0, ReadAmount));
            }
            return Output;
        }

        static void WriteData(NetworkStream stream, string cmd)
        {
            stream.Write(Encoding.UTF8.GetBytes(cmd), 0,
                        Encoding.UTF8.GetBytes(cmd).Length);
        }

        static int Main(string[] args)
        {

            Console.WriteLine("Podaj adres IP i nr portu do wysłania wiadomosci:");
            Console.WriteLine("\nAdres IP (format  xxx.xxx.xxx.xxx np 192.168.0.14):");
            ip = Console.ReadLine();
            Console.WriteLine("\nNumer portu [liczba z przedzialu 1 - 85565:");
            try
            {
                port = Int32.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Niepoprawna wartosc \n");
            }




            while (true)
            {

                TcpClient client = new TcpClient();
                bool move = false;
                while (move == false)
                {
                    Console.WriteLine("\nPodaj tekst wiadomosci:");
                    tekst = Console.ReadLine();
                    try
                    {
                        client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                        move = true;
                    }
                    catch
                    {
                        Console.WriteLine("Brak sluchacza na tym porcie lub na tym adresie IP");
                        Thread.Sleep(2000);

                        Console.WriteLine("Podaj adres IP i nr portu do wysłania wiadomosci:");
                        Console.WriteLine("\nAdres IP (format  xxx.xxx.xxx.xxx np 192.168.0.14):");
                        ip = Console.ReadLine();
                        Console.WriteLine("\nNumer portu [liczba z przedzialu 1 - 85565:");
                        try
                        {
                            port = Int32.Parse(Console.ReadLine());
                        }
                        catch
                        {
                            Console.WriteLine("Niepoprawna wartosc \n");
                        }
                        Console.WriteLine("\nPodaj tekst wiadomosci:");
                        tekst = Console.ReadLine();

                    }

                }


                while (!client.Connected) { } // Wait for connection
                
                try
                {
                    
                    WriteData(client.GetStream(), tekst);
                }
                catch
                {
                    Console.WriteLine("Cant send message. Will try again");
                }

                try
                {
                    client.Close();
                    Console.WriteLine("Polaczenie zostalo zamkniete przez klienta");
                }
                catch
                {
                    Console.WriteLine("Polaczenie zostalo zamkniete przez klienta");
                }
                int a = 0;
                while (a != 1 || a != 2)
                {
                    Console.WriteLine("Czy powtorzyc wyslanie wiadomosci? \n" +
                        "1-tak    2-nie\n");

                    try
                    {
                        a = Int32.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Niepoprawna wartosc \n");
                    }
                    if (a == 1) break;
                    if (a == 2) return 0;
                }
            }
        }
    }
}
