using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Configuration;

namespace MLPipeSysNew.Models
{
    public class WebFileModel
    {
        static HttpFileCollectionBase httpFileCollections = null;

        /// <summary>
        /// Save File
        /// </summary>
        /// <param name="appConfigPathName">Appsetting name</param>
        /// <param name="files">HttpFile object</param>
        /// <param name="subPath">add a subpath to ur folder</param>
        /// <param name="fileNameBefore">add a specific name to avoid cover old file</param>
        public static void SaveFile(string appConfigPathName, HttpFileCollectionBase files, string subPath = "", string fileNameBefore = "")
        {
            httpFileCollections = files;
            string fileName = null;
            try
            {
                //Add a webconfiguration to set the url link
                string rootPath = WebConfigurationManager.AppSettings[appConfigPathName] + (string.IsNullOrEmpty(subPath) ? "" : subPath);
                //Create the directory if not exist
                DirectoryInfo di = new DirectoryInfo(rootPath);
                if (!di.Exists) di.Create();
                for (int i = 0; i < files.Count; i++)
                {
                    if (string.IsNullOrEmpty(files[i].FileName)) continue;
                    //combine the specific string to file name
                    fileName = (string.IsNullOrEmpty(fileNameBefore) ? "" : fileNameBefore + "-") + Path.GetFileName(files[i].FileName);
                    files[i].SaveAs(
                        Path.Combine(rootPath, fileName)
                        );
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }
        /// <summary>
        /// Mapping the form value to the table model
        /// </summary>
        /// <param name="obj">model object</param>
        /// <param name="files">HttpFile object</param>
        /// <param name="fileNameBefore">add a specific name to avoid cover old file</param>
        /// <returns></returns>
        public static object BindModelFileName(object obj, HttpFileCollectionBase files, string fileNameBefore = "")
        {
            string fileName = null;
            Type t = obj.GetType();
            PropertyInfo pi;
            try
            {
                foreach (string f in files)
                {
                    //use reflection to map the value
                    fileName = Path.GetFileName(files[f].FileName);
                    if (string.IsNullOrEmpty(fileName)) continue;
                    pi = t.GetProperty(f);
                    
                    pi.SetValue(obj, string.IsNullOrEmpty(fileNameBefore) ? fileName : fileNameBefore + "-" + fileName, null);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            return obj;
        }

    }
}
