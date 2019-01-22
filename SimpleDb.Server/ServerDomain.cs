using SimpleDb.Common;
using SimpleDb.Common.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDb.Server
{
    public class ServerDomain: BaseDomain
    {
        public ServerDomain():base(SimpleDbConfig.GetInstance().GetDbSetting())
        {

        }
        public void ExcuteCommand(ICommand command)
        {
            ApplyChange(command);
        }

        public void Handle(CreatTableCommand command)
        {
            Console.WriteLine("CreatTableCommand");
            simpledb.CreateTableDirect(command.TableId, command.Data);
        }

        public void Handle(GetDirectCommand command)
        {
            Console.WriteLine("GetDirectCommand");
            var bytes = simpledb.GetDirect(command.TableId, command.Key);
            command.From.Tell(bytes);
        }
        public void Handle(PutDirectCommand command)
        {
            Console.WriteLine("PutDirectCommand");
            simpledb.PutDirect(command.TableId, command.Key, command.Data);
        }

        public void Handle(PutUInt64Command command)
        {
            Console.WriteLine("PutUInt64Command");
            simpledb.PutUInt64Direct(command.TableId, command.Key, command.Data);
        }

        public void Handle(DeleteCommand command)
        {
            Console.WriteLine("DeleteCommand");
            simpledb.DeleteDirect(command.TableId, command.Key);
        }

        public void Handle(DeleteTableCommand command)
        {
            Console.WriteLine("DeleteTableCommand");
            simpledb.DeleteTableDirect(command.TableId);
        }
    }
}
