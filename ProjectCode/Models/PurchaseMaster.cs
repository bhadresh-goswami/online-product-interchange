//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjectCode.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseMaster
    {
        public int PId { get; set; }
        public System.DateTime PurchasedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string LandMark { get; set; }
        public int refCityId { get; set; }
        public string ContactPerson { get; set; }
    
        public virtual CityMaster CityMaster { get; set; }
    }
}
