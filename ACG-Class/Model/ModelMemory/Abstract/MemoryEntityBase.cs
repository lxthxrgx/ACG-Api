using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACG_Class.Model.ModelMemory.Abstract
{
    public abstract class MemoryEntityBase
    {
        public abstract DateTime AddedTime { get; set; }
        public abstract DateTime UpdatedTime { get; set; }
    }
}
