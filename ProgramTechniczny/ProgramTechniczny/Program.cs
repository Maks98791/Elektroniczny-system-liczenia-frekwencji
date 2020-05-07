using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks; 
using System.IO;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace ProgramTechniczny
{
    class Program
    {
        static string date;
        static byte[] myReadBuffer;

        //Zwraca aktualną godzinę
        static string ActualData()
        {
            date = DateTime.Now.ToString("HH:mm:ss");
            return date;
        }

        static string message = "";

        static string ReadData(NetworkStream network)
        {
            
                StringBuilder myCompleteMessage = new StringBuilder();
            if (network.CanRead)
            {
                myReadBuffer = new byte[1024];
                int numberOfBytesRead = 0;

                // Incoming message may be larger than the buffer size.
                do
                {
                    numberOfBytesRead = network.Read(myReadBuffer, 0, myReadBuffer.Length);
                    myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                }
                while (network.DataAvailable);

                // Print out the received message to the console.
                //Console.WriteLine("You received the following message : " + myCompleteMessage);
                message += myCompleteMessage;
            }
            else
            {
                Console.WriteLine("Sorry.  You cannot read from this NetworkStream.");
            }
            return message;
        }

        static void WriteData(NetworkStream stream, string cmd)
        {
            stream.Write(Encoding.UTF8.GetBytes(cmd), 0,
            Encoding.UTF8.GetBytes(cmd).Length);
        }


        static public string getLocalIpAddres()
        {
            string localIpAddress = string.Empty;
            string hostName = Dns.GetHostName();

            IPHostEntry ipEntry = Dns.GetHostEntry(hostName);
            IPAddress[] ipaddress = ipEntry.AddressList;

            if(ipaddress.Length >= 2)
            return ipaddress[1].ToString();
            else return ipaddress[0].ToString();
        }

        static void textConvert(string message)
        {

            string[] pobrane = message.Split(';'); //pobrane[0] -> ile osob weszlo
                                                   //pobran[1] -> ile osob wyszlo
        }

        /*public Image byteArrayToImage(byte[] bytesArr)
        {
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }*/

        static void zapisDoBazy(string message, int whereToSave)
        {
            //whereToSave - zmienna okreslajaca czy zapisac zdjecie, czy zapisac ilosc osob (1 - zdjecie, 2 - ilosc osob)

            string command = "";
            int idKamery = message[0] - '0'; //pobranie id kamery z 1 znaku ciagu
            message = message.Substring(1);
            


            if (whereToSave == 1) //zdjecie
            {
               /* using (MemoryStream mStream = new MemoryStream(byteArrayIn))
                {
                    return Image.FromStream(mStream);
                    
                }
                */
                command = "exec insert into costam values (" + idKamery + "," + message + "," + ActualData() + ");";
            }
            else if (whereToSave == 2) //ilosc osob
            {
                string[] listaOsob = message.Split(';'); // no 0 ile osob weszlo na 1 ile osob wyszlo
                command = "exec insert into costam values (" + idKamery + "," + listaOsob[0] + ", " + listaOsob[1] + "," + ActualData() + ");";
            }

            SqlConnection con;
            SqlCommand cmd;

            string connectString = "";

                //connectString = "Data Source= " + ipAddress + ",1433; Network Library=DBMSSOCN; Initial Catalog =" + '"' + "BD2 komisy samochodowe" + '"' + "; User ID = " + login + "; Password=" + password + ";";

                con = new SqlConnection(connectString);

            try
            {
                if (con.State == ConnectionState.Open) con.Close();

                cmd = new SqlCommand(command, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


                ActualData();
            }
            catch (Exception)
            {
                Console.WriteLine("Nie udalo sie poprawnie wprowadzic danych do bazy danych z operacji nr " + whereToSave + ":   [1 - zdjecie, 2 - liczba gosci])");
            }
        }

        static void GuestListener()
        {
            int port = 8080;
            Console.WriteLine("Weryfikacja gosci: Rozpoczynam nasluch na ip: " + getLocalIpAddres() + "  na porcie: " + port);

            IPAddress ipaddress = IPAddress.Parse(getLocalIpAddres());

            TcpListener listener = new TcpListener(new IPEndPoint(ipaddress, port));
            //listener.ExclusiveAddressUse = true; // One client only?
            listener.Start();

            Func<TcpClient, bool> SendMessage = (TcpClient client) => {

                WriteData(client.GetStream(), "Responeded to client");
                return true;
            };

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("ClientConnect");

                NetworkStream network = client.GetStream();
                if (ReadData(network) != string.Empty)
                {
                    try
                    {
                        Console.WriteLine("Wiadomosc z portu: " + port + "    Tekst wiadomosci: " + message);
                        //konwersja i zapis do bazy
                        message = ""; //reset wiadomości
                    }
                    catch
                    {
                        Console.WriteLine("Nie mozna juz tu nic wyswietlic");
                    }
                }
                else Console.WriteLine("Brak wiadomosci do odczytania");

                client.Close();
            }
        }



        static void PictureListener()
        {
            int port = 8081;
            Console.WriteLine("Zapis zdjec z kamery: Rozpoczynam nasluch na ip: " + getLocalIpAddres() + "  na porcie: " + port);

            IPAddress ipaddress = IPAddress.Parse(getLocalIpAddres());

            TcpListener listener = new TcpListener(new IPEndPoint(ipaddress, port));
            //listener.ExclusiveAddressUse = true; // One client only?
            listener.Start();

            Func<TcpClient, bool> SendMessage = (TcpClient client) => {

                WriteData(client.GetStream(), "Responeded to client");
                return true;
            };

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("ClientConnect");

                NetworkStream network = client.GetStream();
                if (ReadData(network) != string.Empty)
                {
                    try
                    {
                        Console.WriteLine("Wiadomosc z portu: " + port + "    Tekst wiadomosci: " + message);
                        //konwersja i zapis do bazy
                        message = ""; //reset wiadomości
                    }
                    catch
                    {
                        Console.WriteLine("Nie mozna juz tu nic wyswietlic");
                    }
                }
                else Console.WriteLine("Brak wiadomosci do odczytania");

                client.Close();
            }
        }

        [Obsolete]
        static void Main(string[] args)
        {
           
            Thread countGuest = new Thread(GuestListener);
            Thread getPicture = new Thread(PictureListener);

            countGuest.Start();
            getPicture.Start();


        }
    }
}
