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
    
    public partial class Def_ElawaTypes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Def_ElawaTypes()
        {
            this.EmpElawa = new HashSet<EmpElawa>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Repeated { get; set; }
        public Nullable<float> SalaryPercent { get; set; }
        public Nullable<decimal> Value { get; set; }
        public Nullable<bool> Included { get; set; }
        public Nullable<bool> Taxed { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpElawa> EmpElawa { get; set; }
    }
}
