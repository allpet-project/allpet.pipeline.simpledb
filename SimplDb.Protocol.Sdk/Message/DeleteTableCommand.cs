using System;
using System.Collections.Generic;
using System.Text;

namespace SimplDb.Protocol.Sdk.Message
{
    [Serializable()]
    public class DeleteTableCommand:ICommand
    {
        public byte[] TableId { get; set; }
    }
}
