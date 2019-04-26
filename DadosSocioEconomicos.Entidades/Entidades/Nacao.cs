using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DadosSocioEconomicos.Entidades.Entidades
{
    public class Nacao
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [Display(Name = "População")]
        public decimal Populacao { get; set; }

        [Display(Name = "Produto Interno Bruto - em US$")]
        public decimal PIB { get; set; }

        [Display(Name = "Índice de Desenvolvimento Humano")]
        public decimal IDH { get; set; }
    }
}
