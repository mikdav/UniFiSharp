using System;
using System.Collections.Generic;

namespace UniFiSharp.Access.Models
{
    public class Floor : Location
    {
        public override string Location_Type
        {
            get
            {
                return base.Location_Type;
            }

            set
            {
                if (value != "floor")
                {
                    throw new InvalidCastException("Location is not of type floor!");
                }
                base.Location_Type = value;
            }
        }

        public IEnumerable<Door> Doors { get; set; }
    }
}
