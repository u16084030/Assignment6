//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AngularReport_Activity6.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class LocalFlight
    {
        public int FlightNumber { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
    
        public virtual Flight Flight { get; set; }
    }
}
