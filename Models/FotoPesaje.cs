//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Parcial2_apps.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    
    public partial class FotoPesaje
    {
        public int idFotoPesaje { get; set; }
        public string ImagenVehiculo { get; set; }
        public int idPesaje { get; set; }

        [JsonIgnore]
        public virtual Pesaje Pesaje { get; set; }
    }
}
