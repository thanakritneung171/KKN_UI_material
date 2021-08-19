using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KKN_UI.Models
//namespace KKN_UI.Models.Group
{
    public class GroupSQL
    {
        public int group_id { get; set; }
        public string group_name { get; set; }
        public bool active { get; set; }
    }

    public class GroupSQLlist
    {
        public List<GroupSQL> Grouplist { get; set; }
        public GroupSQLlist()
        {
            this.Grouplist = new List<GroupSQL>();
        }
    }

    public class Rowgroup
    {
        public int row { get; set; }
        public string check { get; set; }
    }

}