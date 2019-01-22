using System;
using System.Collections.Generic;
using System.Text;
using AllPet.Pipeline;

namespace SimpleDb.Common.Message
{
    [Serializable()]
    public class CreatTableCommand:ICommand
    {
        public byte[] TableId { get; set; }
        public byte[] Data { get; set; }
    }
}
