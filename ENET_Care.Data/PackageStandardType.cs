//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ENET_Care.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class PackageStandardType
    {
        public PackageStandardType()
        {
            this.Packages = new HashSet<Package>();
        }
    
        public int Id { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> Cost { get; set; }
        public Nullable<bool> IsTempSensitive { get; set; }
        public Nullable<int> ShelfLife { get; set; }
    
        public virtual ICollection<Package> Packages { get; set; }
    }
}