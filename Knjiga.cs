using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBooks
{
    class Knjiga
    {
        public int KnjigaId { get; set; }
        public string Naslov { get; set; }
        public string Autor { get; set; }
        public string Zanr { get; set; }


        public override string ToString()
        {
            return Naslov + " " + Autor;
        }
    }
}
