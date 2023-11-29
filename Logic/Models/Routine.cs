using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Routine
    {
        public int RoutineID { get; set; }
        public string RoutineName { get; set; }



        public int RoutineDuration { get; set; }

        public string Description { get; set; }
        public Difficulty Difficulty { get; set; }


    }
}
