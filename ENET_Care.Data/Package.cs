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
    
    public partial class Package
    {
        public Package()
        {
            this.PackageStatus = new HashSet<PackageStatus>();
        }
    
        public int PackageId { get; set; }
        public Nullable<int> PackageStandardTypeId { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<int> Quantity { get; set; }
    
        public virtual PackageStandardType PackageStandardType { get; set; }
        public virtual ICollection<PackageStatus> PackageStatus { get; set; }
    }
}
