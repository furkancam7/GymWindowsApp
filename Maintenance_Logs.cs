//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sali
{
    using System;
    using System.Collections.Generic;
    
    public partial class Maintenance_Logs
    {
        public int maintenance_id { get; set; }
        public int staff_id { get; set; }
        public int equipment_id { get; set; }
        public Nullable<System.DateTime> maintenance_date { get; set; }
        public string notes { get; set; }
    
        public virtual Equipment Equipment { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
