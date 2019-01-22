using AllPet.Pipeline;
using SimpleDb.Common;
using SimpleDb.Common.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDb.Server.Actor
{
    public class GetDirectActor : Pipeline
    {
        public GetDirectActor(IPipelineSystem system) : base(system)
        {
        }
        public override void OnTell(IPipelineRef from, byte[] data)
        {
            Console.WriteLine("Remote :");
            var message = ConvertMessage.ConvertMessageObj(data, Method.GetDirect);
            //message.from = from;
            ServerDomain domain = new ServerDomain();
            domain.ExcuteCommand(message);
            domain.Dispose();
        }
    }
}
