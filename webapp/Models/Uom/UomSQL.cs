using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KKN_UI.Models.Uom
{
    public class UomSQL
    {
        public int uom_id { get; set; }
        public string uom_name { get; set; }
        public bool active { get; set; }
    }

    public class UomSQLlist
    {
        public List<UomSQL> Uomlist { get; set; }
        //public UomSQLlist()
        //{
        //    this.Uomlist = new List<UomSQL>();
        //}
    }
    public class RowUom
    {
        public int row { get; set; }
        public string check { get; set; }
    }
}