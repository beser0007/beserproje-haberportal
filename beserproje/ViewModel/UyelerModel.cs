using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace beserproje.ViewModel
{
    public class UyelerModel
    {
        public int UyeID { get; set; }
        public string UyeAdSoyad { get; set; }
        public string UyeMail { get; set; }
        public int UyeYas { get; set; }
        public string UyeParola { get; set; }
        public int UyeYetki { get; set; }
        public System.DateTime UyeTarih { get; set; }
    }
}