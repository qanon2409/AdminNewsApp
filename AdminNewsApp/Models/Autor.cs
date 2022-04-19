using System;
using System.Collections.Generic;

namespace AdminNewsApp.Models
{
    public partial class Autor
    {
        public Autor()
        {
            Idnot2s = new HashSet<Noticia>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Noticia> Idnot2s { get; set; }
    }
}
