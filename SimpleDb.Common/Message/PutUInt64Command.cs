using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDb.Common.Message
{
    [Serializable()]
    public class PutUInt64Command:ICommand
    {
        public byte[] TableId { get; set; }
        public byte[] Key { get; set; }
        public UInt64 Data { get; set; }
    }
}
