using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MLPipeSysNew.Helpers
{
    /*
     * If you have a model (database table) need to save several files and file name design likes file1 , file2 , file3 ...etc .
     * Then you can use the way (maybe you have to modify the detail by yourself) to show your files upload input and show this.
     * Of course I add a delete link for ajax delete (if you need). 
     */



    public static class FileUploadHelper
    {
        /// <summary>
        /// File upload Helper
        /// </summary>
        /// <param name="helper">Helper itself</param>
        /// <param name="path">Http path</param>
        /// <param name="fieldObj">file name value</param>
        /// <param name="type">input doc or img</param>
        /// <param name="seq">the seq of file </param>
        /// <returns></returns>
        public static MvcHtmlString FileUploadAndView(this HtmlHelper helper, string path, string fieldObj, string type, string seq)
        {
            StringBuilder sb = new StringBuilder();

            if (type == "doc")
            {

                if (!string.IsNullOrEmpty(fieldObj))
                {
                    sb.Append("<a href=\"" + path + fieldObj + "\" target=\"_blank\">" + fieldObj + "</a>");
                    sb.Append("<a href=\"#\" class=\"delFile\" id=\"DF_FILE" + seq + "\">(Delete)</a>");
                    sb.Append("<input hidden=\"hidden\" name=\"F_FILE" + seq + "\" value=\"" + fieldObj + "\" />");
                    /*
                    * the html will generate likes this  
                    */
                    //    <a href="http://ursite/subpath/@Model.F_TYPE/@Model.F_FILE4" >@Model.F_FILE4</a>
                    //    <a href="#" class="delFile" id="DF_FILE4">(Delete)</a>
                    //    <input hidden="hidden" name="F_FILE4" value="@Model.F_FILE4" />

                }
                else
                    sb.Append("<input type=\"file\" id=\"F_FILE" + seq + "\" name=\"F_FILE" + seq + "\" />");
                    //    <input type="file" id="F_FILE4" name="F_FILE4" />
            }
            else if (type == "img")
            {
                if (!string.IsNullOrEmpty(fieldObj))
                {
                    sb.Append("<img title=\"\" width=\"500\" src=\"" + path + "/" + fieldObj + "\"");
                    sb.Append("<a href=\"#\" class=\"delFile\" id=\"DF_ICON" + seq + "\">(Delete)</a>");
                    sb.Append("<input hidden=\"hidden\" name=\"F_ICON" + seq + "\" value=\"" + fieldObj + "\" />");
                    /*
                     * the html will generate likes this  
                     */
                    //  <img title="" width="500" src="http://ursite/subpath/@Model.F_ICON" />
                    //  <a href="#" class="delFile" id="DF_ICON">(Delete)</a>
                    //  <input hidden="hidden" name="F_ICON" value="@Model.F_ICON" />
                    
                }
                else
                    sb.Append("<input type=\"file\" id=\"F_ICON" + seq + "\" name=\"F_ICON" + seq + "\" />");
                    //<input type="file" id="F_FILE1" name="F_ICON" />
            }
            else
            {

            }
            return new MvcHtmlString(sb.ToString());
        }
        /// <summary>
        /// File view Helper
        /// </summary>
        /// <param name="helper">Helper itself</param>
        /// <param name="path">Http path</param>
        /// <param name="fieldObj">file name value</param>
        /// <param name="type">input doc or img</param>
        /// <param name="seq">the seq of file</param>
        /// <returns></returns>
        public static MvcHtmlString FileView(this HtmlHelper helper, string path, string fieldObj, string type, string seq)
        {
            StringBuilder sb = new StringBuilder();

            if (type == "doc")
            {

                if (!string.IsNullOrEmpty(fieldObj))
                    sb.Append("<a href=\"" + path + fieldObj + "\" target=\"_blank\">" + fieldObj + "</a>");
            }
            else if (type == "img")
            {
                if (!string.IsNullOrEmpty(fieldObj))
                    sb.Append("<img title=\"\" width=\"500\" src=\"" + path + "/" + fieldObj + "\"");
            }
            else
            {

            }
            return new MvcHtmlString(sb.ToString());
        }
    }
}
