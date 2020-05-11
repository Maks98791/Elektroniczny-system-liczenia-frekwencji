using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace USBWebCam.Core
{
    class TCPConnection
    {
        public static string ip, tekst;
        public static int port;

        public TCPConnection()
        { }

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

        public int MainFunction(string new_ip, int new_port, string message)
        { 
            ip = new_ip;
            try
            {
                port = new_port;
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
                    tekst = message;
                    try
                    {
                        client.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
                        move = true;
                    }
                    catch
                    {
                        Console.WriteLine("Brak sluchacza na tym porcie lub na tym adresie IP");
                        return 0;
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
                    return 1;
                }
                catch
                {
                    Console.WriteLine("Polaczenie zostalo zamkniete przez klienta");
                    return 0; 
                }
            }
        }
    }
}

