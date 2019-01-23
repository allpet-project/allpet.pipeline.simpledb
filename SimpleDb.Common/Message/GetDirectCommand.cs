using AllPet.Pipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleDb.Common.Message
{
    [Serializable()]
    public class GetDirectCommand: ICommand
    {
        [field: NonSerializedAttribute]
        public IModuleRef From { get; set; }
        public byte[] TableId { get; set; }
        public byte[] Key { get; set; }
    }
}
