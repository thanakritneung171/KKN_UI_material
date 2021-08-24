//using KKN_UI.Models.Category;
using KKN_UI.Models.Costing_method;
//using KKN_UI.Models.Group;
using KKN_UI.Models.Material;
using KKN_UI.Models.Material_acc;
using KKN_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KKN_UI.Models
{
    //public class MaterialSQL
    //{
    //    public string item_no { get; set; }
    //    public int item_id { get; set; }
    //    public string item_name { get; set; }
    //    public int group_id { get; set; }
    //    public int category_id { get; set; }
    //    public string description { get; set; }
    //    public Boolean status { get; set; }
    //    public int material_acc_id { get; set; }
    //    public int costing_method_id { get; set; }
    //    public Boolean stock_count { get; set; }
    //    public Boolean overdraw_stock { get; set; }
    //    public string picture_path { get; set; }
    //    public string brand { get; set; }
    //    public string version { get; set; }
    //    public string color { get; set; }
    //    public string size { get; set; }
    //    public int uom_in { get; set; }
    //    public decimal qty_in { get; set; }
    //    public int uom_stock { get; set; }
    //    public decimal qty_stock { get; set; }
    //    public GroupSQL GroupSQLModel { get; set; }
    //    public CategorySQL CategorySQLModel { get; set; }
    //}

    //public class GroupSQL
    //{
    //    public int group_id { get; set; }
    //    public string group_name { get; set; }
    //}

    //public class CategorySQL
    //{
    //    public int category_id { get; set; }
    //    public int group_id { get; set; }
    //    public string category_name { get; set; }
    //}

    //public class Material_accSQL
    //{
    //    public int material_acc_id { get; set; }
    //    public string material_acc_name { get; set; }
    //}

    //public class Costing_methodSQL
    //{
    //    public int costing_method_id { get; set; }
    //    public string costing_method_name { get; set; }
    //}
    //public class uomSQL
    //{
    //    public int uom_id { get; set; }
    //    public string uom_name { get; set; }
    //}

    public class MaterialSQLindex
    {
        public List<UomSQL> UomSQL_list { get; set; }
        public List<Costing_methodSQL> Costing_methodSQL_list { get; set; }
        public List<Material_accSQL> Material_accSQLlist { get; set; }
        public List<GroupSQL> GroupSQLlist { get; set; }
        public List<CategorySQL> CategorySQLlist { get; set; }
        public List<MaterialSQL> MaterialSQLlist { get; set; }
        public MaterialSQL MaterialSQLdataselect { get; set; }
        public CategorySQL CategorySQL { get; set; }
    }

}