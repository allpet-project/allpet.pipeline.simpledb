using System;
using System.Collections.Generic;
using System.Text;

namespace SimplDb.Protocol.Sdk.Message
{
    [Serializable()]
    public class GetDirectCommand: ICommand
    {
        public byte[] TableId { get; set; }
        public byte[] Key { get; set; }
    }
}
