using AllPet.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace PipelineHelloWord.Actor
{
    class HelloWordActor: Pipeline
    {
        public HelloWordActor(IPipelineSystem system) : base(system)
        {
        }
        public override void OnTell(IPipelineRef from, byte[] data)
        {
            Console.WriteLine("Hello:" + System.Text.Encoding.UTF8.GetString(data));
        }
    }
}
