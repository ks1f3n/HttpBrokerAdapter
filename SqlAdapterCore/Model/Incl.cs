using System;
using System.Collections.Generic;
using System.Text;

namespace SqlAdapterCore.Model
{
    public class Incl : ModelIntIdentifier
    {
        public int InitX { get; set; }
        public int X { get; set; }
        public int InitY { get; set; }
        public int Y { get; set; }
    }
}
