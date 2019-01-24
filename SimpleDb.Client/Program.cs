using Newtonsoft.Json;
using SimplDb.Protocol.Sdk;
using SimplDb.Protocol.Sdk.Message;
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
            var systemL = AllPet.Pipeline.PipelineSystem.CreatePipelineSystemV1();
            systemL.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            var remote = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8888);
            var systemref = systemL.Connect(remote).Result;
            var actor = systemL.GetPipeline(null, "127.0.0.1:8888/createtable");
            {
                MemoryStream ms = new MemoryStream();               

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
            var systemL = AllPet.Pipeline.PipelineSystem.CreatePipelineSystemV1();
            systemL.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            var remote = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8888);
            var systemref = systemL.Connect(remote).Result;
            var actor = systemL.GetPipeline(null, "127.0.0.1:8888/put");
            {
                MemoryStream ms = new MemoryStream();
                PutDirectCommand command = new PutDirectCommand()
                {
                    TableId = new byte[] { 0x03, 0x02, 0x03 },
                    Key = new byte[] { 0x10,0x10},
                    Data = new byte[8000]
                };
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, command);                
                actor.Tell(ms.ToArray());

                var bytes = ProtocolFormatter.Serialize< PutDirectCommand>(Method.PutDirect,command);
                actor.Tell(bytes);
            }
        }
        private static void GetDirect()
        {
            var clientServer = AllPet.Pipeline.PipelineSystem.CreatePipelineSystemV1();
            clientServer.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            clientServer.OpenListen(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 8889));
            var getactor = new GetActor();
            clientServer.RegistModule("getback", getactor);
            

            var server = AllPet.Pipeline.PipelineSystem.CreatePipelineSystemV1();
            server.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            var remote = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8888);
            var systemref = server.Connect(remote).Result;
            getactor = new GetActor();
            server.RegistModule("getback", getactor);
            server.Start();
            //var actor = server.GetPipeline(null, "127.0.0.1:8888/get");
            //{
            //    MemoryStream ms = new MemoryStream();
            //    GetDirectCommand command = new GetDirectCommand()
            //    {
            //        TableId = new byte[] { 0x02, 0x02, 0x03 },
            //        Key = new byte[] { 0x13, 0x13 }
            //    };
            //    BinaryFormatter bf = new BinaryFormatter();
            //    bf.Serialize(ms, command);
            //    actor.Tell(ms.ToArray());
            //}
        }

        private static void PutUInt64()
        {
            var systemL = AllPet.Pipeline.PipelineSystem.CreatePipelineSystemV1();
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
            var systemL = AllPet.Pipeline.PipelineSystem.CreatePipelineSystemV1();
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
            var systemL = AllPet.Pipeline.PipelineSystem.CreatePipelineSystemV1();
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
