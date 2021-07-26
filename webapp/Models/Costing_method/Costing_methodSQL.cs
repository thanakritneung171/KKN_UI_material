using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KKN_UI.Models.Costing_method
{
    public class Costing_methodSQL
    {
        public int costing_method_id { get; set; }
        public string costing_method_name { get; set; }
    }

    public class Costing_methodSQLlist
    {
        public List<Costing_methodSQL> Costing_methodlist { get; set; }
        //public Costing_methodSQLlist()
        //{
        //    this.Costing_methodlist = new List<Costing_methodSQL>();
        //}
    }
}