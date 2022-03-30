using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XYZProject.Models
{
    public class Endereco
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0} obrigatório.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage ="{0} obrigatório.")]
        public int Numero { get; set;}
                
        public string Complemento { get; set; }

        [Required(ErrorMessage = "{0} obrigatório.")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "{0} obrigatório.")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "{0} obrigatório.")]
        [StringLength(2)]
        public string UF { get; set; }




    }
}
