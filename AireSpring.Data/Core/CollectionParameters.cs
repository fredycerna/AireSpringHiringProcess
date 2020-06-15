using System;
using System.Collections.Generic;
using System.Text;

namespace AireSpring.Data.Core
{
    public enum AscDec { 
       Asc=0,
       Dec=1
    }

  public class CollectionParameters
    {
        public AscDec Ordering { get; set; }

        public string OrderBy { get; set; }

        public int? Limit { get; set; }

        public int? Offset { get; set; }


    }
}
