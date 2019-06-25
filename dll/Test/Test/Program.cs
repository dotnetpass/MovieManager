using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;
using System.Text;
using System.Collections.Generic;
using Newtonsoft.Json;
using NewPageDLL;
using System.Linq;

namespace Test
{
    class Program
    {
        private static byte[] result = new byte[1024 * 1024];
        private static int myProt = 5005;   //端口
        static Socket serverSocket;
        static void Main(string[] args)
        {
            //服务器IP地址
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口
            serverSocket.Listen(10);    //设定最多10个排队连接请求
            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());
            //通过Clientsoket发送数据
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            Console.ReadLine();
        }

        private static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                clientSocket.Send(Encoding.ASCII.GetBytes("Server Say Hello"));
                Thread receiveThread = new Thread(ReceiveMessage);
                receiveThread.Start(clientSocket);
            }
        }
        
        private static void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    //通过clientSocket接收数据
                    int receiveNumber = myClientSocket.Receive(result);
                    List<double> request = JsonConvert.DeserializeObject<List<double>>(Encoding.UTF8.GetString(result, 0, receiveNumber));
                    //Dictionary<string, IQueryable> request = JsonConvert.DeserializeObject<Dictionary<string, IQueryable>>(Encoding.UTF8.GetString(result, 0, receiveNumber));
                    var response = Page.QueryData(int.Parse(request["page"].ToString()), int.Parse(request["size"].ToString()), (IQueryable<object>)request["data"]);
                    Console.WriteLine("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), request["page"]);
                    myClientSocket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(response)));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }
    }
}