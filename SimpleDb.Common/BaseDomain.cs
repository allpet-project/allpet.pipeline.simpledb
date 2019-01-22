using System;
using AllPet.db.simple;

namespace SimpleDb.Common
{
    public class BaseDomain: IDisposable
    {
        protected AllPet.db.simple.DB simpledb = new AllPet.db.simple.DB();
        public BaseDomain(string dbPath)
        {
            simpledb.Open(dbPath, true);
        }

        public void Dispose()
        {
            simpledb.Dispose();
        }

        protected void ApplyChange(ICommand command)
        {
            dynamic d = this;

            d.Handle(Converter.ChangeTo(command, command.GetType()));
        }
    }
}
