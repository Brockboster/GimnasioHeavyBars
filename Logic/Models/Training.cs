using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
   public class Training
    {
        //Atributos 
        public int TraningID { get; set; }
        public DateTime Date { get; set; }
        public bool Active { get; set; }
        public string Notes { get; set; }
    }
}
