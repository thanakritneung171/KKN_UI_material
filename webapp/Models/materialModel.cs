using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KKN_UI.Models
{
    public class materialModel
    {

        public string item_no { get; set; }
        public string item_name { get; set; }
        public int group_id { get; set; }
        public int category { get; set; }
        public string description { get; set; }
        public Boolean status { get; set; }
        public int material_account { get; set; }
        public int costing_method_material { get; set; }
        public Boolean stock_count { get; set; }
        public Boolean overdraw_stock { get; set; }
        public string picture_path { get; set; }
        public HttpPostedFileBase picture_file { get; set; }
        public string brand { get; set; }
        public string version { get; set; }
        public string color { get; set; }
        public string size { get; set; }
        public string uom_in { get; set; }
        public decimal qty_in { get; set; }
        public string uom_stock { get; set; }
        public decimal qty_stock { get; set; }

        public groupmaterial groupmaterial { get; set; }
        public categorymaterial categorymaterial { get; set; }
        public idname idnamegroup { get; set; }
        
    }

    public class groupmaterial
    {
        public int group_id { get; set; }
        public string group_name { get; set; }
    }

    public class categorymaterial
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
    }
    public class materialmodalview
    {
        public List<groupmaterial> grouplist { get; set; }
        public List<categorymaterial> categorylist { get; set; }
        public materialModel materialdata { get; set; }

    }
    public class materialindex
    {
        public groupmaterial grouplist { get; set; }
        public categorymaterial categorylist { get; set; }
        public List<materialModel> materiallist { get; set; }
    }
    public class idname
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}