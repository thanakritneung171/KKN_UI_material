using KKN_UI.Models.Costing_method;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KKN_UI.material.Costing_method
{
    public class Costing_methodDao
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

        private const string READ       = "costing_methodRead";
        private const string CREATE     = "costing_methodCreate";
        private const string DELETE     = "costing_methodDelete";
        private const string UPDATE     = "costing_methodUpdate";
                                           
        private const string READ_BYID  = "costing_methodRead_Byid";


        public Costing_methodSQLlist Getdata()
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    Costing_methodSQLlist result = new Costing_methodSQLlist();
                    //var result = new List<Costing_methodSQL>()
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Costing_methodlist.Add(maplistcosting(rdr));
                        }
                    }
                    return result;
                }
            }
        }

        public Costing_methodSQL GetdataByid(int id)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ_BYID, conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@costing_method_id", id);
                    Costing_methodSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result = maplistcosting(rdr);
                        }
                    }
                    return (result);
                }
            }
        }


        public Costing_methodSQL InsertCosting_method(Costing_methodSQL costing_methodobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CREATE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@costing_method_id", costing_methodobject.costing_method_id);
                    cmd.Parameters.AddWithValue("@costing_method_name", costing_methodobject.costing_method_name);

                    Costing_methodSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = maplistcosting(rdr);
                        }
                    }
                    return result;
                }
            }
        }

        public Costing_methodSQL UpdateCosting_method(Costing_methodSQL costing_methodobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@costing_method_id", costing_methodobject.costing_method_id);
                    cmd.Parameters.AddWithValue("@costing_method_name", costing_methodobject.costing_method_name);

                    Costing_methodSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = maplistcosting(rdr);
                        }
                    }
                    return result;
                }
            }
        }

        public Costing_methodSQL DeleteCosting_method(Costing_methodSQL costing_methodobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(DELETE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@costing_method_id", costing_methodobject.costing_method_id);
                    cmd.Parameters.AddWithValue("@costing_method_name", costing_methodobject.costing_method_name);

                    Costing_methodSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = maplistcosting(rdr);
                        }
                    }
                    return result;
                }
            }
        }



        public Costing_methodSQL maplistcosting(SqlDataReader rdr)
        {
            var resultcosting = new Costing_methodSQL();
            resultcosting.costing_method_id = Convert.ToInt32(rdr["costing_method_id"]);
            resultcosting.costing_method_name = rdr["costing_method_name"].ToString();

            return resultcosting;
        } 
    }
}