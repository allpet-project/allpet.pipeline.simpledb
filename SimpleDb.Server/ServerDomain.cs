﻿using SimplDb.Protocol.Sdk;
using SimplDb.Protocol.Sdk.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDb.Server
{
    public class ServerDomain: BaseDomain
    {
        private AllPet.db.simple.DB SimpleDb;
        public ServerDomain(AllPet.db.simple.DB simpledb)
        {
            this.SimpleDb = simpledb;
        }
        public void ExcuteCommand(ICommand command)
        {
            ApplyChange(command);
        }

        public void Handle(CreatTableCommand command)
        {
            Console.WriteLine("CreatTableCommand");
            this.SimpleDb.CreateTableDirect(command.TableId, command.Data);
        }

        public void Handle(GetDirectCommand command)
        {
            Console.WriteLine("GetDirectCommand");
            var bytes = this.SimpleDb.GetDirect(command.TableId, command.Key);            
        }
        public void Handle(PutDirectCommand command)
        {
            Console.WriteLine("PutDirectCommand");
            this.SimpleDb.PutDirect(command.TableId, command.Key, command.Data);
        }

        public void Handle(PutUInt64Command command)
        {
            Console.WriteLine("PutUInt64Command");
            this.SimpleDb.PutUInt64Direct(command.TableId, command.Key, command.Data);
        }

        public void Handle(DeleteCommand command)
        {
            Console.WriteLine("DeleteCommand");
            this.SimpleDb.DeleteDirect(command.TableId, command.Key);
        }

        public void Handle(DeleteTableCommand command)
        {
            Console.WriteLine("DeleteTableCommand");
            this.SimpleDb.DeleteTableDirect(command.TableId);
        }
    }
}
