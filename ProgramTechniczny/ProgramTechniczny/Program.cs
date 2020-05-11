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
//using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using Convert = System.Convert;
using MemoryStream = System.IO.MemoryStream;
using Image = System.Drawing.Image;

namespace ProgramTechniczny
{
    class Program
    {
        static string date;
        static byte[] myReadBuffer;
        static string message = "";
        static int globalneIdKamery;

        //Zwraca aktualną godzinę
        static string ActualData()
        {
            date = DateTime.Now.ToString("yyyy-MM-d HH:mm:ss");
            return date;
        }

       
        //zwraca odebrana wiadomosc w formacie stringa
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

        public static byte[] ReadFully(NetworkStream input)
        {
            byte[] buffer = new byte[16 * 1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                byte[] temp = ms.ToArray();

                //if (BitConverter.IsLittleEndian)
                //    Array.Reverse(temp);

                //int lengh = temp.Length - 4 ;
                //int i = BitConverter.ToInt32(temp, lengh);
                //int x = BitConverter.ToInt32(temp, 0);
                //Console.WriteLine("int {0}", i); // odczyt indeksu kamery
                //Console.WriteLine("int {0}", x); // odczyt indeksu kamery
                //Console.WriteLine(temp[lengh] + " " + temp[lengh + 1] + " " + temp[lengh + 2] + " " + temp[lengh + 3]);
                //Console.WriteLine(temp[0] + " " + temp[ 1] + " " + temp[2] + " " + temp[3]);


                string idkameryValue = temp[3].ToString();
                globalneIdKamery = Int32.Parse(idkameryValue);

                Console.WriteLine("Odczytane id z tablicy bitowej: " + globalneIdKamery);

                byte[] temp2 = new byte[ms.ToArray().Length];
                Array.Copy(temp, 4, temp2, 0, temp.Length - 4); //przekopiowanie wartosci do nowej tablicy
                return temp2;
            }
        }



        //pisze wiadomsoc do klienta/serwera(nie potrzebne ale tak o jest)
        static void WriteData(NetworkStream stream, string cmd)
        {
            stream.Write(Encoding.UTF8.GetBytes(cmd), 0,
            Encoding.UTF8.GetBytes(cmd).Length);
        }


        //zwraca ip twojego kompa
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


        //proba zrobienia obrazu z tablicy bitow
        static public Image byteArrayToImage(byte[] bytesArr)
        {
            using (MemoryStream memstr = new MemoryStream(bytesArr))
            {
                Image img = Image.FromStream(memstr);
                return img;
            }
        }

        //wszystko co odebralismy wsadzamy do bazy -> logika jest tu
        static void zapisDoBazy(string message, int port, byte[] array)
        {
            //whereToSave - zmienna okreslajaca czy zapisac zdjecie, czy zapisac ilosc osob (1 - zdjecie, 2 - ilosc osob)
            Console.WriteLine("dzialam sobie");
            string command = "";
            int idKamery = 0;
            try
            {
                idKamery = message[0] - '0'; //pobranie id kamery z 1 znaku ciagu
                message = message.Substring(1);
            }
            catch(Exception e)
            {
                Console.WriteLine("Nie przetwarzam ciagu, wiec git");
            }
           

            SqlConnection con;
            SqlCommand cmd;

            string connectString = "";

            connectString = "Data Source= " + getLocalIpAddres() + ",1433; Network Library=DBMSSOCN; Initial Catalog =" + '"' + "GuestCounter" + '"' + "; User ID = " + "test" + "; Password=" + "test" + ";";

            con = new SqlConnection(connectString);

            try
            {
                con.Open();
                con.Close();

                Console.WriteLine("Laczenie z baza dziala pomyslnie");
            }
            catch(Exception e)
            {
                Console.WriteLine("Laczenie z baza nie dziala. Sprawdz ustawienia bazy dla uzytkownika [test] i sprobuj ponownie");
            }


            if (port == 8080)
            {
                command = "insert into Photos (camera_id,raport_date,photo_path) values (@idkam, @data, @path);";
            }
            else if (port == 8081)
            {
                command = "INSERT INTO Visitors(camera_id,guests_in,guests_out,raport_date) VALUES(@idkam, @ileWeszlo, @ileWyszlo, @data)";
            }
            else if (port == 8082)
            {
                command = "INSERT INTO Cameras(camera_location) VALUES(@idkam)";
            }

            cmd = new SqlCommand(command, con);

            if (port == 8080) //zdjecie
            {
                string data = ActualData();
                string newPath = data;
                newPath = newPath.Replace('-', '_');
                newPath = newPath.Replace(' ', '_');
                newPath = newPath.Replace(':', '_');
                newPath = "kamera"+globalneIdKamery+"_"+ newPath + ".png";

                string allPath = @"c:\\temp\" + newPath;
                try
                {
                    MemoryStream tempms = new MemoryStream(array);

                    Image i = Image.FromStream(tempms);


                    i.Save(allPath, ImageFormat.Png);


                    cmd.Parameters.Add(new SqlParameter("@idkam", globalneIdKamery));
                    cmd.Parameters.Add(new SqlParameter("@data", ActualData().ToString()));
                    cmd.Parameters.Add(new SqlParameter("@path", newPath));
                    Console.WriteLine("Zdjecie zapisano");
                }
                catch(Exception e)
                {
                    Console.WriteLine("Nie mozna zapisac zdjecia do pliku");
                }
               

               
            }
            else if (port == 8081) //ilosc osob
            {
                string[] listaOsob = message.Split(';'); // no 0 ile osob weszlo na 1 ile osob wyszlo
               
                cmd.Parameters.Add(new SqlParameter("@idkam", idKamery));
                cmd.Parameters.Add(new SqlParameter("@ileWeszlo", listaOsob[0]));
                cmd.Parameters.Add(new SqlParameter("@ileWyszlo", listaOsob[1]));
                cmd.Parameters.Add(new SqlParameter("@data", ActualData().ToString()));

            }
            else if (port == 8082) //nowa kamera
            {
                cmd.Parameters.Add(new SqlParameter("@idkam", idKamery));
            }

 
            try
            {
                if (con.State == ConnectionState.Open) con.Close();

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Console.WriteLine("Pomyslnie zapisano dane do bazy");
            }
            catch (Exception)
            {
                Console.WriteLine("Nie udalo sie poprawnie wprowadzic danych do bazy danych z operacji nr " + port + ":   [8080 - zdjecie, 8081 - liczba gosci])");
            }
        }

        //funkcja nasluchu dla info odnosnie liczby gosciu
        static void GuestListener()
        {
            int port = 8081;
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
                        byte[] array = { 0 };
                        zapisDoBazy(message, port, array);
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



        //funkcja nasluchu do odebrania obrazow
        static void PictureListener()
        {
            int port = 8080;
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
                //MemoryStream tempMem = new MemoryStream();

                    try
                    {
                    Console.WriteLine(port);
                    byte[] tempArray = ReadFully(network);
                        //zapisDoBazy("nie ma", port, ReadFully(network)); //cala funkcja odczytania info i zapisu zdjecia
                        zapisDoBazy("nie ma", port, tempArray); //cala funkcja odczytania info i zapisu zdjecia
                    //Console.WriteLine("Zdjecie zapisano poprawnie");
                    }
                    catch
                    {
                        Console.WriteLine("Brak zapisanego zdjecia");
                    }

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
