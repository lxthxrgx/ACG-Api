﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACG_Class.Model.TaskManager.Model
{
    interface IUser
    {
        int Id { get; set; }
        string Name { get; set; }
    }

    public class User : IUser
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
