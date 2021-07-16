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
                    category_name                     = "1111Concrete"

                 },
                new categorymaterial {
                    category_id                       = 2,
                    category_name                     = "22Steel"
                 },
                new categorymaterial {
                    category_id                       = 3,
                    category_name                     = "33Wood"
                 },
                new categorymaterial {
                    category_id                       = 4,
                    category_name                     = "444Electrical"
                 },
                new categorymaterial {
                    category_id                       = 5,
                    category_name                     = "55Sanitary"
                 },
                new categorymaterial {
                       category_id                       = 6,
                       category_name                     = "666Colour/Paint"
                 }
            };

        static IList<materialModel> materialModeldata = new List<materialModel>
            {
                 new materialModel {
                    item_no                       = "bm003",
                    item_name                     = "neung3",
                    group_id                      = 1,
                    category                      = 2,
                    description                   = "vvfdfsdfsdsfdsfdsf",
                    status                        = true,
                    material_account              = 3,
                    costing_method_material       = 2,
                    stock_count                   = false,
                    overdraw_stock                = true,
                    picture_path                  = "30-windows-xp-bliss-windows-10 (1).jpg",
                    brand                         = "dsss",
                    version                       = "fg",
                    color                         = "sg",
                    size                          = "hrs4w",
                    uom_in                        = "uon",
                    qty_in                        = 10,
                    uom_stock                     = "upm",
                    qty_stock                     = 100,
                    groupmaterial = groupmaterial.Where(data => data.group_id == 3).FirstOrDefault(),
                    
                 },
               new materialModel {
                    item_no                       = "bm004",
                    item_name                     = "neung2",
                    group_id                      = 2,
                    category                      = 2,
                    description                   = "vvfdfsdfsdsfdsfdsf",
                    status                        = true,
                    material_account              = 3,
                    costing_method_material       = 2,
                    stock_count                   = false,
                    overdraw_stock                = true,
                    picture_path                  = "30-windows-xp-bliss-windows-10 (1).jpg",
                    brand                         = "dsss",
                    version                       = "fg",
                    color                         = "sg",
                    size                          = "hrs4w",
                    uom_in                        = "uon",
                    qty_in                        = 10,
                    uom_stock                     = "upm",
                    qty_stock                     = 100,
                    groupmaterial = groupmaterial.Where(data => data.group_id == 3).FirstOrDefault(),

                 },
            new materialModel {
                    item_no                       = "bm005",
                    item_name                     = "neung5",
                    group_id                      = 3,
                    category                      = 2,
                    description                   = "vvfdfsdfsdsfdsfdsf",
                    status                        = false,
                    material_account              = 3,
                    costing_method_material       = 2,
                    stock_count                   = false,
                    overdraw_stock                = true,
                    picture_path                  = "30-windows-xp-bliss-windows-10 (1).jpg",
                    brand                         = "dsss",
                    version                       = "fg",
                    color                         = "sg",
                    size                          = "hrs4w",
                    uom_in                        = "uon",
                    qty_in                        = 10,
                    uom_stock                     = "upm",
                    qty_stock                     = 100,
                    groupmaterial = groupmaterial.Where(data => data.group_id == 3).FirstOrDefault(),

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



            //var materiallist = materialModeldata.ToList();

            //return View(materiallist);
            return View(employeeData);
        }

        public ActionResult Creatematerial()
        {
            materialmodalview materialmodalviewname = new materialmodalview();
            materialmodalviewname.grouplist = groupmaterial.ToList();
            materialmodalviewname.categorylist = categorymaterial.ToList();

            return View(materialmodalviewname);
        }

        public ActionResult Editmaterial(string id)
        {
            materialmodalview materialmodalviewname = new materialmodalview
            {
                categorylist= categorymaterial.ToList(),
                grouplist = groupmaterial.ToList(),
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
                group_id =  materialdata.group_id,                  
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