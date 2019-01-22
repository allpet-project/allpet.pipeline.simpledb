using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDb.Common.Message
{
    [Serializable()]
    public class DeleteTableCommand:ICommand
    {
        public byte[] TableId { get; set; }
    }
}
