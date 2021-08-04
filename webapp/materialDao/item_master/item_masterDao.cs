using KKN_UI.material.category;
using KKN_UI.material.group;
using KKN_UI.Models.Material;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KKN_UI.materialDao.item_master
{
    public class item_masterDao
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

        private const string READ = "item_masterRead";
        private const string CREATE = "item_masterCreate";
        private const string DELETE = "item_masterDelete";
        private const string UPDATE = "item_masterUpdate";

        private const string READ_BYID = "item_masterRead_Byid";

        private const string CHECKITEMNO = "CheckItemNo";
        private const string CHECKITEMNAME = "CheckItemName";
        private const string CHECKDETAILITEM = "CheckDetailItem";

        public MaterialSQLlist Getdata()
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    MaterialSQLlist result = new MaterialSQLlist();
                    //var result = new List<MaterialSQL>()
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.materiallist.Add(mapView(rdr));
                        }
                    }
                    return result;
                }
            }
        }

        public MaterialSQL GetdataByid(int id)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(READ_BYID, conn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@item_id", id);
                    MaterialSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result = mapView(rdr);
                        }
                    }
                    return (result);
                }
            }
        }


        public MaterialSQL CheckItemNo(MaterialSQL materialobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CHECKITEMNO, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@item_no", materialobject.item_no);

                    MaterialSQL result = new MaterialSQL();
                    //MaterialSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result.msg = 2;
                            return result;
                        }
                        else
                        {
                            result = CheckItemName(materialobject);
                        }
                    }
                    return result;
                }
            }
        }

        public MaterialSQL CheckItemName(MaterialSQL materialobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CHECKITEMNAME, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@item_name", materialobject.item_name);

                    MaterialSQL result = new MaterialSQL();
                    //MaterialSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = CheckDetialItem(materialobject);
                        }
                        else
                        {
                            result.msg = 1;
                            result = InsertItem_master(materialobject);
                        }
                    }
                    return result;
                }
            }
        }

        public MaterialSQL CheckDetialItem(MaterialSQL materialobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CHECKDETAILITEM, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@item_name", materialobject.item_name);
                    cmd.Parameters.AddWithValue("@brand", (object)materialobject.brand ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@version", (object)materialobject.version ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@color", (object)materialobject.color ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@size", (object)materialobject.size ?? DBNull.Value);

                    MaterialSQL result = new MaterialSQL();
                    //MaterialSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result.msg = 3;
                            return result;
                        }
                        else
                        {
                            result.msg = 1;
                            result = InsertItem_master(materialobject);
                        }
                    }
                    return result;
                }
            }
        }

        public MaterialSQL InsertItem_master(MaterialSQL materialobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(CREATE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@item_id           ", materialobject.item_id);
                    cmd.Parameters.AddWithValue("@item_no           ", materialobject.item_no);
                    cmd.Parameters.AddWithValue("@item_name         ", materialobject.item_name);
                    cmd.Parameters.AddWithValue("@group_id          ", materialobject.group_id);
                    cmd.Parameters.AddWithValue("@category_id       ", materialobject.category_id);
                    cmd.Parameters.AddWithValue("@material_acc_id   ", materialobject.material_acc_id);
                    cmd.Parameters.AddWithValue("@costing_method_id ", materialobject.costing_method_id);
                    cmd.Parameters.AddWithValue("@description       ", (object)materialobject.description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@status            ", materialobject.status);
                    cmd.Parameters.AddWithValue("@stock_count       ", materialobject.stock_count);
                    cmd.Parameters.AddWithValue("@overdraw_stock    ", materialobject.overdraw_stock);
                    cmd.Parameters.AddWithValue("@picture_path      ", materialobject.picture_path);
                    cmd.Parameters.AddWithValue("@brand", (object)materialobject.brand ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@version", (object)materialobject.version ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@color", (object)materialobject.color ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@size", (object)materialobject.size ?? DBNull.Value);
                    //cmd.Parameters.AddWithValue("@size", materialobject.size);
                    cmd.Parameters.AddWithValue("@uom_in            ", materialobject.uom_in);
                    cmd.Parameters.AddWithValue("@uom_stock         ", materialobject.uom_stock);
                    cmd.Parameters.AddWithValue("@qty_in            ", materialobject.qty_in);
                    cmd.Parameters.AddWithValue("@qty_stock         ", materialobject.qty_stock);

                    MaterialSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            //var t = Convert.ToInt32(rdr["msg"]);
                            //if (Convert.ToInt32(rdr["msg"]) == 1)
                            //{
                            result = mapinsert(rdr);
                            //}
                        }
                    }
                    return result;
                }
            }
        }

        public MaterialSQL UpdateItem_master(MaterialSQL materialobject)
        {
            using (var conn = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(UPDATE, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@item_id           ", materialobject.item_id);
                    //cmd.Parameters.AddWithValue("@item_no           ", materialobject.item_no);
                    cmd.Parameters.AddWithValue("@item_name         ", materialobject.item_name);
                    cmd.Parameters.AddWithValue("@group_id          ", materialobject.group_id);
                    cmd.Parameters.AddWithValue("@category_id       ", materialobject.category_id);
                    cmd.Parameters.AddWithValue("@material_acc_id   ", materialobject.material_acc_id);
                    cmd.Parameters.AddWithValue("@costing_method_id ", materialobject.costing_method_id);
                    cmd.Parameters.AddWithValue("@description       ", materialobject.description);
                    cmd.Parameters.AddWithValue("@status            ", materialobject.status);
                    cmd.Parameters.AddWithValue("@stock_count       ", materialobject.stock_count);
                    cmd.Parameters.AddWithValue("@overdraw_stock    ", materialobject.overdraw_stock);
                    //cmd.Parameters.AddWithValue("@picture_path      ",materialobject.picture_path     );
                    cmd.Parameters.AddWithValue("@brand             ", materialobject.brand);
                    cmd.Parameters.AddWithValue("@version           ", materialobject.version);
                    cmd.Parameters.AddWithValue("@color             ", materialobject.color);
                    cmd.Parameters.AddWithValue("@size              ", materialobject.size);
                    cmd.Parameters.AddWithValue("@uom_in            ", materialobject.uom_in);
                    cmd.Parameters.AddWithValue("@uom_stock         ", materialobject.uom_stock);
                    cmd.Parameters.AddWithValue("@qty_in            ", materialobject.qty_in);
                    cmd.Parameters.AddWithValue("@qty_stock         ", materialobject.qty_stock);

                    MaterialSQL result = null;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        rdr.Read();
                        if (rdr.HasRows)
                        {
                            result = mapinsert(rdr);
                        }
                    }
                    return result;
                }
            }
        }

        public int /*MaterialSQL*/ DeleteItem_master(MaterialSQL materialobject)
        {
            //MaterialSQL result = null;
            int result = 1;
            using (var coon = OpenDbConnection())
            {
                using (SqlCommand cmd = new SqlCommand(DELETE, coon))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@item_id", materialobject.item_id);
                    cmd.ExecuteReader();
                }
                return result;
            }
        }

        public MaterialSQL mapinsert(SqlDataReader rdr)
        {
            var result = new MaterialSQL();
            //result.item_id = Convert.ToInt32(rdr["item_id"]);
            result.item_no = rdr["item_no"].ToString();
            result.item_name = rdr["item_name"].ToString();
            result.group_id = Convert.ToInt32(rdr["group_id"]);
            result.category_id = Convert.ToInt32(rdr["category_id"]);
            result.material_acc_id = Convert.ToInt32(rdr["material_acc_id"]);
            result.costing_method_id = Convert.ToInt32(rdr["costing_method_id"]);
            result.description = rdr["description"].ToString();
            result.status = (bool)rdr["status"];
            result.stock_count = (bool)rdr["stock_count"];
            result.overdraw_stock = (bool)rdr["overdraw_stock"];
            result.picture_path = rdr["picture_path"].ToString();
            result.brand = rdr["brand"].ToString();
            result.version = rdr["version"].ToString();
            result.color = rdr["color"].ToString();
            result.size = rdr["size"].ToString();
            result.uom_in = Convert.ToInt32(rdr["uom_in"]);
            result.qty_in = Convert.ToDecimal(rdr["qty_in"]);
            result.uom_stock = Convert.ToInt32(rdr["uom_stock"]);
            result.qty_stock = Convert.ToDecimal(rdr["qty_stock"]);


            //result.msg = Convert.ToInt32(rdr["msg"]);
            //result.GroupSQLModel = new GroupDao().maplistgroupDao(rdr);
            //result.CategorySQLModel = new CategoryDao().maplistcategory(rdr);

            return result;
        }
        public MaterialSQL mapView(SqlDataReader rdr)
        {
            if (Convert.ToInt32(rdr["item_master_item_id"]) == null)
            {
                return new MaterialSQL();
            }

            var result = new MaterialSQL();
            result.item_id = Convert.ToInt32(rdr["item_master_item_id"]);
            result.item_no = rdr["item_master_item_no"].ToString();
            result.item_name = rdr["item_master_item_name"].ToString();
            result.group_id = Convert.ToInt32(rdr["item_master_group_id"]);
            result.category_id = Convert.ToInt32(rdr["item_master_category_id"]);
            result.description = rdr["item_master_description"].ToString();
            result.status = (bool)rdr["item_master_status"];
            result.material_acc_id = Convert.ToInt32(rdr["item_master_material_acc_id"]);
            result.costing_method_id = Convert.ToInt32(rdr["item_master_costing_meterial"]);
            result.stock_count = (bool)rdr["item_master_stock_count"];
            result.overdraw_stock = (bool)rdr["item_master_overdraw_stock"];
            result.picture_path = rdr["item_master_picture_path"].ToString();
            result.brand = rdr["item_master_brand"].ToString();
            result.version = rdr["item_master_version"].ToString();
            result.color = rdr["item_master_color"].ToString();
            result.size = rdr["item_master_size"].ToString();
            result.uom_in = Convert.ToInt32(rdr["item_master_uom_in"]);
            result.qty_in = Convert.ToDecimal(rdr["item_master_qty_in"]);
            result.uom_stock = Convert.ToInt32(rdr["item_master_uom_stock"]);
            result.qty_stock = Convert.ToDecimal(rdr["item_master_qty_stock"]);

            result.GroupSQLModel = GroupDao.mapviewlistgroupDao(rdr);
            result.CategorySQLModel = new CategoryDao().mapviewlistcategory(rdr);

            return result;
        }
        //public MaterialSearch mapSearch(SqlDataReader rdr)
        //{
        //    var result = mapView(rdr);

        //    if (rdr["Count"])
        //    {
        //        result.Count = Convert.ToInt32(rdr["item_master_uom_in"]);
        //    }
        //}
    }
}