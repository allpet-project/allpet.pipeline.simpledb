using System;
using System.Collections.Generic;
using System.Text;

namespace PipelineHelloWord.Common.Message
{
    public class HelloMessage
    {
        public Command Command { get; set; }
        public string Path { get; set; }
        public bool CreateIfMissing { get; set; }
    }
    public enum Command
    {
        Open,
        Put,
        Get
    }
}
