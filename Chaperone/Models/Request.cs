//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Chaperone.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Request
    {
        public int Id { get; set; }
        public int TouristId { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int GuideId { get; set; }
        public string RequestState { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
