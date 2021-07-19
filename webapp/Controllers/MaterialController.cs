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


namespace KKN_UI.Controllers
{
    public class MaterialController : Controller
    {
        static IList<groupmaterial> groupmaterial = new List<groupmaterial>
            {
                 new groupmaterial {
                    group_id                       = 1,
                    group_name                     = "Concrete"

                 },
                new groupmaterial {
                    group_id                       = 2,
                    group_name                     = "Steel"
                 },
                new groupmaterial {
                    group_id                       = 3,
                    group_name                     = "Wood"
                 },
                new groupmaterial {
                    group_id                       = 4,
                    group_name                     = "Electrical"
                 },
                new groupmaterial {
                    group_id                       = 5,
                    group_name                     = "Sanitary"
                 },
                new groupmaterial {
                    group_id                       = 6,
                    group_name                     = "Colour/Paint"
                 }
            };

        static IList<categorymaterial> categorymaterial = new List<categorymaterial>
            {
                 new categorymaterial {
                    category_id                       = 1,
                    group_id                       = 1,
                    category_name                     = "ปูนมิกซ์"

                 },
                new categorymaterial {
                    category_id                       = 2,
                    group_id                       = 1,
                    category_name                     = "ปูนถง"
                 },
                new categorymaterial {
                    category_id                       = 3,
                    group_id                       = 1,
                    category_name                     = "เคมีภัณฑ์"
                 },
                new categorymaterial {
                    category_id                       = 4,
                    group_id                       = 2,
                    category_name                     = "เหล็กเส้น"
                 },
                new categorymaterial {
                    category_id                       = 5,
                    group_id                       = 2,
                    category_name                     = "เหล็กรูปพรรณ"
                 },
                new categorymaterial {
                       category_id                       = 6,
                       group_id                       = 3,
                       category_name                     = "ไม้ห้องซาวน่า"
                 }
            };

        static IList<material_account> material_accountdata = new List<material_account>
            {
                 new material_account {
                    material_account_id                       = 1,
                    material_account_name                     = "1000-00 สินทรัพย์"

                 },
                new material_account {
                    material_account_id                       = 2,
                    material_account_name                     = "1100-00 สินทรัพย์หมุนเวียน"
                 },
                new material_account {
                    material_account_id                       = 3,
                    material_account_name                     = "1110-00 เงินสดและเงินฝากธนาคาร"
                 },
                new material_account {
                    material_account_id                       = 4,
                    material_account_name                     = "1111-00 เงินสด"
                 },
                new material_account {
                    material_account_id                       = 5,
                    material_account_name                     = "1111-01 เงินสดระหว่างทาง"
                 },
                new material_account {
                    material_account_id                       = 6,
                    material_account_name                     = "1111-02 เงินสดย่อย"
                 },
                new material_account {
                    material_account_id                       = 7,
                    material_account_name                     = "1112-00 เงินฝากกระแสรายวัน"
                 }
            };

        static IList<costing_method_material> costing_method_material_data = new List<costing_method_material>
            {
                 new costing_method_material {
                    costing_method_material_id                       = 1,
                    costing_method_material_name                     = "FIFO"

                 },
                new costing_method_material {
                    costing_method_material_id                       = 2,
                    costing_method_material_name                     = "Average"
                 }
            };
        
        static IList<uom> uomdata = new List<uom>
            {
                 new uom {
                    uom_id                       = 1,
                    uom_name                     = "ลัง"

                 },
                new uom {
                    uom_id                       = 2,
                    uom_name                     = "กล่อง"
                 },
                new uom {
                    uom_id                       = 3,
                    uom_name                     = "ชิ้น"
                 }
            };

