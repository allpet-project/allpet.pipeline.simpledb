using System;
using System.Collections.Generic;
using System.Text;

namespace SimplDb.Protocol.Sdk.Message
{
    [Serializable()]
    public class CreatTableCommand:ICommand
    {
        public byte[] TableId { get; set; }
        public byte[] Data { get; set; }
    }
}
