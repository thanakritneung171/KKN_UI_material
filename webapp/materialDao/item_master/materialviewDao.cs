using KKN_UI.Models;
using KKN_UI.Models.Material;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KKN_UI.materialDao.item_master
{
    public class materialviewDao
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
        private const string READ = "materialview_edit";
        private const string READSEARCH = "Searchmaterial";
        public MaterialSQL GetdataByid(string id)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ, conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@item_no", id);
                    MaterialSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result = new item_masterDao().mapView(rdr);
                        }
                    }
                    return (result);
                }
            }
        }

        public List<MaterialSQL> GetdataSearch(SearchItem search)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READSEARCH, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@group_id", search.group_id);
                    cmd.Parameters.AddWithValue("@category_id", search.category_id);
                    cmd.Parameters.AddWithValue("@text", search.text);
                    List<MaterialSQL> result = new List<MaterialSQL>();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            if (rdr.HasRows)
                            {
                            result.Add(new item_masterDao().mapViewSearch(rdr));
                            }

                        }
                    }
                    return result;
                }
            }
        }


    }
}