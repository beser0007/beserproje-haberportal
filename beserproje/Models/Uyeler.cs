//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace beserproje.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Uyeler
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Uyeler()
        {
            this.Haberler = new HashSet<Haberler>();
        }
    
        public int UyeID { get; set; }
        public string UyeAdSoyad { get; set; }
        public string UyeMail { get; set; }
        public int UyeYas { get; set; }
        public string UyeParola { get; set; }
        public int UyeYetki { get; set; }
        public System.DateTime UyeTarih { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Haberler> Haberler { get; set; }
    }
}
