using MLPipeSysNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MLPipeSysNew.Controllers
{
    public class FileController : Controller
    {
        //
        // GET: /File/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// A post controller 
        /// </summary>
        /// <param name="model">your table object</param>
        /// <param name="form">form collection if you need</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult FormPost(T_BASE model, FormCollection form)
        {
            HttpFileCollectionBase files = Request.Files;
            /*
             * How to use ...
             * Map the file name to your T_BASE table class 
             * if your T_BASE have the fields like FILE1 , FILE2 , FILE3 ..etc
             * and the web form have the same fields to post 
             * the below steps will map the form fields to class fields and save the files to specific folder
             */
            WebFileModel.BindModelFileName(model, files, "20160314XXXX");
            //Save the files 
            WebFileModel.SaveFile("EgnBaseFolder", files, "", "20160314XXXX");

            // save model block
            //...

            return View();
        }

    }
}
