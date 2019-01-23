using Microsoft.Extensions.Configuration;
using SimpleDb.Common;
using SimpleDb.Common.Message;
using SimpleDb.Server.Actor;
using System;
using System.IO;

namespace SimpleDb.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Server Hello World!");
           
            var serverSys = AllPet.Pipeline.PipelineSystem.CreatePipelineSystemV1();
            serverSys.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            serverSys.OpenListen(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 8888));
            serverSys.RegistModule("createtable", new CreateTableActor());
            serverSys.RegistModule("put", new PutDirectActor());
            serverSys.RegistModule("get", new GetDirectActor());
            serverSys.RegistModule("putuint64", new PutUInt64Actor());
            serverSys.RegistModule("del", new DeleteActor());
            serverSys.RegistModule("deltable", new DeleteTableActor());
            Console.ReadLine();
        }
    }
}
