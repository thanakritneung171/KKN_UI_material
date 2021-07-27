using KKN_UI.Models.Material_acc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KKN_UI.material.Material_acc
{
    public class Material_accDao
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

        private const string READ       = "material_accRead";
        private const string CREATE     = "material_accCreate";
        private const string DELETE     = "material_accDelete";
        private const string UPDATE     = "material_accUpdate";
                                           
        private const string READ_BYID  = "material_accRead_Byid";

        public Material_accSQLlist Getdata()
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ,conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    Material_accSQLlist result = new Material_accSQLlist();

                    using (var rdr = cmd.ExecuteReader())
                    {
                       while(rdr.Read())
                        {
                            result.Material_acclist.Add(maplistmaterialacc(rdr));
                        }
                    }
                    return result;
                }
            }
        }

        public Material_accSQL GetdataByid(int id)
        {
            using (var conn =OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ_BYID,conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@material_acc_id", id);
                    Material_accSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result = maplistmaterialacc(rdr);
                        }
                    }
                    return result;
                }
            }
        }

        public Material_accSQL InsertMaterial_acc(Material_accSQL material_accobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CREATE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@group_id", groupobject.group_id);
                    cmd.Parameters.AddWithValue("@material_acc_name", material_accobject.material_acc_name);

                    Material_accSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = maplistmaterialacc(rdr);
                        }
                    }
                    return result;
                }
            }
        }

        public Material_accSQL UpdateMaterial_acc(Material_accSQL material_accobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CREATE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@material_acc_id", material_accobject.material_acc_id);
                    cmd.Parameters.AddWithValue("@material_acc_name", material_accobject.material_acc_name);

                    Material_accSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = maplistmaterialacc(rdr);
                        }
                    }
                    return result;
                }
            }
        }

        public Material_accSQL DeleteMaterial_acc(Material_accSQL material_accobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(DELETE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@material_acc_id", material_accobject.material_acc_id);
                    cmd.Parameters.AddWithValue("@material_acc_name", material_accobject.material_acc_name);

                    Material_accSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = maplistmaterialacc(rdr);
                        }
                    }
                    return result;
                }
            }
        }


        public Material_accSQL maplistmaterialacc(SqlDataReader rdr)
        {
            var resultmacc = new Material_accSQL();
            resultmacc.material_acc_id = Convert.ToInt32(rdr["material_acc_id"]);
            resultmacc.material_acc_name = rdr["material_acc_name"].ToString();

            return resultmacc;
        }
    }
}