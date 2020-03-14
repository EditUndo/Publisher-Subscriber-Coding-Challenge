using System;
using System.IO;
using System.Net;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

using System.Net.Sockets;


namespace NTCC
{
    public class EchoClient
    {


        public static string[] messages = new string[10];
        public static void Hit(IAsyncResult ar)
        {
            reader owner = (reader)ar.AsyncState;
            int byteread = owner.stream.EndRead(ar);
            Console.WriteLine("From Server: " + Encoding.UTF8.GetString(owner.buffer, 0, byteread));
            owner.stream.BeginRead(owner.buffer, 0, 30, new AsyncCallback(Hit), owner);
        }
        public struct reader
        {
            public byte[] buffer;
            public NetworkStream stream;
        }
        public static void Main()
        {
            try
            {

                TcpClient client = new TcpClient("127.0.0.1", 5001);

                StreamWriter writer = new StreamWriter(client.GetStream());

                writer.Write("     hello");
                writer.Flush();
                reader streamowner = new reader();
                streamowner.buffer = new byte[1024];
                streamowner.stream = (NetworkStream)writer.BaseStream;
                streamowner.stream.BeginRead(streamowner.buffer, 0, 30, new AsyncCallback(Hit), streamowner);
                String s = String.Empty;
                while (true)
                {
                    Console.WriteLine("Enter Message Type:  ");
                    string type = Console.ReadLine();
                    byte[] message = null;
                    int fullmessagelen = 0;
                    switch (type)
                    {
                        case "1":
                            s = "1hello";

                            message = Encoding.UTF8.GetBytes(s);

                            fullmessagelen = message.Length + BitConverter.GetBytes(message.Length).Length;

                            writer.BaseStream.Write(BitConverter.GetBytes(fullmessagelen), 0, 4);
                            writer.Flush();

                            Console.Write("Press Enter to Send");
                            Console.ReadLine();

                            message[0] = 0x01;

                            writer.BaseStream.Write(message, 0, message.Length);
                            writer.Flush();
                            break;
                        case "2":
                            Console.Write("Enter topic: ");
                            s = "2" + Console.ReadLine();
                            message = Encoding.UTF8.GetBytes(s);

                            fullmessagelen = message.Length + BitConverter.GetBytes(message.Length).Length;

                            writer.BaseStream.Write(BitConverter.GetBytes(fullmessagelen), 0, 4);
                            writer.Flush();

                            Console.Write("Press Enter to Send");
                            Console.ReadLine();

                            message[0] = 0x02;

                            writer.BaseStream.Write(message, 0, message.Length);
                            writer.Flush();
                            break;
                        case "3":
                            Console.Write("Enter topic: ");
                            s = "3" + Console.ReadLine();
                            message = Encoding.UTF8.GetBytes(s);

                            fullmessagelen = message.Length + BitConverter.GetBytes(message.Length).Length;

                            writer.BaseStream.Write(BitConverter.GetBytes(fullmessagelen), 0, 4);
                            writer.Flush();

                            Console.Write("Press Enter to Send");
                            Console.ReadLine();

                            message[0] = 0x03;

                            writer.BaseStream.Write(message, 0, message.Length);
                            writer.Flush();
                            break;
                        case "5":

                            Console.Write("Enter topic: ");
                            string topic = Console.ReadLine();
                            Int16 toplen = (short)Encoding.UTF8.GetByteCount(topic);

                            Console.Write("Enter content: ");
                            string content = Console.ReadLine();
                            Int16 conlen = (short)Encoding.UTF8.GetByteCount(content);

                            using (MemoryStream ms = new MemoryStream())
                            {
                                ms.Write(Encoding.UTF8.GetBytes("5"), 0, Encoding.UTF8.GetByteCount("5"));

                                ms.Write(BitConverter.GetBytes(toplen), 0, BitConverter.GetBytes(toplen).Length);
                                ms.Write(Encoding.UTF8.GetBytes(topic), 0, toplen);

                                ms.Write(BitConverter.GetBytes(conlen), 0, BitConverter.GetBytes(conlen).Length);
                                ms.Write(Encoding.UTF8.GetBytes(content), 0, conlen);

                                message = ms.ToArray();

                                message[0] = 0x05;

                                fullmessagelen = message.Length + BitConverter.GetBytes(message.Length).Length;
                            }

                            writer.BaseStream.Write(BitConverter.GetBytes(fullmessagelen), 0, 4);
                            writer.Flush();
                            Console.Write("Press Enter to Send");
                            Console.ReadLine();
                            writer.BaseStream.Write(message, 0, message.Length);
                            writer.Flush();
                            break;
                    }


                }

                writer.Close();
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
        }
    }
}