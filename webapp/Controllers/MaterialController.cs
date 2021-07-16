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
                    group_name                     = "groupp111"

                 },
                new groupmaterial {
                    group_id                       = 2,
                    group_name                     = "groupp222"
                 },
                new groupmaterial {
                    group_id                       = 3,
                    group_name                     = "groupp3332"
                 }
            };

        static IList<materialModel> materialModeldata = new List<materialModel>
            {
                 new materialModel {
                    item_no                       = 1,
                    item_name                     = "neung",
                    group_id                         = 3,
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
                    item_no                       = 2,
                    item_name                     = "songg",
                    group_id                         = 3,
                    category                      = 2,
                    description                   = "vvfdfsdfsdssdsfgfdsfdsf",
                    status                        = false,
                    material_account              = 3,
                    costing_method_material       = 2,
                    stock_count                   = true,
                    overdraw_stock                = true,
                    picture_path                  = "Y",
                    brand                         = "dsss",
                    version                       = "fg",
                    color                         = "sg",
                    size                          = "hrs4w",
                    uom_in                        = "uon",
                    qty_in                        = 10,
                    uom_stock                     = "upm",
                    qty_stock                     = 100
                 },
                new materialModel {
                    item_no                       = 3,
                    item_name                     = "hhhhh",
                    group_id                         = 3,
                    category                      = 2,
                    description                   = "vvfdfsdfsdsfdsfdsf",
                    status                        = true,
                    material_account              = 3,
                    costing_method_material       = 2,
                    stock_count                   = true,
                    overdraw_stock                = false,
                    picture_path                  = "Y",
                    brand                         = "dsss",
                    version                       = "fg",
                    color                         = "sg",
                    size                          = "hrs4w",
                    uom_in                        = "uon",
                    qty_in                        = 10,
                    uom_stock                     = "upm",
                    qty_stock                     = 100
                 },
                new materialModel {
                    item_no                       = 4,
                    item_name                     = "hhhhh",
                    group_id                         = 3,
                    category                      = 2,
                    description                   = "vvfdfsdfsdsfdsfdsf",
                    status                        = false,
                    material_account              = 3,
                    costing_method_material       = 2,
                    stock_count                   = false,
                    overdraw_stock                = true,
                    picture_path                  = "Y",
                    brand                         = "dsss",
                    version                       = "fg",
                    color                         = "sg",
                    size                          = "hrs4w",
                    uom_in                        = "uon",
                    qty_in                        = 10,
                    uom_stock                     = "upm",
                    qty_stock                     = 100
                 },
                new materialModel {
                    item_no                       = 5,
                    item_name                     = "hhhhh",
                    group_id                         = 3,
                    category                      = 2,
                    description                   = "vvfdfsdfsdsfdsfdsf",
                    status                        = true,
                    material_account              = 3,
                    costing_method_material       = 2,
                    stock_count                   = false,
                    overdraw_stock                = false,
                    picture_path                  = "Y",
                    brand                         = "dsss",
                    version                       = "fg",
                    color                         = "sg",
                    size                          = "hrs4w",
                    uom_in                        = "uon",
                    qty_in                        = 10,
                    uom_stock                     = "upm",
                    qty_stock                     = 100
                 },
                new materialModel {
                    item_no                       = 6,
                    item_name                     = "hhhhh",
                    group_id                         = 3,
                    category                      = 2,
                    description                   = "vvfdfsdfsdsfdsfdsf",
                    status                        = true,
                    material_account              = 3,
                    costing_method_material       = 2,
                    stock_count                   = true,
                    overdraw_stock                = true,
                    picture_path                  = "Y",
                    brand                         = "dsss",
                    version                       = "fg",
                    color                         = "sg",
                    size                          = "hrs4w",
                    uom_in                        = "uon",
                    qty_in                        = 10,
                    uom_stock                     = "upm",
                    qty_stock                     = 100
                 },
                new materialModel {
                    item_no                       = 7,
                    item_name                     = "hhhhh",
                    group_id                         = 3,
                    category                      = 2,
                    description                   = "vvfdfsdfsdsfdsfdsf",
                    status                        = true,
                    material_account              = 3,
                    costing_method_material       = 2,
                    stock_count                   = true,
                    overdraw_stock                = true,
                    picture_path                  = "Y",
                    brand                         = "dsss",
                    version                       = "fg",
                    color                         = "sg",
                    size                          = "hrs4w",
                    uom_in                        = "uon",
                    qty_in                        = 10,
                    uom_stock                     = "upm",
                    qty_stock                     = 100
                 }
            };

        //private materialModel data = new materialModel();
        // GET: Material
        public ActionResult Index()
        {
            var materiallist = materialModeldata.ToList();
            return View(materiallist);
        }

        public ActionResult Creatematerial()
        {
            return View(groupmaterial);
        }

        public ActionResult Editmaterial(int? id)
        {

            //List<materialModel> materialModel = new data.data().Materials().ToList();
            //materialModel material = new data.data().Materials().Where(data => data.item_no == item_no).FirstOrDefault();
            var material = materialModeldata.Where(data =>data.item_no == id).FirstOrDefault();
            
            return View(material);
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