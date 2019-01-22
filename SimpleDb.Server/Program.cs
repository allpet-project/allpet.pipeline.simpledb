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
           
            var serverSys = AllPet.Pipeline.Instance.CreateActorSystem();
            serverSys.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            serverSys.OpenListen(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 8888));
            serverSys.RegistPipeline("createtable", new CreateTableActor(serverSys));
            serverSys.RegistPipeline("put", new PutDirectActor(serverSys));
            serverSys.RegistPipeline("get", new GetDirectActor(serverSys));
            serverSys.RegistPipeline("putuint64", new PutUInt64Actor(serverSys));
            serverSys.RegistPipeline("del", new DeleteActor(serverSys));
            serverSys.RegistPipeline("deltable", new DeleteTableActor(serverSys));
            Console.ReadLine();
        }
    }
}
