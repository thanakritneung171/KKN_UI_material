using KKN_UI.Models.Category;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KKN_UI.material.category
{
    public class CategoryDao
    {
        private SqlConnection conn;
        private SqlTransaction tran;

        protected SqlConnection OpenDbConnection()
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["DBContext"].ConnectionString;
            conn = new SqlConnection(connString);
            conn.Open();
            return conn;
        }

        private const string READ       = "categoryRead";
        private const string CREATE     = "categoryCreate";
        private const string DELETE     = "categoryDelete";
        private const string UPDATE     = "categoryUpdate";

        private const string READ_BYID  = "categoryRead_Byid";


        public CategorySQLlist Getdata()
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    CategorySQLlist result = new CategorySQLlist();
                    //var result = new List<GroupSQL>()
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Categorylist.Add(maplistcategory(rdr));
                        }
                    }
                    return result;
                }

            }

        }

        public CategorySQL GetdataByid(int id)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ_BYID, conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@category_id", id);
                    CategorySQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result = maplistcategory(rdr);
                        }
                    }
                    return (result);
                }

            }

        }

        public CategorySQL maplistcategory(SqlDataReader rdr)
        {
            var resultcategory = new CategorySQL();
            resultcategory.category_id = Convert.ToInt32(rdr["category_id"]);
            resultcategory.group_id = Convert.ToInt32(rdr["group_id"]);
            resultcategory.category_name = rdr["category_name"].ToString();

            return resultcategory;
        }
    }
}