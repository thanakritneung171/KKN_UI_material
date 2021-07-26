using KKN_UI.Models;
using KKN_UI.Models.Group;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KKN_UI.material.group
{
    public class GroupDao
    {
        private const string READ       = "group_itemRead";
        private const string CREATE     = "group_itemCreate";
        private const string DELETE     = "group_itemDelete";
        private const string UPDATE     = "group_itemUpdate";
                                           
        private const string READ_BYID  = "group_itemRead_Byid";




        public GroupSQL maplistgroup(SqlDataReader rdr)
        {
            var resultgroup = new GroupSQL();
            resultgroup.group_id = Convert.ToInt32(rdr["group_id"]);
            resultgroup.group_name = rdr["group_name"].ToString();

            return resultgroup;
        }

    }
}