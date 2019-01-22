using PipelineHelloWord.Common;
using System;

namespace PipelineHelloWord.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World Server!");
            var serverSys = AllPet.Pipeline.Instance.CreateActorSystem();
            serverSys.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            serverSys.OpenListen(new System.Net.IPEndPoint(System.Net.IPAddress.Any, 8888));
            serverSys.RegistPipeline("hello", new HelloRemoteActor(serverSys));
            Console.ReadLine();
        }
    }
}
