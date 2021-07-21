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




        static IList<groupmaterial> groupmaterial = new List<groupmaterial>
            {
                 new groupmaterial {
                    group_id                       = 1,
                    group_name                     = "Concrete"

                 }
            };

        static IList<categorymaterial> categorymaterial = new List<categorymaterial>
            {
                 new categorymaterial {
                    category_id                       = 1,
                    group_id                       = 1,
                    category_name                     = "ปูนมิกซ์"

                 }
            };

        static IList<material_account> material_accountdata = new List<material_account>
            {
                 new material_account {
                    material_account_id                       = 1,
                    material_account_name                     = "1000-00 สินทรัพย์"

                 }
            };

        static IList<costing_method_material> costing_method_material_data = new List<costing_method_material>
            {
                 new costing_method_material {
                    costing_method_material_id                       = 1,
                    costing_method_material_name                     = "FIFO"

                 }
            };
        
        static IList<uom> uomdata = new List<uom>
            {
                 new uom {
                    uom_id                       = 1,
                    uom_name                     = "ลัง"

                 }
            };

        static IList<materialModel> materialModeldata = new List<materialModel>
            {
                new materialModel {
                    item_no                       = "bm003",
                    item_name                     = "บัวปูนปั่น",
                    group_id                      = 1,
                    category_id                      = 1,
                    description                   = "ฟหกดเ้่าสว222",
                    status                        = true,
                    material_account              = 1,
                    costing_method_material       = 2,
                    stock_count                   = false,
                    overdraw_stock                = true,
                    picture_path                  = "30-windows-xp-bliss-windows-10 (1).jpg",
                    brand                         = "กุซซี่",
                    version                       = "เวอร์ชั่นปรับปรุง5",
                    color                         = "แดง",
                    size                          = "20 cm, หนา 2.5 cm , ยาว 2.2m ",
                    uom_in                        = 1,
                    qty_in                        = 1,
                    uom_stock                     = 3,
                    qty_stock                     = 100,
                    //groupmaterial = groupmaterial.Where(data => data.group_id == 3).FirstOrDefault(),
                    
                 }
            };

        //private materialModel data = new materialModel();
        // GET: Material
        public ActionResult Index()
        {
            //List<materialModel> materialModeldatauser = materialModeldata.ToList();
            //List<groupmaterial> groupmaterialdata = groupmaterial.ToList();
            //List<categorymaterial> categorymaterialdata = categorymaterial.ToList();

            //var employeeData = from m in materialModeldatauser
            //                join g in groupmaterialdata on m.group_id equals g.group_id into table1
            //                from g in table1.ToList()
            //                join c in categorymaterialdata on m.category_id equals c.category_id into table2
            //                from c in table2.ToList()
            //                select new mateview
            //                {
            //                    materialModeldata = m,
            //                    groupmaterialdata = g,
            //                    categorymaterialdata = c

            //                };

            List<MaterialSQL> mtlist = new List<MaterialSQL>();
            using (var conn = OpenDbConnection())
            {
                var query = "SELECT * FROM  MaterialView";

                // Build a command to execute this
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    //var result = new MaterialSQL();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            //result=mapView(rdr);
                            mtlist.Add(mapView(rdr));
                        }
                    }


                }
            }


            //List<MaterialSQL> mtlist = new List<MaterialSQL>();
            //string CS = ConfigurationManager.ConnectionStrings["DBContext"].ConnectionString;
            //using (SqlConnection con = new SqlConnection(CS))
            //{
            //    SqlCommand cmd = new SqlCommand("SELECT * FROM item_master", con);
            //    cmd.CommandType = CommandType.Text;
            //    con.Open();

            //    SqlDataReader rdr = cmd.ExecuteReader();
            //    while (rdr.Read())
            //    {
            //        var result = new MaterialSQL();

            //        result.item_no = rdr["item_no"].ToString();
            //        result.item_name = rdr["item_name"].ToString();
            //        result.category_id = Convert.ToInt32(rdr["category_id"]);
            //        result.description = rdr["description"].ToString();
            //        result.status = rdr["status"].ToString();
            //        result.material_acc_id = Convert.ToInt32(rdr["material_acc_id"]);
            //        result.group_id = Convert.ToInt32(rdr["group_id"]);
            //        result.costing_method_id = Convert.ToInt32(rdr["costing_method_id"]);
            //        result.stock_count = rdr["stock_count"].ToString();
            //        result.overdraw_stock = rdr["overdraw_stock"].ToString();
            //        result.picture_path = rdr["picture_path"].ToString();
            //        result.brand = rdr["brand"].ToString();
            //        result.version = rdr["version"].ToString();
            //        result.color = rdr["color"].ToString();
            //        result.size = rdr["size"].ToString();
            //        result.uom_in = Convert.ToInt32(rdr["uom_in"]);
            //        result.qty_in = Convert.ToDecimal(rdr["qty_in"]);
            //        result.uom_stock = Convert.ToInt32(rdr["uom_stock"]);
            //        result.qty_stock = Convert.ToDecimal(rdr["qty_stock"]);


            //        mtlist.Add(result);
            //    }
            //}



            return View(mtlist);
        }


        public ActionResult Creatematerial()
        {
            materialmodalview materialmodalviewname = new materialmodalview();
            materialmodalviewname.grouplist = groupmaterial.ToList();
            materialmodalviewname.categorylist = categorymaterial.ToList();
            materialmodalviewname.material_account_list = material_accountdata.ToList();
            materialmodalviewname.costing_method_material_list = costing_method_material_data.ToList();
            materialmodalviewname.uomlist = uomdata.ToList();

            return View(materialmodalviewname);
        }

        public ActionResult Editmaterial(string id)
        {
            materialmodalview materialmodalviewname = new materialmodalview
            {
                categorylist= categorymaterial.ToList(),
                grouplist = groupmaterial.ToList(),
                material_account_list = material_accountdata.ToList(),
                costing_method_material_list = costing_method_material_data.ToList(),
                uomlist = uomdata.ToList(),
                materialdata = materialModeldata.Where(data => data.item_no == id).FirstOrDefault()
        };

            //List<materialModel> materialModel = new data.data().Materials().ToList();
            //materialModel material = new data.data().Materials().Where(data => data.item_no == item_no).FirstOrDefault();
            //var material = materialModeldata.Where(data =>data.item_no == id).FirstOrDefault();
            
            return View(materialmodalviewname);
        }

        [HttpPost]
        public JsonResult Editmaterialdata(materialModel material)
        {

            //List<materialModel> materialModel = new data.data().Materials().ToList();
            //materialModel material = new data.data().Materials().Where(data => data.item_no == item_no).FirstOrDefault();
            var student = materialModeldata.Where(s => s.item_no == material.item_no).FirstOrDefault();
       
            //student.size = material.size;
            materialModeldata.Remove(student);
            materialModeldata.Add(material);

            string output = "1";

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public JsonResult Createtodata(materialModel materialdata)
        {
            var i = materialModeldata.Where(s => s.item_no == materialdata.item_no).FirstOrDefault();
            string output;
            if (i != null)
            {
                 output = "0";
                return Json(new {  output=output }, JsonRequestBehavior.AllowGet);
            }

            materialModel materialListpush = new materialModel
            {
                item_no                        =  materialdata.item_no,      
                item_name                      =  materialdata.item_name,
                group_id                        =  materialdata.group_id,                  
                category_id                       =  materialdata.category_id,               
                material_account               =  materialdata.material_account,       
                costing_method_material        =  materialdata.costing_method_material,
                stock_count                    =  materialdata.stock_count,            
                overdraw_stock                 =  materialdata.overdraw_stock,         
                status                         =  materialdata.status,                 
                brand                          =  materialdata.brand,                  
                version                        =  materialdata.version,                
                color                          =  materialdata.color,                  
                size                           =  materialdata.size,                   
                description                    =  materialdata.description,            
                uom_in                         =  materialdata.uom_in,                 
                uom_stock                      =  materialdata.uom_stock,              
                qty_in                         =  materialdata.qty_in,                 
                qty_stock                      =  materialdata.qty_stock,
                picture_path                   =  materialdata.picture_path,
                //picture_file                   = materialdata.picture_file
                
            };

          //  UploadFiles(materialListpush.picture_file);
            materialModeldata.Add(materialListpush);
            
             output = "1";


            return Json(materialListpush, output, JsonRequestBehavior.AllowGet);
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


        public MaterialSQL mapView(SqlDataReader rdr)
        {
            

            var result = new MaterialSQL();
            result.item_no = rdr["item_no"].ToString();
            result.item_name = rdr["item_name"].ToString();
            //result.category_id = Convert.ToInt32(rdr["category_id"]);
            result.description = rdr["description"].ToString();
            //result.status = rdr.GetBool("status");
            result.status = (bool)rdr["status"];
            //result.material_acc_id = Convert.ToInt32(rdr["material_acc_id"]);
            //result.group_id = Convert.ToInt32(rdr["group_id"]);
            //result.costing_method_id = Convert.ToInt32(rdr["costing_method_id"]);
            result.stock_count = (bool)rdr["stock_count"];
            result.overdraw_stock = (bool)rdr["overdraw_stock"];
            result.picture_path = rdr["picture_path"].ToString();
            result.brand = rdr["brand"].ToString();
            result.version = rdr["version"].ToString();
            result.color = rdr["color"].ToString();
            result.size = rdr["size"].ToString();
            //result.uom_in = Convert.ToInt32(rdr["uom_in"]);
            result.qty_in = Convert.ToDecimal(rdr["qty_in"]);
            //result.uom_stock = Convert.ToInt32(rdr["uom_stock"]);
            result.qty_stock = Convert.ToDecimal(rdr["qty_stock"]);


            result.GroupSQLModel = mapviewgroup(rdr);
            result.CategorySQLModel = mapviewcategory(rdr);


            return result;
        }

        public GroupSQL mapviewgroup(SqlDataReader rdr)
        {


            var resultgroup = new GroupSQL();
            //resultgroup.group_id = Convert.ToInt32(rdr["group_id"]);
            resultgroup.group_name = rdr["group_name"].ToString();
            //result.GroupSQLModel = 


            return resultgroup;
        }

        public CategorySQL mapviewcategory(SqlDataReader rdr)
        {


            var resultcategory = new CategorySQL();
            //resultgroup.group_id = Convert.ToInt32(rdr["group_id"]);
            resultcategory.category_name = rdr["category_name"].ToString();
            //result.GroupSQLModel = 


            return resultcategory;
        }



    }
}





