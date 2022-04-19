using System;
using System.Collections.Generic;

namespace AdminNewsApp.Models
{
    public partial class Pai
    {
        public Pai()
        {
            Fuentes = new HashSet<Fuente>();
            Noticia = new HashSet<Noticia>();
        }

        public int Id { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Fuente> Fuentes { get; set; }
        public virtual ICollection<Noticia> Noticia { get; set; }
    }
}
