using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DadosSocioEconomicos.Entidades.Entidades
{
    public class Comparativo
    {
        [Key]
        public string PrimeiraNacao { get; set; }

        public string SegundaNacao { get; set; }

        public string Indicador { get; set; }
    }
}
