using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KKN_UI.Models
//namespace KKN_UI.Models.Category
{
    public class CategorySQL
    {
        public int category_id { get; set; }
        public int group_id { get; set; }
        public string category_name { get; set; }
        public bool active { get; set; }
        public GroupSQL GroupModel { get; set; }
    }
    //public class CategoryGroupSQL
    //{
    //    public List<CategorySQL> CategoryGlist { get; set; }
    //    public List<GroupSQL> GroupClist { get; set; }
    //}

    public class CategorySQLlist
    {
        public List<CategorySQL> Categorylist { get; set; }
        public CategorySQLlist()
        {
            this.Categorylist = new List<CategorySQL>();
        }
    }
    public class Rowcategory
    {
        public int row { get; set; }
        public string check { get; set; }
    }
}