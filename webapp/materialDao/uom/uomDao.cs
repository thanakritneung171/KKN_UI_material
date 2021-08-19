using KKN_UI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Data;
using System.IO;
using KKN_UI.Models.Uom;

namespace KKN_UI.material.uom
{
    public class uomDao 
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

        private const string READ       = "uomRead";
        private const string CREATE     = "uomCreate";
        private const string UPDATE     = "uomUpdate";
        private const string DELETE     = "uomDelete";

        private const string READ_BYID  = "uomRead_Byid";

        public IEnumerable<UomSQL> uomlistdata
        {
            get
            {
               
                List<UomSQL> uomlist = new List<UomSQL>();
                using (var conn = OpenDbConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(READ,conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var rdr = cmd.ExecuteReader())
                        {
                            while(rdr.Read())
                            {
                                //UomSQL uomdata = new UomSQL();
                                //uomdata.uom_id = Convert.ToInt32(rdr["uom_id"]);
                                //uomdata.uom_name = rdr["uom_name"].ToString();
                                uomlist.Add(maplistuom(rdr));
                            }
                        }
                    }
                }
                return uomlist;
            }
        }

        public UomSQL GetdataByid(int id)
        {
            UomSQL uomlist = new UomSQL();
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ_BYID,conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Uom_id", id);

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while(rdr.Read())
                        {
                            uomlist = maplistuom(rdr);
                        }
                    }
                }
            }
            return uomlist;
        }



        public UomSQL InsertUom(UomSQL uomobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CREATE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@uom_id", uomobject.uom_id);
                    cmd.Parameters.AddWithValue("@uom_name", uomobject.uom_name);

                    UomSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = maplistuom(rdr);
                        }
                    }
                    return result;
                }
            }
        }

        public UomSQL UpdateUom(UomSQL uomobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uom_id", uomobject.uom_id);
                    cmd.Parameters.AddWithValue("@uom_name", uomobject.uom_name);

                    UomSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = maplistuom(rdr);
                        }
                    }
                    return result;
                }
            }
        }

        public UomSQL DeleteUom(UomSQL uomobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(DELETE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uom_id", uomobject.uom_id);
                    cmd.Parameters.AddWithValue("@uom_name", uomobject.uom_name);

                    UomSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = maplistuom(rdr);
                        }
                    }
                    return result;
                }
            }
        }


        public UomSQL maplistuom(SqlDataReader rdr)
        {
            var resultuom = new UomSQL();
            resultuom.uom_id = Convert.ToInt32(rdr["uom_id"]);
            resultuom.uom_name = rdr["uom_name"].ToString();

            return resultuom;
        }
    }
}