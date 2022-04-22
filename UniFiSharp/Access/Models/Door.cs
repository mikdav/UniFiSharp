using System;

namespace UniFiSharp.Access.Models
{
    public class Door : Location
    {
        public override string Location_Type
        {
            get
            {
                return base.Location_Type;
            }

            set
            {
                if (value != "door")
                {
                    throw new InvalidCastException("Location is not of type door!");
                }
                base.Location_Type = value;
            }
        }
    }
}
