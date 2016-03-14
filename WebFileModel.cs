using MLPipeSysNew.Models.Business;
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
        /// 存檔
        /// </summary>
        /// <param name="appConfigPathName">Appsetting的名稱</param>
        /// <param name="files">HttpFile物件</param>
        public static void SaveFile(string appConfigPathName, HttpFileCollectionBase files, string subPath = "", string fileNameBefore = "")
        {
            httpFileCollections = files;
            string fileName = null;
            //string dir = @"H:\WebFile$\File\Standard";
            try
            {
                // 2015/07/31 追加修正各附件子資料夾以區別檔案
                string rootPath = WebConfigurationManager.AppSettings[appConfigPathName] + (string.IsNullOrEmpty(subPath) ? "" : subPath);

                DirectoryInfo di = new DirectoryInfo(rootPath);
                if (!di.Exists) di.Create();
                for (int i = 0; i < files.Count; i++)
                {
                    if (string.IsNullOrEmpty(files[i].FileName)) continue;
                    //2015/07/31 為避免檔案名稱相同追加fileNameBefore來區別
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
        /// Mapping 檔案名稱到物件上
        /// </summary>
        /// <param name="obj">要Mapping的物件</param>
        /// <param name="files"></param>
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
                    fileName = Path.GetFileName(files[f].FileName);
                    if (string.IsNullOrEmpty(fileName)) continue;
                    pi = t.GetProperty(f);
                    // 2015/07/31 為避免檔案名稱相同追加fileNameBefore來區別
                    pi.SetValue(obj, string.IsNullOrEmpty(fileNameBefore) ? fileName : fileNameBefore + "-" + fileName, null);
                }
            }
            catch (Exception ex)
            {
                LogModel.addStaticErrorLog(ex);
                Debug.Print(ex.Message);
            }

            return obj;
        }

    }
}