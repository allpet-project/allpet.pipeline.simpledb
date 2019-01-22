using Newtonsoft.Json;
using SimpleDb.Common.Message;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace SimpleDb.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client Hello World!");
            TestLoop();
            Console.ReadLine();
        }

        static void TestLoop()
        {
            while (true)
            {
                Console.WriteLine("1.CreateTable>");
                Console.WriteLine("2.PutDirect>");
                Console.WriteLine("3.GetDirect>");
                Console.WriteLine("4.PutUInt64>");
                Console.WriteLine("5.DeleteDirect>");
                Console.WriteLine("6.DeleteTable>");
                var line = Console.ReadLine();
                if (line == "1")
                {
                    CreateTable();
                }
                if (line == "2")
                {                   
                    PutDirect();
                }
                if (line == "3")
                {
                    GetDirect();
                }
                if (line == "4")
                {
                    PutUInt64();
                }
                if (line == "5")
                {
                    DeleteDirect();
                }
                if (line == "6")
                {
                    DeleteTable();
                }
            }
        }

        private static void CreateTable()
        {
            var systemL = AllPet.Pipeline.Instance.CreateActorSystem();
            systemL.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            var remote = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8888);
            var systemref = systemL.Connect(remote).Result;
            var actor = systemL.GetPipeline(null, "127.0.0.1:8888/createtable");
            {
                MemoryStream ms = new MemoryStream();
                ////方法
                //byte[] methodBytes = BitConverter.GetBytes(4);
                //byte[] mlenBytes = BitConverter.GetBytes(methodBytes.Length);
                //ms.Write(mlenBytes, 0, mlenBytes.Length);
                //ms.Write(methodBytes, 0, methodBytes.Length);

                ////tableId
                //byte[] tableIdBytes = new byte[] { 0x01, 0x02, 0x03 };
                //byte[] tlenBytes = BitConverter.GetBytes(tableIdBytes.Length);
                //ms.Write(tlenBytes, 0, tlenBytes.Length);
                //ms.Write(tableIdBytes, 0, tableIdBytes.Length);

                ////data
                //byte[] dataBytes = new byte[8000];
                //byte[] dlenBytes = BitConverter.GetBytes(dataBytes.Length);
                //ms.Write(dlenBytes, 0, dlenBytes.Length);
                //ms.Write(dataBytes, 0, dataBytes.Length);

                //actor.Tell(ms.ToArray());

                CreatTableCommand command = new CreatTableCommand()
                {
                     TableId = new byte[] { 0x01, 0x02, 0x03 },
                     Data  = new byte[8000]
                };
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, command);
                actor.Tell(ms.ToArray());
            }
        }
        private static void PutDirect()
        {
            var systemL = AllPet.Pipeline.Instance.CreateActorSystem();
            systemL.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            var remote = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8888);
            var systemref = systemL.Connect(remote).Result;
            var actor = systemL.GetPipeline(null, "127.0.0.1:8888/put");
            {
                MemoryStream ms = new MemoryStream();
                PutDirectCommand command = new PutDirectCommand()
                {
                    TableId = new byte[] { 0x02, 0x02, 0x03 },
                    Key = new byte[] { 0x12,0x12},
                    Data = new byte[8000]
                };
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, command);
                actor.Tell(ms.ToArray());
            }
        }
        private static void GetDirect()
        {
            var systemL = AllPet.Pipeline.Instance.CreateActorSystem();
            systemL.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            var remote = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8888);
            var systemref = systemL.Connect(remote).Result;
            var actor = systemL.GetPipeline(null, "127.0.0.1:8888/get");
            {
                MemoryStream ms = new MemoryStream();
                ////方法
                //byte[] methodBytes = BitConverter.GetBytes(4);
                //byte[] mlenBytes = BitConverter.GetBytes(methodBytes.Length);
                //ms.Write(mlenBytes, 0, mlenBytes.Length);
                //ms.Write(methodBytes, 0, methodBytes.Length);

                //tableId
                byte[] tableIdBytes = new byte[] { 0x01, 0x02, 0x03 };
                byte[] tlenBytes = BitConverter.GetBytes(tableIdBytes.Length);
                ms.Write(tlenBytes, 0, tlenBytes.Length);
                ms.Write(tableIdBytes, 0, tableIdBytes.Length);

                //data
                byte[] dataBytes = new byte[8000];
                byte[] dlenBytes = BitConverter.GetBytes(dataBytes.Length);
                ms.Write(dlenBytes, 0, dlenBytes.Length);
                ms.Write(dataBytes, 0, dataBytes.Length);

                actor.Tell(ms.ToArray());
            }
        }

        private static void PutUInt64()
        {
            var systemL = AllPet.Pipeline.Instance.CreateActorSystem();
            systemL.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            var remote = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8888);
            var systemref = systemL.Connect(remote).Result;
            var actor = systemL.GetPipeline(null, "127.0.0.1:8888/putuint64");
            {
                MemoryStream ms = new MemoryStream();
                PutUInt64Command command = new PutUInt64Command()
                {
                    TableId = new byte[] { 0x02, 0x02, 0x03 },
                    Key = new byte[] { 0x13, 0x13 },
                    Data = 18446744073709551614
                };
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, command);
                actor.Tell(ms.ToArray());
            }
        }

        private static void DeleteDirect()
        {
            var systemL = AllPet.Pipeline.Instance.CreateActorSystem();
            systemL.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            var remote = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8888);
            var systemref = systemL.Connect(remote).Result;
            var actor = systemL.GetPipeline(null, "127.0.0.1:8888/del");
            {
                MemoryStream ms = new MemoryStream();
                DeleteCommand command = new DeleteCommand()
                {
                    TableId = new byte[] { 0x02, 0x02, 0x03 },
                    Key = new byte[] { 0x12, 0x12 },
                };
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, command);
                actor.Tell(ms.ToArray());
            }
        }

        private static void DeleteTable()
        {
            var systemL = AllPet.Pipeline.Instance.CreateActorSystem();
            systemL.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            var remote = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8888);
            var systemref = systemL.Connect(remote).Result;
            var actor = systemL.GetPipeline(null, "127.0.0.1:8888/deltable");
            {
                MemoryStream ms = new MemoryStream();
                DeleteTableCommand command = new DeleteTableCommand()
                {
                    TableId = new byte[] { 0x01, 0x02, 0x03 }
                };
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, command);
                actor.Tell(ms.ToArray());
            }
        }
    }
}
