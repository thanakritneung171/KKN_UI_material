using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KKN_UI.Models.Material_acc
{
    public class Material_accSQL
    {
        public int material_acc_id { get; set; }
        public string material_acc_name { get; set; }
    }

    public class Material_accSQLlist
    {
        public List<Material_accSQL> Material_acclist { get; set; }
        public Material_accSQLlist()
        {
            this.Material_acclist = new List<Material_accSQL>();
        }
    }
}