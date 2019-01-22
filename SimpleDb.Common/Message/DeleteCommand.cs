using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDb.Common.Message
{
    [Serializable()]
    public class DeleteCommand : ICommand
    {
        public byte[] TableId { get; set; }
        public byte[] Key { get; set; }
    }
}
