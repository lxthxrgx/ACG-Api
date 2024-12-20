using System;
using System.Collections;
using System.Collections.Generic;

namespace ACG_Class.Model.Class
{
    public class _2D
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
        public SubleaseDop? SubleaseDop { get; set; }
    }

}
