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
    
    public partial class Private_Lessons
    {
        public int lesson_id { get; set; }
        public int member_id { get; set; }
        public Nullable<int> staff_id { get; set; }
        public Nullable<System.DateTime> lesson_date { get; set; }
        public string notes { get; set; }
    
        public virtual Member Member { get; set; }
    }
}
