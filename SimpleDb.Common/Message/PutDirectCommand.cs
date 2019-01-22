using AllPet.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDb.Common.Message
{
    [Serializable()]
    public class PutDirectCommand : ICommand
    {
        public byte[] TableId { get; set; }
        public byte[] Key { get; set; }
        public byte[] Data { get; set; }
    }
}
