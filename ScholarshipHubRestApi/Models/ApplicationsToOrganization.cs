//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScholarshipHubRestApi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ApplicationsToOrganization
    {
        public int id { get; set; }
        public int StudentId { get; set; }
        public int organizationsOfferID { get; set; }
        public string Motivation { get; set; }
        public string StudentBio { get; set; }
        public Nullable<int> AplicationStatus { get; set; }
        public string ApplicationInformation { get; set; }
    
        public virtual OrganizationOffer OrganizationOffer { get; set; }
        public virtual Student Student { get; set; }
    }
}
