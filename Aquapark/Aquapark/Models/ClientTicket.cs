//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Aquapark.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientTicket
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> EntriesLeft { get; set; }
        public bool WasPaid { get; set; }
        public System.DateTime ActivationDate { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public int IdTicketInPriceList { get; set; }
        public Nullable<int> IdWristband { get; set; }
    
        public virtual TicketInPriceList TicketInPriceList { get; set; }
        public virtual Wristband Wristband { get; set; }
    }
}
