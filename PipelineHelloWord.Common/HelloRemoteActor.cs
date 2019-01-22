using AllPet.Pipeline;
using Newtonsoft.Json;
using PipelineHelloWord.Common.Message;
using System;

namespace PipelineHelloWord.Common
{
    public class HelloRemoteActor : Pipeline
    {
        public HelloRemoteActor(IPipelineSystem system) : base(system)
        {
        }
        public override void OnTell(IPipelineRef from, byte[] data)
        {
            Console.WriteLine("Remote :" );
            var json = System.Text.Encoding.UTF8.GetString(data);
            var message = JsonConvert.DeserializeObject<HelloMessage>(json);
            switch(message.Command)
            {
                case Command.Open:
                    Console.WriteLine($"OpenDb Path:{message.Path}  CreateIfMissing:{message.CreateIfMissing}" );
                    break;
                case Command.Put:
                    Console.WriteLine($"Put ");
                    break;
                case Command.Get:
                    Console.WriteLine($"Get ");
                    break;
            }
        }
    }
}
