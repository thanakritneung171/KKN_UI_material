//using KKN_UI.Models.Category;
//using KKN_UI.Models.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KKN_UI.Models.Material
{
    public class MaterialSQL
    {
        public int item_id { get; set; }
        public string item_no { get; set; }
        public string item_name { get; set; }
        public int group_id { get; set; }
        public int category_id { get; set; }
        public string description { get; set; }
        public Boolean status { get; set; }
        public int material_acc_id { get; set; }
        public int costing_method_id { get; set; }
        public Boolean stock_count { get; set; }
        public Boolean overdraw_stock { get; set; }
        public string picture_path { get; set; }
        public string brand { get; set; }
        public string version { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public int uom_in { get; set; }
        public decimal qty_in { get; set; }
        public int uom_stock { get; set; }
        public decimal qty_stock { get; set; }
        public GroupSQL GroupSQLModel { get; set; }
        public CategorySQL CategorySQLModel { get; set; }
        public int msg { get; set; }

    }
    public class MaterialSearch : MaterialSQL
    {
        public int Count { get; set; }
    }

    public class MaterialSQLlist
    {
        public List<MaterialSQL> materiallist { get; set; }
        public MaterialSQLlist()
        {
            this.materiallist = new List<MaterialSQL>();
        }
    }
}