        static IList<materialModel> materialModeldata = new List<materialModel>
            {
                new materialModel {
                    item_no                       = "bm003",
                    item_name                     = "บัวปูนปั่น",
                    group_id                      = 1,
                    category                      = 1,
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
                    
                 },
                 new materialModel {
                    item_no                       = "bm004",
                    item_name                     = "บัวปูนปั่น",
                    group_id                      = 1,
                    category                      = 2,
                    description                   = "ฟหกดเ้่าสว33",
                    status                        = false,
                    material_account              = 2,
                    costing_method_material       = 1,
                    stock_count                   = false,
                    overdraw_stock                = true,
                    picture_path                  = "windows-xp-7680x4320-day-microsoft-8k-23307.jpg",
                    brand                         = "กุซซี่4",
                    version                       = "เวอร์ชั่นปรับปรุง4",
                    color                         = "ส้ม",
                    size                          = "30 cm, หนา 3 cm , ยาว 2.2m ",
                    uom_in                        = 1,
                    qty_in                        = 1,
                    uom_stock                     = 1,
                    qty_stock                     = 1,
                    //groupmaterial = groupmaterial.Where(data => data.group_id == 3).FirstOrDefault(),
                    
                 },
                 new materialModel {
                    item_no                       = "bm005",
                    item_name                     = "บัวปูนปั่น",
                    group_id                      = 2,
                    category                      = 4,
                    description                   = "ฟหกดเ้่าสว",
                    status                        = true,
                    material_account              = 3,
                    costing_method_material       = 1,
                    stock_count                   = true,
                    overdraw_stock                = true,
                    picture_path                  = "windows-xp-7680x4320-abstract-microsoft-8k-23308.jpg",
                    brand                         = "กุซซี่5",
                    version                       = "เวอร์ชั่นปรับปรุง3",
                    color                         = "เขียว",
                    size                          = "20 cm, หนา 2.5 cm , ยาว 2.2m ",
                    uom_in                        = 2,
                    qty_in                        = 1,
                    uom_stock                     = 3,
                    qty_stock                     = 10,
                    //groupmaterial = groupmaterial.Where(data => data.group_id == 3).FirstOrDefault(),
                    
                 },
                  new materialModel {
                    item_no                       = "bm006",
                    item_name                     = "บัวปูนปั่น",
                    group_id                      = 2,
                    category                      = 5,
                    description                   = "ฟหกดเ้่าสว111",
                    status                        = false,
                    material_account              = 5,
                    costing_method_material       = 2,
                    stock_count                   = false,
                    overdraw_stock                = true,
                    picture_path                  = "Digital-Art-HD-4K-Wallpapers-41715.jpg",
                    brand                         = "กุซซี่",
                    version                       = "เวอร์ชั่นปรับปรุง2",
                    color                         = "เหลือง",
                    size                          = "40 cm, หนา 4.5 cm , ยาว 2.2m ",
                    uom_in                        = 2,
                    qty_in                        = 1,
                    uom_stock                     = 2,
                    qty_stock                     = 1,
                    //groupmaterial = groupmaterial.Where(data => data.group_id == 3).FirstOrDefault(),
                    
                 },
                  new materialModel {
                    item_no                       = "bm007",
                    item_name                     = "บัวปูนปั่น",
                    group_id                      = 3,
                    category                      = 6,
                    description                   = "ฟหกดเ้่าสว55",
                    status                        = true,
                    material_account              = 6,
                    costing_method_material       = 1,
                    stock_count                   = false,
                    overdraw_stock                = false,
                    picture_path                  = "Abstract-Digital-Art-Artistic-Desktop-Wallpaper-099941.jpg",
                    brand                         = "กุซซี่6",
                    version                       = "เวอร์ชั่นปรับปรุง1",
                    color                         = "ม่วง",
                    size                          = "20 cm, หนา 2.5 cm , ยาว 5m ",
                    uom_in                        = 3,
                    qty_in                        = 100,
                    uom_stock                     = 3,
                    qty_stock                     = 100,
                    //groupmaterial = groupmaterial.Where(data => data.group_id == 3).FirstOrDefault(),
                    
                 }
            };

        //private materialModel data = new materialModel();
        // GET: Material
        public ActionResult Index()
        {
            List<materialModel> materialModeldatauser = materialModeldata.ToList();
            List<groupmaterial> groupmaterialdata = groupmaterial.ToList();
            List<categorymaterial> categorymaterialdata = categorymaterial.ToList();


            var employeeData = from m in materialModeldatauser
                            join g in groupmaterialdata on m.group_id equals g.group_id into table1
                            from g in table1.ToList()
                            join c in categorymaterialdata on m.category equals c.category_id into table2
                            from c in table2.ToList()
                            select new mateview
                            {
                                materialModeldata = m,
                                groupmaterialdata = g,
                                categorymaterialdata = c

                            };

            return View(employeeData);
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

            materialModeldata.Remove(student);
            materialModeldata.Add(material);

            string output = "1";

            return Json(output, JsonRequestBehavior.AllowGet);
        }

        

        [HttpPost]
        public JsonResult Createtodata(materialModel materialdata)
        {
            materialModel materialListpush = new materialModel
            {
                item_no                        =  materialdata.item_no,      
                item_name                      =  materialdata.item_name,
                group_id                        =  materialdata.group_id,                  
                category                       =  materialdata.category,               
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
            
            string output = "1";


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


    }
}





