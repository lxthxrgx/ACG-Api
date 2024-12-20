using ACG_Class.Model.ModelMemory.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ACG_Class.Model.ModelMemory.Class
{
    public class _2D_Memory : MemoryEntityBase
    {
        public int Id { get; set; }
        public int NumberGroup { get; set; }
        public string NameGroup { get; set; }
        public string? PIBS { get; set; }
        public string address { get; set; }
        public double? area { get; set; }
        public string? rent { get; set; }

        public bool? isAlert { get; set; }
        public DateTime? DateCloseDepartment { get; set; }

        public int? SubleaseDopId { get; set; }
        public SubleaseDop_Memory? SubleaseDop { get; set; }

        public DateTime added { get; set; }
        public DateTime updated { get; set; }

        public override DateTime AddedTime
        {
            get => added;
            set => added = DateTime.Now;
        }

        public override DateTime UpdatedTime
        {
            get => updated;
            set => updated = DateTime.Now;
        }
    }

}
