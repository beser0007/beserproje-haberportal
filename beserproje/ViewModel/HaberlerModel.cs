using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace beserproje.ViewModel
{
    public class HaberlerModel
    {
        public int HaberId { get; set; }
        public string HaberBaslik { get; set; }
        public string HaberIcerik { get; set; }
        public System.DateTime HaberTarih { get; set; }
        public string HaberFoto { get; set; }
        public int HaberUyeId { get; set; }
        public int HaberKatId { get; set; }
    }
}