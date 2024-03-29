﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadaca1RPR.Models;
using System.Drawing;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace Zadaca1RPR.Abstracts
{
    [Serializable]
    public abstract class Patient
    {

        public Patient() { }
        
        [XmlIgnore]
        protected static int ID { get; set; }

        public virtual string UserName { get; set; }
        
        public virtual string Password { get; set; }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual DateTime BirthDate { get; set; }

        public virtual int IDnum { get; set; }

        public virtual string Address { get; set; }

        public virtual bool Married { get; set; }

        public virtual DateTime RegisterDate { get; set; }
        
        [XmlIgnore]
        public virtual EnumGender Gender { get; set; }

        public virtual HealthBook HealthBook { get; set; }

        public virtual bool HasHealthCard { get; set; }

        public virtual List<string> Schedule { get; set; }

        public virtual int GetID() { return IDnum;  }
        
        public virtual double Cost { get; set; }

        public virtual int numOfTimesVisited { get; set; }

        public virtual string CitizenID { get; set; }

        [XmlIgnore]
        public virtual Image img { get; set; }

    }
}
