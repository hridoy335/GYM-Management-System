//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GYM_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientBill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientBill()
        {
            this.ClientBillTransections = new HashSet<ClientBillTransection>();
        }
    
        public int ClientBillId { get; set; }
        public int ClientId { get; set; }
        public System.DateTime BillMonth { get; set; }
        public int BillAmount { get; set; }
        public bool BillStatus { get; set; }
        public Nullable<int> DueStatus { get; set; }
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientBillTransection> ClientBillTransections { get; set; }
    }
}
