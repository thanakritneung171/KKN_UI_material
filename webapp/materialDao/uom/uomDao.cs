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

        private const string READ_BYID   = "uomRead";
        private const string CREATE = "uomCreate";
        private const string UPDATE = "uomUpdate";
        private const string DELETE = "uomDelete";

        private const string READ = "uomRead";

        public IEnumerable<uomSQL> uomlistdata
        {
            get
            {
               
                List<uomSQL> uomlist = new List<uomSQL>();
                using (var conn = OpenDbConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(READ,conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (var rdr = cmd.ExecuteReader())
                        {
                            while(rdr.Read())
                            {
                                uomSQL uomdata = new uomSQL();
                                uomdata.uom_id = Convert.ToInt32(rdr["uom_id"]);
                                uomdata.uom_name = rdr["uom_name"].ToString();
                                uomlist.Add(uomdata);
                            }
                        }
                    }
                }
                return uomlist;
            }
        }


    //public MaterialSQL GetdataById(int uomID)
    //{
    //    SqlCommand cmd = new SqlCommand(READ, conn);
    //    cmd.AddIntParameter("READ", uomID);
    //    cmd.CommandType = System.Data.CommandType.StoredProcedure;

    //    MaterialSQL result = null;
    //    using (var rdr = cmd.ExecuteReader())
    //    {
    //        rdr.Read();
    //        if (rdr.HasRows)
    //        {
    //            result = mapView(rdr);
    //            if(!rdr.IsDBNull(rdr.GetOrdinal("")))
    //            {
    //                result.
    //            }
    //        }
    //    }

    //        return result;
    //}


}
}