﻿//using KKN_UI.Models.Category;
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
        public Boolean active { get; set; }
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
        public DateTime expiry { get; set; }
        //public picture_master picturemodel { get; set; }

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
    public class picture_master
    {
        public int id { get; set; }
        public string picture_path { get; set; }
        public string picture_name { get; set; }
        public string picture_type { get; set; }
        //public string picture_newnamesave { get; set; }
        public string item_no { get; set; }
        public int msg { get; set; }
    }
    public class picturemasterlist
    {
        public List<picture_master> picturelist { get; set; }
        public picturemasterlist()
        {
            this.picturelist = new List<picture_master>();
        }
    }
    public class img
    {
        public string pathimg { get; set; }
        public string nameimg { get; set; }
        public string typeimg { get; set; }
        public string idimg { get; set; }
    }

    public class imgcontrollist
    {
        public List<img> imglist { get; set; }
        public imgcontrollist()
        {
            this.imglist = new List<img>();
        }
    }
}