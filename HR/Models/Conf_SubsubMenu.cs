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
    
    public partial class Conf_SubsubMenu
    {
        public int ID { get; set; }
        public Nullable<int> SubMenuID { get; set; }
        public string Name { get; set; }
        public string AreaName { get; set; }
        public string ControllerName { get; set; }
        public string PageName { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
    
        public virtual Conf_SubMenu Conf_SubMenu { get; set; }
    }
}