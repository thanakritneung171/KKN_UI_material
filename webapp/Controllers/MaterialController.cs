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
            MaterialSQLindex listindex = new MaterialSQLindex();
            List<MaterialSQL> mtlist = new List<MaterialSQL>();
            List<GroupSQL> gtlist = new List<GroupSQL>();
            List<CategorySQL> ctlist = new List<CategorySQL>();
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

                var query2 = "SELECT * FROM  group_item";

                // Build a command to execute this
                using (SqlCommand cmd = new SqlCommand(query2, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    //var result = new MaterialSQL();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            //result=mapView(rdr);
                            gtlist.Add(maplistgroup(rdr));
                        }
                    }
                }

                var query3 = "SELECT * FROM  category";

                // Build a command to execute this
                using (SqlCommand cmd = new SqlCommand(query3, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    //var result = new MaterialSQL();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            //result=mapView(rdr);
                            ctlist.Add(maplistcategory(rdr));
                        }
                    }
                }
            }
           
            listindex.MaterialSQLlist = mtlist.ToList();
            listindex.GroupSQLlist = gtlist.ToList();
            listindex.CategorySQLlist = ctlist.ToList();

            return View(listindex);
        }


        public ActionResult Creatematerial()
        {
            MaterialSQLindex MaterialSQLlistindex = new MaterialSQLindex();
            List<MaterialSQL> mtlist = new List<MaterialSQL>();
            List<GroupSQL> gtlist = new List<GroupSQL>();
            List<CategorySQL> ctlist = new List<CategorySQL>();
            List<Material_accSQL> macclist = new List<Material_accSQL>();
            List<Costing_methodSQL> costinglist = new List<Costing_methodSQL>();
            List<uomSQL> uomlist = new List<uomSQL>();
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

                var query2 = "SELECT * FROM  group_item";

                // Build a command to execute this
                using (SqlCommand cmd = new SqlCommand(query2, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    //var result = new MaterialSQL();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            //result=mapView(rdr);
                            gtlist.Add(maplistgroup(rdr));
                        }
                    }
                }

                var query3 = "SELECT * FROM  category";

                // Build a command to execute this
                using (SqlCommand cmd = new SqlCommand(query3, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    //var result = new MaterialSQL();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            //result=mapView(rdr);
                            ctlist.Add(maplistcategory(rdr));
                        }
                    }
                }

                var query4 = "SELECT * FROM  material_acc";

                // Build a command to execute this
                using (SqlCommand cmd = new SqlCommand(query4, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    //var result = new MaterialSQL();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            //result=mapView(rdr);
                            macclist.Add(maplistmaterialacc(rdr));
                        }
                    }


                }

                var query5 = "SELECT * FROM  costing_method";

                // Build a command to execute this
                using (SqlCommand cmd = new SqlCommand(query5, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    //var result = new MaterialSQL();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            //result=mapView(rdr);
                            costinglist.Add(maplistcosting(rdr));
                        }
                    }
                }

                var query6 = "SELECT * FROM  uom";

                // Build a command to execute this
                using (SqlCommand cmd = new SqlCommand(query6, conn))
                {
                    cmd.CommandType = CommandType.Text;

                    //var result = new MaterialSQL();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            //result=mapView(rdr);
                            uomlist.Add(maplistuom(rdr));
                        }
                    }
                }
            }

            MaterialSQLlistindex.MaterialSQLlist = mtlist.ToList();
            MaterialSQLlistindex.GroupSQLlist = gtlist.ToList();
            MaterialSQLlistindex.CategorySQLlist = ctlist.ToList();
            MaterialSQLlistindex.Material_accSQLlist = macclist.ToList();
            MaterialSQLlistindex.Costing_methodSQL_list = costinglist.ToList();
            MaterialSQLlistindex.UomSQL_list = uomlist.ToList();
            

            return View(MaterialSQLlistindex);
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
        public JsonResult Createtodata(MaterialSQL materialdata)
        {

            using (var conn = OpenDbConnection())
            {
               
                using (SqlCommand cmd = new SqlCommand("dbo.item_masterCreate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@item_no           ",materialdata.item_no);
                    cmd.Parameters.AddWithValue("@item_name         ",materialdata.item_name        );
                    cmd.Parameters.AddWithValue("@group_id          ",materialdata.group_id         );
                    cmd.Parameters.AddWithValue("@category_id       ",materialdata.category_id      );
                    cmd.Parameters.AddWithValue("@material_acc_id   ",materialdata.material_acc_id  );
                    cmd.Parameters.AddWithValue("@costing_method_id ",materialdata.costing_method_id);
                    cmd.Parameters.AddWithValue("@description       ", materialdata.description);
                    cmd.Parameters.AddWithValue("@status            ",materialdata.status           );
                    cmd.Parameters.AddWithValue("@stock_count       ",materialdata.stock_count      );
                    cmd.Parameters.AddWithValue("@overdraw_stock    ",materialdata.overdraw_stock   );
                    //cmd.Parameters.AddWithValue("@picture_path      ",materialdata.picture_path     );
                    cmd.Parameters.AddWithValue("@brand             ",materialdata.brand            );
                    cmd.Parameters.AddWithValue("@version           ",materialdata.version          );
                    cmd.Parameters.AddWithValue("@color             ",materialdata.color            );
                    cmd.Parameters.AddWithValue("@size              ",materialdata.size             );
                    cmd.Parameters.AddWithValue("@uom_in            ",materialdata.uom_in           );
                    cmd.Parameters.AddWithValue("@uom_stock         ",materialdata.uom_stock        );
                    cmd.Parameters.AddWithValue("@qty_in            ",materialdata.qty_in           );
                    cmd.Parameters.AddWithValue("@qty_stock         ", materialdata.qty_stock);

                    cmd.ExecuteReader();
                }
            }

            //    var i = materialModeldata.Where(s => s.item_no == materialdata.item_no).FirstOrDefault();
            //string output;
            //if (i != null)
            //{
            //     output = "0";
            //    return Json(new {  output=output }, JsonRequestBehavior.AllowGet);
            //}

            //MaterialSQL materialListpush = new MaterialSQL
            //{
            //    item_no                        =  materialdata.item_no,      
            //    item_name                      =  materialdata.item_name,
            //    group_id                       =  materialdata.group_id,                  
            //    category_id                    =  materialdata.category_id,               
            //    material_acc_id                =  materialdata.material_acc_id,       
            //    costing_method_id              =  materialdata.costing_method_id,
            //    stock_count                    =  materialdata.stock_count,            
            //    overdraw_stock                 =  materialdata.overdraw_stock,         
            //    status                         =  materialdata.status,                 
            //    brand                          =  materialdata.brand,                  
            //    version                        =  materialdata.version,                
            //    color                          =  materialdata.color,                  
            //    size                           =  materialdata.size,                   
            //    description                    =  materialdata.description,            
            //    uom_in                         =  materialdata.uom_in,                 
            //    uom_stock                      =  materialdata.uom_stock,              
            //    qty_in                         =  materialdata.qty_in,                 
            //    qty_stock                      =  materialdata.qty_stock,
            //    picture_path                   =  materialdata.picture_path,
            //    //picture_file                   = materialdata.picture_file
                
            //};

            //materialModeldata.Add(MaterialSQLlist);
            
             //output = "1";


            return Json( JsonRequestBehavior.AllowGet);
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


            result.GroupSQLModel = mapviewgroup(rdr);
            result.CategorySQLModel = mapviewcategory(rdr);


            return result;
        }

        public GroupSQL mapviewgroup(SqlDataReader rdr)
        {
            var resultgroup = new GroupSQL();
            //resultgroup.group_id = Convert.ToInt32(rdr["group_id"]);
            resultgroup.group_name = rdr["group_time_group_name"].ToString();

            return resultgroup;
        }

        public CategorySQL mapviewcategory(SqlDataReader rdr)
        {
            var resultcategory = new CategorySQL();
            //resultgroup.group_id = Convert.ToInt32(rdr["group_id"]);
            resultcategory.category_name = rdr["category_category_name"].ToString();
            //result.GroupSQLModel = 


            return resultcategory;
        }

        public GroupSQL maplistgroup(SqlDataReader rdr)
        {
            var resultgroup = new GroupSQL();
            resultgroup.group_id = Convert.ToInt32(rdr["group_id"]);
            resultgroup.group_name = rdr["group_name"].ToString();

            return resultgroup;
        }

        public CategorySQL maplistcategory(SqlDataReader rdr)
        {


            var resultcategory = new CategorySQL();
            resultcategory.category_id = Convert.ToInt32(rdr["category_id"]);
            resultcategory.group_id = Convert.ToInt32(rdr["group_id"]);
            resultcategory.category_name = rdr["category_name"].ToString();
   
            return resultcategory;
        }

        public Material_accSQL maplistmaterialacc(SqlDataReader rdr)
        {
            var resultmacc = new Material_accSQL();
            resultmacc.material_acc_id = Convert.ToInt32(rdr["material_acc_id"]);
            resultmacc.material_acc_name = rdr["material_acc_name"].ToString();

            return resultmacc;
        }

        public Costing_methodSQL maplistcosting(SqlDataReader rdr)
        {
            var resultcosting = new Costing_methodSQL();
            resultcosting.costing_method_id = Convert.ToInt32(rdr["costing_method_id"]);
            resultcosting.costing_method_name = rdr["costing_method_name"].ToString();

            return resultcosting;
        }

        public uomSQL maplistuom(SqlDataReader rdr)
        {
            var resultuom = new uomSQL();
            resultuom.uom_id = Convert.ToInt32(rdr["uom_id"]);
            resultuom.uom_name = rdr["uom_name"].ToString();

            return resultuom;
        }
    }
}





