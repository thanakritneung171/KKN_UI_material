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

        public MaterialSQL GetdataByid(string id)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ, conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@item_id", id);
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
    }
}