//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HR.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmpCards
    {
        public int ID { get; set; }
        public Nullable<int> CardTypeID { get; set; }
        public Nullable<int> EmpID { get; set; }
        public string IdentityNumber { get; set; }
        public string Note { get; set; }
    
        public virtual Def_CardType Def_CardType { get; set; }
        public virtual Employee Employee { get; set; }
    }
}