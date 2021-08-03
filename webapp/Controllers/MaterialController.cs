using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KKN_UI.Models;
using System.Data;
using KKN_UI.data;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using KKN_UI.material.uom;
using KKN_UI.Models.Material;
//using KKN_UI.Models.Group;
//using KKN_UI.Models.Category;
using KKN_UI.Models.Uom;
using KKN_UI.Models.Material_acc;
using KKN_UI.Models.Costing_method;
using KKN_UI.material.group;
using KKN_UI.material.category;
using KKN_UI.material.Costing_method;
using KKN_UI.material.Material_acc;
using KKN_UI.materialDao.item_master;

namespace KKN_UI.Controllers
{
    public class MaterialController : Controller
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

      
        public ActionResult Index()
        {
            MaterialSQLindex listindex = new MaterialSQLindex();
            List<MaterialSQL> mtlist = new List<MaterialSQL>();
            //List<GroupSQL> gtlist = new List<GroupSQL>();
            //List<CategorySQL> ctlist = new List<CategorySQL>();
            using (var conn = OpenDbConnection())
            {
                var query = "SELECT * FROM  MaterialView";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            //mtlist.Add(mapView(rdr));
                            mtlist.Add(new item_masterDao().mapView(rdr));
                        }
                    }
                }

                #region old
                //var query2 = "SELECT * FROM  group_item";

                //using (SqlCommand cmd = new SqlCommand(query2, conn))
                //{
                //    cmd.CommandType = CommandType.Text;

                //    using (var rdr = cmd.ExecuteReader())
                //    {
                //        while (rdr.Read())
                //        {
                //            gtlist.Add(maplistgroup(rdr));
                //        }
                //    }
                //}

                //var query3 = "SELECT * FROM  category";

                //using (SqlCommand cmd = new SqlCommand(query3, conn))
                //{
                //    cmd.CommandType = CommandType.Text;

                //    using (var rdr = cmd.ExecuteReader())
                //    {
                //        while (rdr.Read())
                //        {
                //            ctlist.Add(maplistcategory(rdr));
                //        }
                //    }
                //}
                #endregion
            }
            //listindex.GroupSQLlist = gtlist.ToList();
            //listindex.CategorySQLlist = ctlist.ToList();


            //uomDao uomd = new uomDao();
            //List<UomSQL> uud = new uomDao().uomlistdata.ToList();

            listindex.MaterialSQLlist = mtlist.ToList();
            listindex.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();
            listindex.CategorySQLlist = new CategoryDao().Getdata().Categorylist.ToList();
            
            return View(listindex);
        }

        public ActionResult _selectcategory(int id)
        {
            CategorySQLlist Cdata = new CategoryDao().GetdataCategoryGourpByid(id);

            return PartialView(Cdata);
        }

        public ActionResult Creatematerial()
        {
            MaterialSQLindex MaterialSQLlistindex = new MaterialSQLindex();

            #region older
            ////List<MaterialSQL> mtlist = new List<MaterialSQL>();
            //List<GroupSQL> gtlist = new List<GroupSQL>();
            //List<CategorySQL> ctlist = new List<CategorySQL>();
            //List<Material_accSQL> macclist = new List<Material_accSQL>();
            //List<Costing_methodSQL> costinglist = new List<Costing_methodSQL>();
            //List<UomSQL> uomlist = new List<UomSQL>();


            //using (var conn = OpenDbConnection())
            //{
            //    //var query = "SELECT * FROM  MaterialView";
            //    //using (SqlCommand cmd = new SqlCommand(query, conn))
            //    //{
            //    //    cmd.CommandType = CommandType.Text;

            //    //    using (var rdr = cmd.ExecuteReader())
            //    //    {
            //    //        while (rdr.Read())
            //    //        {
            //    //            mtlist.Add(mapView(rdr));
            //    //        }
            //    //    }
            //    //} 

            //    //var query2 = "SELECT * FROM  group_item";

            //    //// Build a command to execute this
            //    //using (SqlCommand cmd = new SqlCommand(query2, conn))
            //    //{
            //    //    cmd.CommandType = CommandType.Text;

            //    //    //var result = new MaterialSQL();
            //    //    using (var rdr = cmd.ExecuteReader())
            //    //    {
            //    //        while (rdr.Read())
            //    //        {
            //    //            //result=mapView(rdr);
            //    //            gtlist.Add(maplistgroup(rdr));
            //    //        }
            //    //    }
            //    //}

            //    //var query3 = "SELECT * FROM  category";

            //    //// Build a command to execute this
            //    //using (SqlCommand cmd = new SqlCommand(query3, conn))
            //    //{
            //    //    cmd.CommandType = CommandType.Text;

            //    //    //var result = new MaterialSQL();
            //    //    using (var rdr = cmd.ExecuteReader())
            //    //    {
            //    //        while (rdr.Read())
            //    //        {
            //    //            //result=mapView(rdr);
            //    //            ctlist.Add(maplistcategory(rdr));
            //    //        }
            //    //    }
            //    //}

            //    //var query4 = "SELECT * FROM  material_acc";

            //    //// Build a command to execute this
            //    //using (SqlCommand cmd = new SqlCommand(query4, conn))
            //    //{
            //    //    cmd.CommandType = CommandType.Text;

            //    //    //var result = new MaterialSQL();
            //    //    using (var rdr = cmd.ExecuteReader())
            //    //    {
            //    //        while (rdr.Read())
            //    //        {
            //    //            //result=mapView(rdr);
            //    //            macclist.Add(maplistmaterialacc(rdr));
            //    //        }
            //    //    }


            //    //}

            //    //var query5 = "SELECT * FROM  costing_method";

            //    //// Build a command to execute this
            //    //using (SqlCommand cmd = new SqlCommand(query5, conn))
            //    //{
            //    //    cmd.CommandType = CommandType.Text;

            //    //    using (var rdr = cmd.ExecuteReader())
            //    //    {
            //    //        while (rdr.Read())
            //    //        {
            //    //            costinglist.Add(maplistcosting(rdr));
            //    //        }
            //    //    }
            //    //}

            //    //var query6 = "SELECT * FROM  uom";

            //    //using (SqlCommand cmd = new SqlCommand(query6, conn))
            //    //{
            //    //    cmd.CommandType = CommandType.Text;

            //    //    //var result = new MaterialSQL();
            //    //    using (var rdr = cmd.ExecuteReader())
            //    //    {
            //    //        while (rdr.Read())
            //    //        {
            //    //            uomlist.Add(maplistuom(rdr));
            //    //        }
            //    //    }
            //    //}
            //}


            ////MaterialSQLlistindex.MaterialSQLlist = mtlist.ToList();
            //MaterialSQLlistindex.GroupSQLlist = gtlist.ToList();
            //MaterialSQLlistindex.CategorySQLlist = ctlist.ToList();
            //MaterialSQLlistindex.Material_accSQLlist = macclist.ToList();
            //MaterialSQLlistindex.Costing_methodSQL_list = costinglist.ToList();
            //MaterialSQLlistindex.UomSQL_list = uomlist.ToList();       
            #endregion

            //MaterialSQLlistindex.MaterialSQLlist = mtlist.ToList();
            MaterialSQLlistindex.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();
            MaterialSQLlistindex.CategorySQLlist = new CategoryDao().Getdata().Categorylist.ToList();
            MaterialSQLlistindex.Material_accSQLlist = new Material_accDao().Getdata().Material_acclist.ToList();
            MaterialSQLlistindex.Costing_methodSQL_list = new Costing_methodDao().Getdata().Costing_methodlist.ToList();
            MaterialSQLlistindex.UomSQL_list = new uomDao().uomlistdata.ToList();


            return View(MaterialSQLlistindex);
        }

        [HttpPost]
        public JsonResult Createtodata(MaterialSQL materialdata)
        {
         var output =   new item_masterDao().CheckItemNo(materialdata);

         //var output =   new item_masterDao().InsertItem_master(materialdata);

            #region hidden
            //using (var conn = OpenDbConnection())
            //{
            //    using (SqlCommand cmd = new SqlCommand("dbo.item_masterCreate", conn))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        //cmd.Parameters.AddWithValue("@item_id           ",materialdata.item_id);
            //        cmd.Parameters.AddWithValue("@item_no           ",materialdata.item_no);
            //        cmd.Parameters.AddWithValue("@item_name         ",materialdata.item_name        );
            //        cmd.Parameters.AddWithValue("@group_id          ",materialdata.group_id         );
            //        cmd.Parameters.AddWithValue("@category_id       ",materialdata.category_id      );
            //        cmd.Parameters.AddWithValue("@material_acc_id   ",materialdata.material_acc_id  );
            //        cmd.Parameters.AddWithValue("@costing_method_id ",materialdata.costing_method_id);
            //        cmd.Parameters.AddWithValue("@description       ", materialdata.description);
            //        cmd.Parameters.AddWithValue("@status            ",materialdata.status           );
            //        cmd.Parameters.AddWithValue("@stock_count       ",materialdata.stock_count      );
            //        cmd.Parameters.AddWithValue("@overdraw_stock    ",materialdata.overdraw_stock   );
            //        //cmd.Parameters.AddWithValue("@picture_path      ",materialdata.picture_path     );
            //        cmd.Parameters.AddWithValue("@brand             ",materialdata.brand            );
            //        cmd.Parameters.AddWithValue("@version           ",materialdata.version          );
            //        cmd.Parameters.AddWithValue("@color             ",materialdata.color            );
            //        cmd.Parameters.AddWithValue("@size              ",materialdata.size             );
            //        cmd.Parameters.AddWithValue("@uom_in            ",materialdata.uom_in           );
            //        cmd.Parameters.AddWithValue("@uom_stock         ",materialdata.uom_stock        );
            //        cmd.Parameters.AddWithValue("@qty_in            ",materialdata.qty_in           );
            //        cmd.Parameters.AddWithValue("@qty_stock         ", materialdata.qty_stock);

            //        cmd.ExecuteReader();
            //    }
            //}
            #endregion

            
            return Json( new {output = output  }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editmaterial(string id)
        {
           
            MaterialSQLindex MaterialSQLlistindex = new MaterialSQLindex();
            MaterialSQL mtlist = new MaterialSQL();
            List<GroupSQL> gtlist = new List<GroupSQL>();
            List<CategorySQL> ctlist = new List<CategorySQL>();
            List<Material_accSQL> macclist = new List<Material_accSQL>();
            List<Costing_methodSQL> costinglist = new List<Costing_methodSQL>();
            List<UomSQL> uomlist = new List<UomSQL>();
            using (var conn = OpenDbConnection())
            {
                var query = "SELECT TOP 1 * FROM MaterialView WHERE item_master_item_no=" + "'"+ id+"'";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            mtlist = new item_masterDao().mapView(rdr);
                        }
                    }
                }

                #region old
                //var query2 = "SELECT * FROM  group_item";

                //// Build a command to execute this
                //using (SqlCommand cmd = new SqlCommand(query2, conn))
                //{
                //    cmd.CommandType = CommandType.Text;

                //    //var result = new MaterialSQL();
                //    using (var rdr = cmd.ExecuteReader())
                //    {
                //        while (rdr.Read())
                //        {
                //            //result=mapView(rdr);
                //            gtlist.Add(maplistgroup(rdr));
                //        }
                //    }
                //}

                //var query3 = "SELECT * FROM  category";

                //// Build a command to execute this
                //using (SqlCommand cmd = new SqlCommand(query3, conn))
                //{
                //    cmd.CommandType = CommandType.Text;

                //    //var result = new MaterialSQL();
                //    using (var rdr = cmd.ExecuteReader())
                //    {
                //        while (rdr.Read())
                //        {
                //            //result=mapView(rdr);
                //            ctlist.Add(maplistcategory(rdr));
                //        }
                //    }
                //}

                //var query4 = "SELECT * FROM  material_acc";

                //// Build a command to execute this
                //using (SqlCommand cmd = new SqlCommand(query4, conn))
                //{
                //    cmd.CommandType = CommandType.Text;

                //    //var result = new MaterialSQL();
                //    using (var rdr = cmd.ExecuteReader())
                //    {
                //        while (rdr.Read())
                //        {
                //            //result=mapView(rdr);
                //            macclist.Add(maplistmaterialacc(rdr));
                //        }
                //    }


                //}

                //var query5 = "SELECT * FROM  costing_method";

                //// Build a command to execute this
                //using (SqlCommand cmd = new SqlCommand(query5, conn))
                //{
                //    cmd.CommandType = CommandType.Text;

                //    //var result = new MaterialSQL();
                //    using (var rdr = cmd.ExecuteReader())
                //    {
                //        while (rdr.Read())
                //        {
                //            //result=mapView(rdr);
                //            costinglist.Add(maplistcosting(rdr));
                //        }
                //    }
                //}

                //var query6 = "SELECT * FROM  uom";

                //// Build a command to execute this
                //using (SqlCommand cmd = new SqlCommand(query6, conn))
                //{
                //    cmd.CommandType = CommandType.Text;

                //    //var result = new MaterialSQL();
                //    using (var rdr = cmd.ExecuteReader())
                //    {
                //        while (rdr.Read())
                //        {
                //            //result=mapView(rdr);
                //            uomlist.Add(maplistuom(rdr));
                //        }
                //    }
                //}
                #endregion
            }

            //MaterialSQLlistindex.MaterialSQLdataselect = mtlist;
            //MaterialSQLlistindex.GroupSQLlist = gtlist.ToList();
            //MaterialSQLlistindex.CategorySQLlist = ctlist.ToList();
            //MaterialSQLlistindex.Material_accSQLlist = macclist.ToList();
            //MaterialSQLlistindex.Costing_methodSQL_list = costinglist.ToList();
            //MaterialSQLlistindex.UomSQL_list = uomlist.ToList(); 


            MaterialSQLlistindex.MaterialSQLdataselect = new materialviewDao().GetdataByid(id);
            MaterialSQLlistindex.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();
            MaterialSQLlistindex.CategorySQLlist = new CategoryDao().Getdata().Categorylist.ToList();
            MaterialSQLlistindex.Material_accSQLlist = new Material_accDao().Getdata().Material_acclist.ToList();
            MaterialSQLlistindex.Costing_methodSQL_list = new Costing_methodDao().Getdata().Costing_methodlist.ToList();
            MaterialSQLlistindex.UomSQL_list = new uomDao().uomlistdata.ToList();

            return View(MaterialSQLlistindex);
        }

        [HttpPost]
        public JsonResult Editmaterialdata(MaterialSQL materialdata)
        {
            #region old
            //using (var conn = OpenDbConnection())
            //{
            //    using (SqlCommand cmd = new SqlCommand("dbo.item_masterUpdate", conn))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        cmd.Parameters.AddWithValue("@item_id           ", materialdata.item_id);
            //        cmd.Parameters.AddWithValue("@item_no           ", materialdata.item_no);
            //        cmd.Parameters.AddWithValue("@item_name         ", materialdata.item_name);
            //        cmd.Parameters.AddWithValue("@group_id          ", materialdata.group_id);
            //        cmd.Parameters.AddWithValue("@category_id       ", materialdata.category_id);
            //        cmd.Parameters.AddWithValue("@material_acc_id   ", materialdata.material_acc_id);
            //        cmd.Parameters.AddWithValue("@costing_method_id ", materialdata.costing_method_id);
            //        cmd.Parameters.AddWithValue("@description       ", materialdata.description);
            //        cmd.Parameters.AddWithValue("@status            ", materialdata.status);
            //        cmd.Parameters.AddWithValue("@stock_count       ", materialdata.stock_count);
            //        cmd.Parameters.AddWithValue("@overdraw_stock    ", materialdata.overdraw_stock);
            //        //cmd.Parameters.AddWithValue("@picture_path      ",materialdata.picture_path     );
            //        cmd.Parameters.AddWithValue("@brand             ", materialdata.brand);
            //        cmd.Parameters.AddWithValue("@version           ", materialdata.version);
            //        cmd.Parameters.AddWithValue("@color             ", materialdata.color);
            //        cmd.Parameters.AddWithValue("@size              ", materialdata.size);
            //        cmd.Parameters.AddWithValue("@uom_in            ", materialdata.uom_in);
            //        cmd.Parameters.AddWithValue("@uom_stock         ", materialdata.uom_stock);
            //        cmd.Parameters.AddWithValue("@qty_in            ", materialdata.qty_in);
            //        cmd.Parameters.AddWithValue("@qty_stock         ", materialdata.qty_stock);

            //        cmd.ExecuteReader();
            //    }
            //}
            #endregion
            new item_masterDao().UpdateItem_master(materialdata);

            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteMaterial(MaterialSQL mateid)
        {
            #region old
            //using (var coon = OpenDbConnection())
            //{
            //    using (SqlCommand cmd = new SqlCommand("dbo.item_masterDelete",coon))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;

            //        cmd.Parameters.AddWithValue("@item_id", mateid.item_id);
            //        cmd.ExecuteReader();
            //    }
            //}
            #endregion

            new item_masterDao().DeleteItem_master(mateid);
                return Json(JsonRequestBehavior.AllowGet);
        }

 

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try 
                {

                    //Method 2 Get file details from HttpPostedFileBase class    

                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadedFiles"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading."; ;
                }
            }
            return View("Index");
        }


        #region map older
        //public MaterialSQL mapView(SqlDataReader rdr)
        //{
        //    var result = new MaterialSQL();
        //    result.item_id = Convert.ToInt32(rdr["item_master_item_id"]);
        //    result.item_no = rdr["item_master_item_no"].ToString();
        //    result.item_name = rdr["item_master_item_name"].ToString();
        //    result.group_id = Convert.ToInt32(rdr["item_master_group_id"]);
        //    result.category_id = Convert.ToInt32(rdr["item_master_category_id"]);
        //    result.description = rdr["item_master_description"].ToString();
        //    result.status = (bool)rdr["item_master_status"];
        //    result.material_acc_id = Convert.ToInt32(rdr["item_master_material_acc_id"]);
        //    result.costing_method_id = Convert.ToInt32(rdr["item_master_costing_meterial"]);
        //    result.stock_count = (bool)rdr["item_master_stock_count"];
        //    result.overdraw_stock = (bool)rdr["item_master_overdraw_stock"];
        //    result.picture_path = rdr["item_master_picture_path"].ToString();
        //    result.brand = rdr["item_master_brand"].ToString();
        //    result.version = rdr["item_master_version"].ToString();
        //    result.color = rdr["item_master_color"].ToString();
        //    result.size = rdr["item_master_size"].ToString();
        //    result.uom_in = Convert.ToInt32(rdr["item_master_uom_in"]);
        //    result.qty_in = Convert.ToDecimal(rdr["item_master_qty_in"]);
        //    result.uom_stock = Convert.ToInt32(rdr["item_master_uom_stock"]);
        //    result.qty_stock = Convert.ToDecimal(rdr["item_master_qty_stock"]);

        //    result.GroupSQLModel = new GroupDao().maplistgroupDao(rdr);
        //    result.CategorySQLModel = new CategoryDao().maplistcategory(rdr);

        //    return result;
        //}

        //public GroupSQL mapviewgroup(SqlDataReader rdr)
        //{
        //    var resultgroup = new GroupSQL();
        //    resultgroup.group_name = rdr["group_time_group_name"].ToString();

        //    return resultgroup;
        //}

        //public CategorySQL mapviewcategory(SqlDataReader rdr)
        //{
        //    var resultcategory = new CategorySQL();
        //    resultcategory.category_name = rdr["category_category_name"].ToString();

        //    return resultcategory;
        //}


        //public GroupSQL maplistgroup(SqlDataReader rdr)
        //{
        //    var resultgroup = new GroupSQL();
        //    resultgroup.group_id = Convert.ToInt32(rdr["group_id"]);
        //    resultgroup.group_name = rdr["group_name"].ToString();

        //    return resultgroup;
        //}

        //public CategorySQL maplistcategory(SqlDataReader rdr)
        //{
        //    var resultcategory = new CategorySQL();
        //    resultcategory.category_id = Convert.ToInt32(rdr["category_id"]);
        //    resultcategory.group_id = Convert.ToInt32(rdr["group_id"]);
        //    resultcategory.category_name = rdr["category_name"].ToString();

        //    return resultcategory;
        //}

        //public Material_accSQL maplistmaterialacc(SqlDataReader rdr)
        //{
        //    var resultmacc = new Material_accSQL();
        //    resultmacc.material_acc_id = Convert.ToInt32(rdr["material_acc_id"]);
        //    resultmacc.material_acc_name = rdr["material_acc_name"].ToString();

        //    return resultmacc;
        //}

        //public Costing_methodSQL maplistcosting(SqlDataReader rdr)
        //{
        //    var resultcosting = new Costing_methodSQL();
        //    resultcosting.costing_method_id = Convert.ToInt32(rdr["costing_method_id"]);
        //    resultcosting.costing_method_name = rdr["costing_method_name"].ToString();

        //    return resultcosting;
        //}

        //public UomSQL maplistuom(SqlDataReader rdr)
        //{
        //    var resultuom = new UomSQL();
        //    resultuom.uom_id = Convert.ToInt32(rdr["uom_id"]);
        //    resultuom.uom_name = rdr["uom_name"].ToString();

        //    return resultuom;
        //}
        #endregion




        public ActionResult _groupView()
        {
            //List<GroupSQL> Grouplistdata = new List<GroupSQL>();

            List<GroupSQL> Grouplistdata = new GroupDao().Getdata().Grouplist.ToList();
            
            return PartialView("Groupmaterial/_groupView", Grouplistdata);
        }


        [HttpPost]
        public JsonResult CreateGroup(GroupSQL groupdata)
        {
            var output = new GroupDao().InsertGroup(groupdata);

            return Json(new { output = output is null ? 0 : 1 }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult _creategroup()
        {
            GroupSQL gdata = new GroupSQL();
            return PartialView("Groupmaterial/_creategroup", gdata);
        }

        public ActionResult _editgroup(int id)
        {
            GroupSQL gdata = new GroupDao().GetdataByid(id);
            return PartialView("Groupmaterial/_creategroup", gdata);
        }

        [HttpPost]
        public JsonResult UpdateGroup(GroupSQL groupdata)
        {
           new GroupDao().UpdateGroup(groupdata);

            return Json( JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteGroup(GroupSQL groupdata)
        {
            var output = new GroupDao().DeleteGroup(groupdata);

            return Json(/*new { output = output is null ? 0 : 1 },*/ JsonRequestBehavior.AllowGet);
        }

        public ActionResult _comfirmgroup()
        {
            return PartialView("Groupmaterial/_comfirmgroup");
        }

        ///////////////////////////////////////////////////
        public ActionResult _categoryView()
        {
            //List<GroupSQL> Grouplistdata = new List<GroupSQL>();

            //CategorySQL CGlist = new CategorySQL();
            List<CategorySQL> Categorylistdata = new CategoryDao().GetdataView().Categorylist.ToList();
            return PartialView("Categorymaterial/_categoryView", Categorylistdata);
        }


        [HttpPost]
        public JsonResult CreateCategory(CategorySQL categorydata)
        {
            var output = new CategoryDao().InsertCategory(categorydata);

            return Json(new { output = output is null ? 0 : 1 }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult _createcategory()
        {
            MaterialSQLindex materiallist = new MaterialSQLindex();
            CategorySQL gdata = new CategorySQL();
            materiallist.CategorySQL = gdata;
            materiallist.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();

            return PartialView("Categorymaterial/_createcategory", materiallist);
        }

        public ActionResult _editcategory(int id)
        {
            MaterialSQLindex materiallist = new MaterialSQLindex();
            CategorySQL gdata = new CategoryDao().GetdataByid(id);
            materiallist.CategorySQL = gdata;
            materiallist.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();
            return PartialView("Categorymaterial/_createcategory", materiallist);
        }

        [HttpPost]
        public JsonResult UpdateCategory(CategorySQL categorydata)
        {
            new CategoryDao().UpdateCategory(categorydata);

            return Json(JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteCategory(CategorySQL categorydata)
        {
            var output = new CategoryDao().DeleteCategory(categorydata);

            return Json(/*new { output = output is null ? 0 : 1 },*/ JsonRequestBehavior.AllowGet);
            //var output = DeleteService(12);
            return Json(/*new { output = output is null ? 0 : 1 },*/ JsonRequestBehavior.AllowGet);
        }

        public ActionResult _comfirmcategory()
        {
            return PartialView("Categorymaterial/_comfirmcategory");
        }



        //public ActionResult DeleteService(int id)
        //{
        //    var code = CategoryDao().Code(string);
        //    if (code != null)
        //    {
        //        return "duplicate";
        //    }

        //    var output = new CategoryDao().DeleteCategory(id);
        //    return output;
        //}

    }
}