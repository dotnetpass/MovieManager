using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NewPageDLL;
using Newtonsoft.Json;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            //设定服务器IP地址
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip, 5005)); //配置服务器IP与端口
                Console.WriteLine("连接服务器成功");
            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return;
            }
            byte[] temp = new byte[1024];
            int receiveLength = clientSocket.Receive(temp);
            Console.WriteLine("接收服务器消息：{0}", Encoding.UTF8.GetString(temp, 0, receiveLength));
            try
            {
                Dictionary<string, IQueryable> request = new Dictionary<string, IQueryable>();
                var input = new List<int>();
                input.Add(1);
                input.Add(2);
                input.Add(3);
                input.Add(4);
                request.Add("data", (IQueryable<object>)input);
                clientSocket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(input)));
                Console.WriteLine("向服务器发送消息。");
                byte[] result = new byte[1024 * 1024 * 512];
                int receiveNumber = clientSocket.Receive(result);
                Console.WriteLine("消息:\n{0}", Encoding.UTF8.GetString(result, 0, receiveNumber));
            }
            catch
            {
                clientSocket.Shutdown(SocketShutdown.Both);
                clientSocket.Close();
            }
            Console.WriteLine("发送完毕，按回车键退出");
            Console.ReadLine();
        }
    }
}