using Newtonsoft.Json;
using PipelineHelloWord.Actor;
using PipelineHelloWord.Common;
using PipelineHelloWord.Common.Message;
using System;

namespace PipelineHelloWord
{ 
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //var system = AllPet.Pipeline.Instance.CreateActorSystem();
            //system.RegistPipeline("hello", new HelloWordActor(system));//actor习惯，连注册这个活都丢线程池，我这里简化一些

            //system.Start();
            //var actor = system.GetPipeline(null, "this/hello");
            //{
            //    actor.Tell(System.Text.Encoding.UTF8.GetBytes("yeah very good."));
            //}
            

            var systemL = AllPet.Pipeline.Instance.CreateActorSystem();
            systemL.OpenNetwork(new AllPet.peer.tcp.PeerOption());
            var remote = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 8888);
            var systemref = systemL.Connect(remote).Result;
            var actor = systemL.GetPipeline(null, "127.0.0.1:8888/hello");
            {
                HelloMessage message = new HelloMessage();
                message.Command = Command.Open;
                message.Path = "/aa/bb";
                message.CreateIfMissing = true;
                var json = JsonConvert.SerializeObject(message);
                actor.Tell(System.Text.Encoding.UTF8.GetBytes(json));
            }

            Console.ReadLine();
        }
    }
}
