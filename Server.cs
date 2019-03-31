using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Diagnostics;

//Kaleb Gebrekirstos
//Carrik McNerlin
//CS 305 Client-Server Exercise
//03/29/19
/***************************************************************************************************************************************************
 *The aim of this assignment is to create a simple service that allows for a direct communication between the client and the server. It does this  *
 * by allowing the server to accept a string from the client and then sending an appropriate message of its own. This establishes a two way channel*
 * for communication between the client and the server. Furthermore, timers(stopwatch) have been used in this modified version.                                                                                            *
 *************************************************************************************************************************************************** 
 */
namespace ClientServer
{
    class Server
    {
        [STAThread]
        static void Main(string[] args)
        {
            TcpListener tserver = null;
            var watch = new Stopwatch();
            try
            {
                //Using port number 25000
                Console.WriteLine("Starting server");
                IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
                tserver = new TcpListener(ipAddr, 25000);
                tserver.Start();
                watch.Start();
                //most servers run forever
                for (; ; )
                {
                    //we're blocked here until a connection request arrives
                    Console.WriteLine("Waiting for connection...");
                    TcpClient tclient = tserver.AcceptTcpClient();
                    
                    //no input is considered; just open the output stream
                    //and respond with the time
                    Console.WriteLine("...Connection accepted.");
                    StreamWriter sw = new StreamWriter(tclient.GetStream());
                    StreamReader sr = new StreamReader(tclient.GetStream());
                    watch.Stop();
                    Console.WriteLine($"Time taken to establish a connection: {watch.ElapsedMilliseconds} ms");

                    //Get input from client and see how much time it takes to read data from the stream
                    if (!watch.IsRunning)
                        watch.Restart();
                    String input = sr.ReadLine();
                    watch.Stop();

                    Console.WriteLine($"Time taken to get data from client: {watch.ElapsedMilliseconds} ms");

                    Console.WriteLine("Message from client: "+input);
                    
                    Console.WriteLine("Message to client: ");

                    String message = Console.ReadLine();
                    sw.WriteLine(message);
                    sw.Flush();
                }
            } //ouch--something unexpected happened
            catch (Exception e)
            {
                Console.WriteLine(e + " " + e.StackTrace);
            }
            finally
            {
                tserver.Stop();
                Console.WriteLine("Server stopped!");
            }
        }
    }
}
