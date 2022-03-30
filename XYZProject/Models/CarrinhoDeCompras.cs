using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XYZProject.Models
{
    public class CarrinhoDeCompras
    {
        List<Item> itens { get; set; }

        private int IDCliente { get; set; }

        public double TotalGeral { get; set; }
    }
}
