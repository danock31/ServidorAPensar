using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServidorApensar
{
    class Program
    {
        public static Hashtable clientsList = new Hashtable();
        public static Hashtable Opciones = new Hashtable();
        public static int contadorGuardar = 0;
        public static int puntoajugadoruno = 1;
        public static int puntoajugadordos = 1;
        static void Main(string[] args)
        {
            TcpListener serverSocket = new TcpListener(8888);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            Console.WriteLine("SERVIDOR INICIADO ....");

            counter = 0;
            try
            {
                while ((true))
                {
                    counter += 1;
                    clientSocket = serverSocket.AcceptTcpClient();

                    byte[] bytesFrom = new byte[10025];
                    string dataFromClient = null;

                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, 1024);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    clientsList.Add(dataFromClient, clientSocket);

                    broadcast(dataFromClient + " Se Ha Unido a La Sala ", dataFromClient, false);

                    Console.WriteLine(dataFromClient + " Se Ha Unido a La Sala ");
                    handleClinet client = new handleClinet();
                    client.startClient(clientSocket, dataFromClient, clientsList);
                }
            }
            catch
            {
                clientSocket.Close();
                serverSocket.Stop();
                Console.WriteLine("exit");
                Console.ReadLine();
            }


            
        }

        public static void broadcast(string msg, string uName, bool flag)
        {
            foreach (DictionaryEntry Item in clientsList)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;

                if (flag == true)
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(" Dice : " + uName+" :" + msg);
                }
                else
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(msg);
                }

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }  //end broadcast function
        public static void GuardarSeleccion(string msg, string uName)
        {
            if (contadorGuardar < 2)
            {
                String[] buscarnombre = System.Text.RegularExpressions.Regex.Split(msg, ",");
                string seleccion = buscarnombre[1];
                Opciones.Add(uName, seleccion);
                contadorGuardar++;
            }
            if (contadorGuardar == 2)
            {
                contadorGuardar = 0;
                string[] nombre = new string[2];
                string[] selec = new string[2];
                int cont = 0;
                foreach (DictionaryEntry Item in Opciones)
                {
                    nombre[cont] = Item.Key.ToString();
                    selec[cont] = Item.Value.ToString();
                    cont++;
                }
                if (selec[0] == "1" && selec[1]=="0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadoruno++;
                }
                else if (selec[0] == "2" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadoruno++;
                }
                else if (selec[0] == "3" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadoruno++;
                }
                else if (selec[0] == "4" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadoruno++;
                }
                else if (selec[0] == "5" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadoruno++;
                }
                else if (selec[0] == "6" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadoruno++;
                }
                else if (selec[0] == "7" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadoruno++;
                }
                 if (selec[0] == "Gano" && selec[1]=="NoTermino")
                {
                    MandarGanador("Gano", nombre[0], true);
                    puntoajugadoruno = 8;
                }
                
                 else if (selec[1] == "Gano" && selec[0]== "NoTermino")
                {
                    MandarGanador("Gano", nombre[1], false);
                    puntoajugadordos =8;
                }
                if (selec[1] == "1" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadordos++;
                }
                else if (selec[1] == "2" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadordos++;
                }
                else if (selec[1] == "3" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadordos++;
                }
                else if (selec[1] == "4" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadordos++;
                }
                else if (selec[1] == "5" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadordos++;
                }
                else if (selec[1] == "6" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadordos++;
                }
                else if (selec[1] == "7" && selec[1] == "0")
                {
                    MandarGanador("Acertaste ", nombre[0], true);
                    puntoajugadordos++;
                }


            }


        }  //Fin de GuardarSeleccion
        public static void MandarGanador(string msg, string uName, bool flag)
        {
            foreach (DictionaryEntry Item in clientsList)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;

                if (flag == true)
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(msg + " " + uName + " " + "Puntos Actuales " + puntoajugadoruno);
                }
                else
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(msg + " " + uName + " " + "Puntos Actuales " + puntoajugadordos);
                }

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();

            }
            Opciones.Clear();
        }  //fin de mandarganador
    }
    public class handleClinet
    {
        TcpClient clientSocket;
        string clNo;
        Hashtable clientsList;

        public void startClient(TcpClient inClientSocket, string clineNo, Hashtable cList)
        {
            this.clientSocket = inClientSocket;
            this.clNo = clineNo;
            this.clientsList = cList;
            Thread ctThread = new Thread(doChat);
            ctThread.Start();
        }

        private void doChat()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[10025];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;

            while ((true))
            {
                try
                {
                   
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    // networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    networkStream.Read(bytesFrom, 0, 1024);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine("From client - " + clNo + " : " + dataFromClient);
                    rCount = Convert.ToString(requestCount);
                    //if (dataFromClient.Contains("Listo,Si"))
                    //{
                    //    Program.GuardarSeleccion(dataFromClient, clNo);
                    //}
                    //else
                    //{
                        Program.broadcast(dataFromClient, clNo, true);
                    //}
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
