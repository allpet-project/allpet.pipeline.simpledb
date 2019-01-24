using AllPet.Pipeline;
using SimplDb.Protocol.Sdk;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SimpleDb.Server.Actor
{
    public class SimpleDbModule : Module
    {
        protected AllPet.db.simple.DB simpledb = new AllPet.db.simple.DB();
        public SimpleDbModule() : base(false)
        {
        }
        public override void OnStart()
        {
            var dbPath = SimpleDbConfig.GetInstance().GetDbSetting();
            simpledb.Open(dbPath, true);
        }
        public override void OnTell(IModulePipeline from, byte[] data)
        {
            Console.WriteLine("SimpleDbModule");
            var command  = ProtocolFormatter.Deserialize(data);

            ServerDomain domain = new ServerDomain(this.simpledb);
            domain.ExcuteCommand(command);

        }
    }
}
