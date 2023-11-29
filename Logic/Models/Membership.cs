﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Models
{
    public class Membership
    {
        #region Atributos
        //Atributos 
        public int MerbershipID { get; set; }
        public string MembershipName { get; set; }
        public bool Active { get; set; }
        public decimal price { get; set; }
        #endregion

        

        #region Lista de membresias 

        //Lista de Membresias 
        public DataTable List()
        {
            DataTable R = new DataTable();
            Services.Conection MiCnn = new Services.Conection();
            R = MiCnn.EjecutarSELECT("MembershipList");
            return R;
        }

        #endregion


    }
}
