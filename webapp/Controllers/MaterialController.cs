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
using KKN_UI.Models.Material_acc;
using KKN_UI.Models.Costing_method;
using KKN_UI.material.group;
using KKN_UI.material.category;
using KKN_UI.material.Costing_method;
using KKN_UI.material.Material_acc;
using KKN_UI.materialDao.item_master;
using System.Drawing;
using System.Text.RegularExpressions;

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
                            mtlist.Add(new item_masterDao().mapView(rdr));
                        }
                    }
                }
            }
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
        }

        public /*JsonResult*/ ActionResult _tabledatalistIndex(SearchItem search)
        {
            MaterialSQLindex listindex = new MaterialSQLindex();

            //List<MaterialSQL> mtlist = new List<MaterialSQL>();
            //using (var conn = OpenDbConnection())
            //{
            //    var query = "SELECT top 0 * FROM  MaterialView";

            //    using (SqlCommand cmd = new SqlCommand(query, conn))
            //    {
            //        cmd.CommandType = CommandType.Text;
            //        using (var rdr = cmd.ExecuteReader())
            //        {
            //            while (rdr.Read())
            //            {
            //                //mtlist.Add(mapView(rdr));
            //                mtlist.Add(new item_masterDao().mapView(rdr));
            //            }
            //        }
            //    }
            //}
            //listindex.MaterialSQLlist = mtlist.ToList();
            //listindex.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();
            //listindex.CategorySQLlist = new CategoryDao().Getdata().Categorylist.ToList();

            listindex.MaterialSQLlist = new List<MaterialSQL>();
            listindex.GroupSQLlist = new List<GroupSQL>();
            listindex.CategorySQLlist = new List<CategorySQL>();
            return PartialView("_tabledatalist", listindex);
        }

        public ActionResult _grouptable(bool active)
        {
            List<GroupSQL> Grouplistdata = new List<GroupSQL>();
            Grouplistdata = new GroupDao().GetdataByActive(active).Grouplist.ToList();

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
            categorylistdata = new CategoryDao().GetdataByActive(active).Categorylist.ToList();
            return PartialView("Categorymaterial/_categorytable", categorylistdata);
        }


        public /*JsonResult*/ ActionResult IndexSearch(SearchItem search)
        {
            MaterialSQLindex listindex = new MaterialSQLindex();
            listindex.MaterialSQLlist = new materialviewDao().GetdataSearch(search).ToList();
            listindex.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();
            listindex.CategorySQLlist = new CategoryDao().Getdata().Categorylist.ToList();
            return PartialView(listindex);
        }

        public ActionResult _selectgroup()
        {
            MaterialSQLindex MaterialSQLlistindex = new MaterialSQLindex();
            MaterialSQLlistindex.GroupSQLlist = new GroupDao().Getdata().Grouplist.ToList();
            return PartialView(MaterialSQLlistindex.GroupSQLlist);
        }
        public ActionResult _selectcategory(int id)
        {
            CategorySQLlist Cdata = new CategoryDao().GetdataCategoryGourpByid(id);
            return PartialView(Cdata);
        }



        public ActionResult Creatematerial()
        {
            MaterialSQLindex MaterialSQLlistindex = new MaterialSQLindex();
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

        private static string FILE_PATH = @"C:\UploadedFiles";
        private string base64File;

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

        public static picture_master genaratePathfile(MaterialSQL material, /*HttpPostedFileBase file*/ img img)
        {
            picture_master picture = new picture_master();
            var originalFilename = Path.GetFileName(img.nameimg);
            string fileId = Guid.NewGuid().ToString().Replace("-", "");
            string newnamesave = string.Format("{0}{1}{2}", fileId, DateTime.Now.Year.ToString(), originalFilename);
            picture.picture_path = newnamesave;
            picture.picture_name = originalFilename;
            picture.picture_type = img.typeimg;
            picture.item_no = material.item_no;

            return picture;
        }

        private void SaveByteArrayAsImage(string fullOutputPath, string base64String)
        {
            Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
            base64File = regex.Replace(base64String, string.Empty);
            byte[] bytes = Convert.FromBase64String(base64File);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            image.Save(fullOutputPath, System.Drawing.Imaging.ImageFormat.Png);

        }
        [HttpPost]
        public JsonResult Createtodata(MaterialSQL material, List<HttpPostedFileBase> file, List<img> img)
        {
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
                if (img != null)
                {
                    foreach (img im in img)
                    {
                        picture = genaratePathfile(material, im);
                        var fullOutputPath = Path.Combine(path, picture.picture_path);
                        var pictureoutput = new item_masterDao().InsertPicture_master(picture);
                        SaveByteArrayAsImage(fullOutputPath, im.pathimg);
                        picture.msg = pictureoutput.msg;
                    }
                }
            }
            return Json(new { output = output }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Editmaterialdata(MaterialSQL material, List<HttpPostedFileBase> file, List<img> img)
        {
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

            foreach (img im in img)
            {
                if (im.idimg == null)
                {
                    int changimg = 1;
                }
            }
            //if(changimg != 1)
            //{

            //}
            var namepathdelete = searchdeletefile(material.item_no);



            if (output.msg == 1/* && file != null */  && namepathdelete != null)
            {
                foreach (var items in namepathdelete)
                {
                    if (!String.IsNullOrEmpty(items.picture_path))
                    {
                        Deletefile(path, items.picture_path);
                    }
                }
            }

            if (img != null)
            {
                foreach (img im in img)
                {
                    picture = genaratePathfile(material, im);
                    var fullOutputPath = Path.Combine(path, picture.picture_path);
                    var pictureoutput = new item_masterDao().InsertPicture_master(picture);
                    SaveByteArrayAsImage(fullOutputPath, im.pathimg);
                    picture.msg = pictureoutput.msg;
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
            var pictureserch = new item_masterDao().deletefilepath(id);
            new item_masterDao().Picture_masterDelete(id);
            return pictureserch;
        }

        [HttpPost]
        public JsonResult DeleteMaterial(MaterialSQL mateid)
        {
            new item_masterDao().DeleteItem_master(mateid);
            return Json(JsonRequestBehavior.AllowGet);
        }

        public ActionResult _groupView()
        {
            List<GroupSQL> Grouplistdata = new GroupDao().Getdata().Grouplist.ToList();
            return PartialView("Groupmaterial/_groupView", Grouplistdata);
        }

        public ActionResult _groupViewActive(bool active)
        {
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

        public ActionResult _categoryView()
        {
            List<CategorySQL> Categorylistdata = new CategoryDao().GetdataView().Categorylist.ToList();
            return PartialView("Categorymaterial/_categoryView", Categorylistdata);
        }

        public ActionResult _categoryViewActive(bool active)
        {
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
            return Json(new { output = rowcategory }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _comfirmcategory()
        {
            return PartialView("Categorymaterial/_comfirmcategory");
        }

        public ActionResult _uomView()
        {
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