using System;
using System.Collections.Generic;

namespace UniFiSharp.Access.Models
{
    public class Building : Location 
    {
        public override string Location_Type
        {
            get
            {
                return base.Location_Type;
            }

            set
            {
                if (value != "building")
                {
                    throw new InvalidCastException("Location is not of type building!");
                }
                base.Location_Type = value;
            }
        }

        public IEnumerable<Floor> Floors { get; set; }
    }
}
