using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Enciklopedija.Models
{
    [MetadataType(typeof(ZanimljivostMetadata))]
    public partial class Zanimljivost
    {
        
    }

    public partial class ZanimljivostMetadata
    {
        [Editable(false)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Obavezno unesite naslov Zanimljivosti.")]
        public string Naslov { get; set; }
        public string Opis { get; set; }
        public string Url { get; set; }

        [Display(Name = "Video-URL")]
        public string VideoUrl { get; set; }

        [Range(typeof(int), "0", "1200", ErrorMessage = "Smijete unijeti broj od 0 do 1000")]
        [Display(Name = "Start")]
        public Nullable<int> VideoStart { get; set; }

        [Range(typeof(int), "0", "1200", ErrorMessage = "Smijete unijeti broj od 0 do 1000")]
        [Display(Name = "End")]
        public Nullable<int> VideoEnd { get; set; }
        
        [Display(Name = "Žanr")]
        public Nullable<int> ZanrID { get; set; }
    

    }
}