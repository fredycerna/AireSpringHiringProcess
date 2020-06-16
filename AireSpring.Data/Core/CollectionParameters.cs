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
        /// <summary>
        /// Parameter to order collection asc or desc
        /// </summary>
        public AscDec Ordering { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderBy { get; set; }

        public int? Limit { get; set; }

        public int? Offset { get; set; }


    }
}
