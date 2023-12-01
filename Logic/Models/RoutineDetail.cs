using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
   public class RoutineDetail
    {
        public int Reps { get; set; }
        public int Sets { get; set; }
        public Exercise MyExercise { get; set; }

        public RoutineDetail()
        {
            MyExercise = new Exercise();
        }


    }
}
