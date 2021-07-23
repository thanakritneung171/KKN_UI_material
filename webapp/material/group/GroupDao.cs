using KKN_UI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KKN_UI.material.group
{
    public class GroupDao
    {
        private const string Read = "";
        private const string Creat = "";
        private const string Update = "";
        private const string Delete = "";




        public GroupSQL maplistgroup(SqlDataReader rdr)
        {
            var resultgroup = new GroupSQL();
            resultgroup.group_id = Convert.ToInt32(rdr["group_id"]);
            resultgroup.group_name = rdr["group_name"].ToString();

            return resultgroup;
        }

    }
}