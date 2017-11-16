﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Models;

namespace Zadaca1RPR.Abstracts
{
    abstract class Patient
    {

        protected static int ID { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual DateTime BirthDate { get; set; }

        public virtual int IDnum { get; set; }

        public virtual string Address { get; set; }

        public virtual bool Married { get; set; }

        public virtual DateTime RegisterDate { get; set; }

        public virtual EnumGender Gender { get; set; }

        public virtual HealthBook HealthBook { get; set; }

        public virtual bool HasHealthCard { get; set; }

        public virtual List<string> Schedule { get; set; }

        public virtual int GetID() { return IDnum;  }

    }
}