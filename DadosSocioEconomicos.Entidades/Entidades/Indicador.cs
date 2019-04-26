using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DadosSocioEconomicos.Entidades.Entidades
{
    public class Indicador
    {
        [Key]
        public string Dado { get; set; }
    }
}
