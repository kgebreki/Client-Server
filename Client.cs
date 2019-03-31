using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Net;

//Kaleb Gebrekirstos
//Carrik McNerlin
//CS 305 Client-Server Exercise
//03/29/19
/***************************************************************************************************************************************************
 *The aim of this assignment is to create a simple service that allows for a direct communication between the client and the server. It does this  *
 * by allowing the server to accept a string from the client and then sending an appropriate message of its own. This establishes a two way channel*
 * for communication between the client and the server. Furthermore, timers(stopwatch) have been used in this modified version.                                                                                           *
 *************************************************************************************************************************************************** 
 */
namespace ClientServer2
{
    class Client
    {
        static void Main(string[] args)
        {
            TcpClient trec = null;  //client tcp connection object
            StreamReader sr = null; //input stream for connection
            StreamWriter sw = null;//output stream for connection
            String serverIp = "127.0.0.1";
            string message = null;
            var watch = new Stopwatch();//Timer object to check how long a specific process takes to execute
            Console.WriteLine("Enter your name: ");
            message = Console.ReadLine();
            Console.WriteLine("Hello, " + message);
            Console.WriteLine("You may begin your communication with the server... ");
            do
            {
                try
                {
                    trec = new TcpClient();
                    trec.Connect(serverIp, 25000);
                   
                    //we send nothing, but should get a couple of lines back
                    sr = new StreamReader(trec.GetStream());
                    sw = new StreamWriter(trec.GetStream());
                    
                    message = Console.ReadLine();
  
                    sw.WriteLine(message);
                    sw.Flush();

                    watch.Start();
                    String serverInput = sr.ReadLine();
                    watch.Stop();
                    Console.WriteLine($"Time taken to retrieve data from server: {watch.ElapsedMilliseconds} ms");
                    Console.WriteLine("Message from server: " + serverInput);
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e + " " + e.StackTrace);
                    System.Threading.Thread.Sleep(5000);
                }
                //clean up tcp connection and input stream
                if (sr != null) sr.Close();
                if (trec != null) trec.Close();
            } while (message != null);
        }
    }
}
