//using KKN_UI.Models.Category;
using KKN_UI.material.group;
using KKN_UI.Models;
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
        private const string READVIEW       = "categoryReadView";
        private const string CREATE     = "categoryCreate";
        private const string DELETE     = "categoryDelete";
        private const string UPDATE     = "categoryUpdate";

        private const string READ_BYID  = "categoryRead_Byid";
        private const string READ_BYGROUPID  = "categoryRead_ByGroupid";
        private const string CHECKCATEGORYNEW = "CheckCategoryNew";

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

        public CategorySQLlist GetdataView()
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READVIEW, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    CategorySQLlist result = new CategorySQLlist();
                    //var result = new List<GroupSQL>()
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Categorylist.Add(mapviewcategory(rdr));
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

        public CategorySQLlist GetdataCategoryGourpByid(int id)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ_BYGROUPID, conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@group_id", id);
                    CategorySQLlist result = new CategorySQLlist();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Categorylist.Add(maplistcategory(rdr));
                        }
                                                                                                                                                                                                                                                                                                                                                                                            }
                    return (result);
                }

            }

        }

        public /*MaterialSQL*/ Boolean CheckCategoryNew(CategorySQL categoryobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CHECKCATEGORYNEW, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@category_id", categoryobject.category_id);
                    cmd.Parameters.AddWithValue("@group_id", categoryobject.group_id);
                    cmd.Parameters.AddWithValue("@category_name", categoryobject.category_name);

                    CategorySQL result = new CategorySQL();
                    //MaterialSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    //return result;
                }
            }
        }

        public CategorySQL InsertCategory(CategorySQL categoryobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CREATE,conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@category_id", categoryobject.category_id);
                    cmd.Parameters.AddWithValue("@group_id", categoryobject.group_id);
                    cmd.Parameters.AddWithValue("@category_name", categoryobject.category_name);

                    CategorySQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if(rdr.HasRows)
                        {
                            result = maplistcategory(rdr);
                        }
                    }
                    return result;
                }
            }
        }

        public CategorySQL UpdateCategory(CategorySQL categoryobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE,conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@category_id", categoryobject.category_id);
                    cmd.Parameters.AddWithValue("@group_id", categoryobject.group_id);
                    cmd.Parameters.AddWithValue("@category_name", categoryobject.category_name);

                    CategorySQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if(rdr.HasRows)
                        {
                            result = maplistcategory(rdr);
                        }
                    }
                    return result;
                }
            }
        }

        public CategorySQL DeleteCategory(CategorySQL categoryobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(DELETE,conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@category_id", categoryobject.category_id);

                    CategorySQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if(rdr.HasRows)
                        {
                            result = maplistcategory(rdr);
                        }
                    }
                    return result;
                }
            }
        }


        public CategorySQL maplistcategory(SqlDataReader rdr)
        {
            var resultcategory = new CategorySQL();
            resultcategory.category_id = Convert.ToInt32(rdr["category_id"]);
            resultcategory.group_id = Convert.ToInt32(rdr["group_id"]);
            resultcategory.category_name = rdr["category_name"].ToString();

            //resultcategory.GroupModel = new GroupDao().maplistgroupDao(rdr);
            return resultcategory;
        }

        public CategorySQL mapviewlistcategory(SqlDataReader rdr)
        {
            var resultcategory = new CategorySQL();
            resultcategory.category_id = Convert.ToInt32(rdr["category_category_id"]);
            resultcategory.group_id = Convert.ToInt32(rdr["group_time_group_id"]);
            resultcategory.category_name = rdr["category_category_name"].ToString();

            return resultcategory;
        }

        public CategorySQL mapviewcategory(SqlDataReader rdr)
        {
            var resultcategory = new CategorySQL();
            resultcategory.category_id = Convert.ToInt32(rdr["category_category_id"]);
            resultcategory.group_id = Convert.ToInt32(rdr["group_time_group_id"]);
            resultcategory.category_name = rdr["category_category_name"].ToString();

            resultcategory.GroupModel =  GroupDao.mapviewlistgroupDao(rdr);
            return resultcategory;
        }
    }
}

