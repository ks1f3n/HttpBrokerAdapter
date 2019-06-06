using System;
using System.Collections.Generic;
using System.Text;

namespace SqlAdapterCore.Model
{
    public class Gap : ModelIntIdentifier
    {
        public int InitValue { get; set; }
        public int Value { get; set; }
    }
}
