using AllPet.Pipeline;
using SimplDb.Protocol.Sdk.Message;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SimpleDb.Client
{
    public class GetActor : Module
    {
        public GetActor() : base(false)
        {
        }
        public override void OnStart()
        {
            MemoryStream ms = new MemoryStream();
            GetDirectCommand command = new GetDirectCommand()
            {
                TableId = new byte[] { 0x02, 0x02, 0x03 },
                Key = new byte[] { 0x13, 0x13 }
            };
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, command);
            var actor = this.GetPipeline("127.0.0.1:8888/get");
            actor.Tell(ms.ToArray());

        }
        public override void OnTell(IModulePipeline from, byte[] data)
        {
            Console.WriteLine("Remote :GetActor");

            //var message = ConvertMessage.ConvertMessageObj(data, Method.CreateTable);
            MemoryStream mStream = new MemoryStream();
            mStream.Write(data, 0, data.Length);
            mStream.Flush();
            mStream.Position = 0;
            BinaryFormatter bf = new BinaryFormatter();
            if (mStream.Capacity > 0)
            {
                //DeleteTableCommand command = (DeleteTableCommand)bf.Deserialize(mStream);
               
            }

        }
    }
}
