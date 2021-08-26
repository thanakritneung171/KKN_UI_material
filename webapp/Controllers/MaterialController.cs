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
                var query = "SELECT top 100 * FROM  MaterialView";

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

        public /*JsonResult*/ ActionResult _tabledatalist(SearchItem search)
        {
            MaterialSQLindex listindex = new MaterialSQLindex();


            if (search.category_id == 0 && search.group_id == 0 && search.text == null)
            {
                List<MaterialSQL> mtlist = new List<MaterialSQL>();
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
                }
                listindex.MaterialSQLlist = mtlist.ToList();
            }
            else
            {
                listindex.MaterialSQLlist = new materialviewDao().GetdataSearch(search).ToList();
            }

            listindex.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();
            listindex.CategorySQLlist = new CategoryDao().Getdata().Categorylist.ToList();
            return PartialView(listindex);
            //return Json(new { listindex }, JsonRequestBehavior.AllowGet);
        }

        public /*JsonResult*/ ActionResult _tabledatalistIndex(SearchItem search)
        {
            MaterialSQLindex listindex = new MaterialSQLindex();


            //if (search.category_id == 0 && search.group_id == 0 && search.text == null)
            //{
            List<MaterialSQL> mtlist = new List<MaterialSQL>();
            using (var conn = OpenDbConnection())
            {
                var query = "SELECT top 0 * FROM  MaterialView";

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
            }
            listindex.MaterialSQLlist = mtlist.ToList();
            //}
            //else
            //{
            //    listindex.MaterialSQLlist = new materialviewDao().GetdataSearch(search).ToList();
            //}

            listindex.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();
            listindex.CategorySQLlist = new CategoryDao().Getdata().Categorylist.ToList();
            return PartialView("_tabledatalist", listindex);
        }

        public ActionResult _grouptable(bool active)
        {
            List<GroupSQL> Grouplistdata = new List<GroupSQL>();
            //if (active == true)
            //{
            //    Grouplistdata = new GroupDao().Getdata().Grouplist.ToList();
            //}
            //else
            //{
            Grouplistdata = new GroupDao().GetdataByActive(active).Grouplist.ToList();
            //}

            return PartialView("Groupmaterial/_grouptable", Grouplistdata);
        }
        public ActionResult _uomtable(bool active)
        {
            List<UomSQL> Uomlistdata = new List<UomSQL>();

            Uomlistdata = new uomDao().GetdataByActive(active).Uomlist.ToList();
            

            return PartialView("Uom/_uomtable", Uomlistdata);
        }

        public ActionResult _categorytable(bool active)
        {
            List<CategorySQL> categorylistdata = new List<CategorySQL>();
            //if (active == true)
            //{
            //    categorylistdata = new CategoryDao().GetdataView().Categorylist.ToList();
            //}
            //else
            //{
            categorylistdata = new CategoryDao().GetdataByActive(active).Categorylist.ToList();
            //}

            return PartialView("Categorymaterial/_categorytable", categorylistdata);
        }


        public /*JsonResult*/ ActionResult IndexSearch(SearchItem search)
        {
            MaterialSQLindex listindex = new MaterialSQLindex();
            //List<MaterialSQL> mlist = new List<MaterialSQL>();
            listindex.MaterialSQLlist = new materialviewDao().GetdataSearch(search).ToList();
            listindex.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();
            listindex.CategorySQLlist = new CategoryDao().Getdata().Categorylist.ToList();
            return PartialView(listindex);
            //return  Json(new { listindex }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _selectgroup()
        {
            MaterialSQLindex MaterialSQLlistindex = new MaterialSQLindex();
            MaterialSQLlistindex.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();

            return PartialView(MaterialSQLlistindex.GroupSQLlist);
        }
        public ActionResult _selectcategory(int id, string page)
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
            MaterialSQLlistindex.MaterialSQLdataselect = new MaterialSQL();
            MaterialSQLlistindex.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();
            MaterialSQLlistindex.CategorySQLlist = new CategoryDao().Getdata().Categorylist.ToList();
            MaterialSQLlistindex.Material_accSQLlist = new Material_accDao().Getdata().Material_acclist.ToList();
            MaterialSQLlistindex.Costing_methodSQL_list = new Costing_methodDao().Getdata().Costing_methodlist.ToList();
            MaterialSQLlistindex.UomSQL_list = new uomDao().Getdata().Uomlist.ToList();
            MaterialSQLlistindex.Picturelist = new List<picture_master>();

            return View(MaterialSQLlistindex);
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
            //using (var conn = OpenDbConnection())
            //{
            //    var query = "SELECT TOP 1 * FROM MaterialView WHERE item_master_item_no=" + "'" + id + "'";

            //    using (SqlCommand cmd = new SqlCommand(query, conn))
            //    {
            //        cmd.CommandType = CommandType.Text;

            //        using (var rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                mtlist = new item_masterDao().mapView(rdr);
            //            }
            //        }
            //    }
            //}
            MaterialSQLlistindex.MaterialSQLdataselect = new materialviewDao().GetdataByid(id);
            MaterialSQLlistindex.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();
            MaterialSQLlistindex.CategorySQLlist = new CategoryDao().Getdata().Categorylist.ToList();
            MaterialSQLlistindex.Material_accSQLlist = new Material_accDao().Getdata().Material_acclist.ToList();
            MaterialSQLlistindex.Costing_methodSQL_list = new Costing_methodDao().Getdata().Costing_methodlist.ToList();
            MaterialSQLlistindex.UomSQL_list = new uomDao().Getdata().Uomlist.ToList();
            MaterialSQLlistindex.Picturelist = new item_masterDao().GetPicturedataByid(id).ToList();
            ViewBag.dis = "disabled";

            return View("Creatematerial", MaterialSQLlistindex);
        }

        private static string FILE_PATH111 = @"C:\UploadedFiles";
        [HttpPost]
        public ActionResult Createfile111111(MaterialSQL material, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var originalFilename = Path.GetFileName(file.FileName);
                string fileId = Guid.NewGuid().ToString().Replace("-", "");
                //string userId = GetUserId(); // Function to get user id based on your schema
                string newnamesave = string.Format("{0}{1}{2}", fileId, DateTime.Now.Year.ToString(), originalFilename);

                var path = Path.Combine(Server.MapPath("~/UploadedFiles/Photo/") + newnamesave);
                file.SaveAs(path);

                //eventmodel.picture_path = fileId;
                material.picture_path = newnamesave;

                //_db.EventModels.AddObject(eventmodel);
                //_db.SaveChanges();
                //return Json(new {success = true, Error = "" },JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = material.picture_path, Error = "error cant save" }, JsonRequestBehavior.AllowGet);
        }

        private static string FILE_PATH = @"C:\UploadedFiles";
        public static void Createfile(string path, picture_master picture, HttpPostedFileBase file)
        {
            var pathus = Path.Combine(path, picture.picture_path);
            file.SaveAs(pathus);

        }
        public static void Deletefile(string path, string newnamedel)
        {
            var pathus = Path.Combine(path, newnamedel);
            System.IO.File.Delete(pathus);

        }

        public static picture_master genaratePathfile(MaterialSQL material, HttpPostedFileBase file)
        {
            picture_master picture = new picture_master();
            var originalFilename = Path.GetFileName(file.FileName);
            string fileId = Guid.NewGuid().ToString().Replace("-", "");
            //string userId = GetUserId(); // Function to get user id based on your schema
            string newnamesave = string.Format("{0}{1}{2}", fileId, DateTime.Now.Year.ToString(), originalFilename);

            picture.picture_path = newnamesave;
            picture.picture_name = originalFilename;
            picture.item_no = material.item_no;

            return picture;
        }

        [HttpPost]
        public JsonResult Createtodata(MaterialSQL material, List<HttpPostedFileBase> file)
        {
            //if(ModelState.IsValid)
            //{

            //}

            //var output = new item_masterDao().CheckItemNo(material);

            MaterialSQL output = new MaterialSQL();
            picture_master picture = new picture_master();
            var path = Server.MapPath("~/UploadedFiles/Photo/");

            var checkitemno = new item_masterDao().CheckItemNo(material);
            if (checkitemno == true)
            {
                output.msg = 2;
            }
            else if (checkitemno == false)
            {
                var checkitemname = new item_masterDao().CheckItemName(material);
                if (checkitemname == true)
                {
                    var checkdetailitem = new item_masterDao().CheckDetialItem(material);
                    if (checkdetailitem == true)
                    {
                        output.msg = 3;
                    }
                    else if (checkdetailitem == false)
                    {
                        output = new item_masterDao().InsertItem_master(material);
                    }

                }
                else if (checkitemname == false)
                {
                    output = new item_masterDao().InsertItem_master(material);
                }
            }
            if (output.msg == 1)
            {
                if (file != null)
                {
                    foreach (HttpPostedFileBase ff in file)
                    {
                        picture = genaratePathfile(material, ff);

                        var pictureoutput = new item_masterDao().InsertPicture_master(picture);
                        picture.msg = pictureoutput.msg;
                        Createfile(path, picture, ff);
                    }
                }
            }



            //new  MaterialController().Createfile(material, file);

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



            return Json(new { output = output }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Editmaterialdata(MaterialSQL material, List<HttpPostedFileBase> file)
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

            MaterialSQL output = new MaterialSQL();
            picture_master picture = new picture_master();
            var path = Server.MapPath("~/UploadedFiles/Photo/");
           

            var checkupdateitem = new item_masterDao().Checkupdateitem(material);
            if (checkupdateitem == true)
            {
                output.msg = 3;
            }
            else
            {
                output = new item_masterDao().UpdateItem_master(material);
            }

            var namepathdelete = searchdeletefile(material.item_no);

            if (output.msg == 1 && file != null && namepathdelete != null )
            {
                
                foreach (var items in namepathdelete) {
                    if (!String.IsNullOrEmpty(items.picture_path)) {
                        Deletefile(path, items.picture_path);
                    }
                }
            }

            foreach (HttpPostedFileBase ff in file)
            {
                if (output.msg == 1 && file != null)
                {
                  var genaratepathfile =   genaratePathfile(material, ff);
                    var pictureoutput = new item_masterDao().InsertPicture_master(genaratepathfile);
                    picture.msg = pictureoutput.msg;
                    Createfile(path, genaratepathfile, ff);
                }
            }

            return Json(new { output = output }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateItemActive(MaterialSQL itemSQL)
        {
            MaterialSQL output = new MaterialSQL();
            new item_masterDao().UpdateActiveItem_master(itemSQL);
            return Json(new { output = output is null ? 0 : 1 }, JsonRequestBehavior.AllowGet);
        }

        public static List<picture_master> searchdeletefile(string id)
        {
            //var pictureserch = new picture_master();
            var pictureserch = new item_masterDao().deletefilepath(id);
            new item_masterDao().Picture_masterDelete(id);
            return pictureserch;
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

        public ActionResult _groupView()
        {
            //List<GroupSQL> Grouplistdata = new List<GroupSQL>();

            List<GroupSQL> Grouplistdata = new GroupDao().Getdata().Grouplist.ToList();

            return PartialView("Groupmaterial/_groupView", Grouplistdata);
        }

        public ActionResult _groupViewActive(bool active)
        {
            //List<GroupSQL> Grouplistdata = new List<GroupSQL>();

            List<GroupSQL> Grouplistdata = new GroupDao().GetdataByActive(active).Grouplist.ToList();

            return PartialView("Groupmaterial/_groupView", Grouplistdata);
        }


        [HttpPost]
        public JsonResult CreateGroup(GroupSQL groupdata)
        {
            GroupSQL output = new GroupSQL();
            var check = new GroupDao().CheckGroupNew(groupdata);
            if (check == true)
            {
                output = null;
            }
            else
            {
                output = new GroupDao().InsertGroup(groupdata);
            }

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

            GroupSQL output = new GroupSQL();
            var check = new GroupDao().CheckGroupNew(groupdata);
            if (check == true)
            {
                output = null;
            }
            else
            {
                new GroupDao().UpdateGroup(groupdata);
            }


            return Json(new { output = output is null ? 0 : 1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateGorupActive(GroupSQL groupSQL)
        {
            GroupSQL output = new GroupSQL();
            new GroupDao().UpdateGroupActive(groupSQL);
            return Json(new { output = output is null ? 0 : 1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteGroup(GroupSQL groupdata)
        {
            Rowgroup rowgroup = new Rowgroup();
            var checkdeleteitem = new GroupDao().DeleteCheckItem(groupdata);
            if (checkdeleteitem.row == 0)
            {
                var checkdeletecategory = new GroupDao().DeleteCheckCategory(groupdata);
                if (checkdeletecategory.row == 0)
                {
                    new GroupDao().DeleteGroup(groupdata);
                }
                else
                {
                    rowgroup = checkdeletecategory;
                    rowgroup.check = "checkcategory";
                }
            }
            else
            {
                rowgroup = checkdeleteitem;
                rowgroup.check = "checkitem";
            }



            return Json(new { output = rowgroup }, JsonRequestBehavior.AllowGet);
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

        public ActionResult _categoryViewActive(bool active)
        {
            //List<GroupSQL> Grouplistdata = new List<GroupSQL>();

            List<CategorySQL> Categorylistdata = new CategoryDao().GetdataByActive(active).Categorylist.ToList();

            return PartialView("Groupmaterial/_groupView", Categorylistdata);
        }

        [HttpPost]
        public JsonResult CreateCategory(CategorySQL categorydata)
        {
            CategorySQL output = new CategorySQL();
            var check = new CategoryDao().CheckCategoryNew(categorydata);
            if (check == true)
            {
                output = null;
            }
            else
            {
                output = new CategoryDao().InsertCategory(categorydata);
            }

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
            CategorySQL output = new CategorySQL();
            var check = new CategoryDao().CheckCategoryNew(categorydata);
            if (check == true)
            {
                output = null;
            }
            else
            {
                new CategoryDao().UpdateCategory(categorydata);
            }

            return Json(new { output = output is null ? 0 : 1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateCategoryActive(CategorySQL categorydata)
        {
            CategorySQL output = new CategorySQL();
            new CategoryDao().UpdateCategoryActive(categorydata);
            return Json(new { output = output is null ? 0 : 1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteCategory(CategorySQL categorydata)
        {
            Rowcategory rowcategory = new Rowcategory();
            var checkdeleteitem = new CategoryDao().DeleteCheckItem(categorydata);
            if (checkdeleteitem.row == 0)
            {
                new CategoryDao().DeleteCategory(categorydata);
            }
            else
            {
                rowcategory = checkdeleteitem;
                rowcategory.check = "checkitem";
            }


            //var output = DeleteService(12);
            return Json(new { output = rowcategory }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _comfirmcategory()
        {
            return PartialView("Categorymaterial/_comfirmcategory");
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        public ActionResult _uomView()
        {
            //List<GroupSQL> Grouplistdata = new List<GroupSQL>();

            List<UomSQL> Uomlistdata = new uomDao().Getdata().Uomlist.ToList();

            return PartialView("Uom/_uomView", Uomlistdata);
        }

        public ActionResult _uomViewActive(bool active)
        {

            List<UomSQL> Uomlistdata = new uomDao().GetdataByActive(active).Uomlist.ToList();

            return PartialView("Uom/_uomView", Uomlistdata);
        }


        [HttpPost]
        public JsonResult CreateUom(UomSQL uomdata)
        {
            UomSQL output = new UomSQL();
            var check = new uomDao().CheckUomNew(uomdata);
            if (check == true)
            {
                output = null;
            }
            else
            {
                output = new uomDao().InsertUom(uomdata);
            }

            return Json(new { output = output is null ? 0 : 1 }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult _createuom()
        {
            UomSQL udata = new UomSQL();
            return PartialView("Uom/_createuom", udata);
        }

        public ActionResult _edituom(int id)
        {
            UomSQL udata = new uomDao().GetdataByid(id);
            return PartialView("Uom/_createuom", udata);
        }

        [HttpPost]
        public JsonResult UpdateUom(UomSQL uomdata)
        {

            UomSQL output = new UomSQL();
            var check = new uomDao().CheckUomNew(uomdata);
            if (check == true)
            {
                output = null;
            }
            else
            {
                new uomDao().UpdateUom(uomdata);
            }


            return Json(new { output = output is null ? 0 : 1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdateUomActive(UomSQL uomdata)
        {
            UomSQL output = new UomSQL();
            new uomDao().UpdateUomActive(uomdata);
            return Json(new { output = output is null ? 0 : 1 }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteUom(UomSQL uomdata)
        {
            RowUom rowuom = new RowUom();
            var checkdeleteitem = new uomDao().DeleteCheckItem(uomdata);
            if (checkdeleteitem.row == 0)
            {
                    new uomDao().DeleteUom(uomdata);
            }
            else
            {
                rowuom = checkdeleteitem;
                rowuom.check = "checkitem";
            }



            return Json(new { output = rowuom }, JsonRequestBehavior.AllowGet);
        }

     
 

    }
}