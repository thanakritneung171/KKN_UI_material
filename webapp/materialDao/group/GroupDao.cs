﻿using KKN_UI.Models;
using KKN_UI.Models.Group;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KKN_UI.material.group
{
    public class GroupDao
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

        private const string READ       = "group_itemRead";
        private const string CREATE     = "group_itemCreate";
        private const string DELETE     = "group_itemDelete";
        private const string UPDATE     = "group_itemUpdate";
                                           
        private const string READ_BYID  = "group_itemRead_Byid";


        public GroupSQLlist Getdata()
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ,conn))
                {    
                    cmd.CommandType = CommandType.StoredProcedure;
                    GroupSQLlist result = new GroupSQLlist();
                    //var result = new List<GroupSQL>()
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while(rdr.Read())
                        {
                            result.Grouplist.Add(maplistgroupDao(rdr));
                        }
                    }
                    return result;
                }
                
            }
                
        }

        public GroupSQL GetdataByid(int id)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ_BYID, conn))
                {
                   
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@group_id", id);
                    GroupSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result = maplistgroupDao(rdr);
                        }
                    }
                    return (result);
                }
            }
        }

        public GroupSQL InsertGroup(GroupSQL groupobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CREATE,conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@group_id", groupobject.group_id);
                    cmd.Parameters.AddWithValue("@group_name", groupobject.group_name);

                    GroupSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if(rdr.HasRows)
                        {
                            result = maplistgroupDao(rdr);
                        }
                    }
                    return result;
                }
            }
        }

        public GroupSQL UpdateGroup(GroupSQL groupobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@group_id", groupobject.group_id);
                    cmd.Parameters.AddWithValue("@group_name", groupobject.group_name);

                    GroupSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = maplistgroupDao(rdr);
                        }
                    }
                    return result;
                }
            }
        }

        public GroupSQL DeleteGroup(GroupSQL groupobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(DELETE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@group_id", groupobject.group_id);
                    cmd.Parameters.AddWithValue("@group_name", groupobject.group_name);

                    GroupSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = maplistgroupDao(rdr);
                        }
                    }
                    return result;
                }
            }
        }


        public GroupSQL maplistgroupDao(SqlDataReader rdr)
        {
            var resultgroup = new GroupSQL();
            resultgroup.group_id = Convert.ToInt32(rdr["group_id"]);  
            resultgroup.group_name = rdr["group_name"].ToString();

            return resultgroup;
        }

        public GroupSQL mapviewlistgroupDao(SqlDataReader rdr)
        {
            var resultgroup = new GroupSQL();
            resultgroup.group_id = Convert.ToInt32(rdr["group_time_group_id"]);
            resultgroup.group_name = rdr["group_time_group_name"].ToString();

            return resultgroup;
        }
    }
}