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
    
    public partial class EmpBorrow
    {
        public int ID { get; set; }
        public Nullable<int> EmpID { get; set; }
        public Nullable<System.DateTime> Dateorder { get; set; }
        public Nullable<System.DateTime> payoffDate { get; set; }
        public Nullable<decimal> BorrowVal { get; set; }
        public Nullable<decimal> InstallmentVal { get; set; }
        public Nullable<float> Installmentscount { get; set; }
        public Nullable<decimal> payoffValue { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
