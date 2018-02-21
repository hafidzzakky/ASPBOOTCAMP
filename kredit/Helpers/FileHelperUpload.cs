using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace kredit.Helpers
{
    public class FileHelperUpload
    {
        public static string UploadKtp(HttpPostedFileBase file)
        {
            var path = "/Content/Images/Upload/KTP/";
            if (file == null || file.ContentLength == 0)
            {
                return null;
            }
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                var resultPath = Path.Combine(HttpContext.Current.Server.MapPath(path), fileName);
                file.SaveAs(resultPath);
                return path + fileName;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
            }
            return null;
        }
        public static string UploadKK(HttpPostedFileBase file)
        {
            var path = "/Content/Images/Upload/KK/";
            if (file == null || file.ContentLength == 0)
            {
                return null;
            }
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                var resultPath = Path.Combine(HttpContext.Current.Server.MapPath(path), fileName);
                file.SaveAs(resultPath);
                return path + fileName;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
            }
            return null;
        }
        public static string UploadSKKerja(HttpPostedFileBase file)
        {
            var path = "/Content/Images/Upload/SKKerja/";
            if (file == null || file.ContentLength == 0)
            {
                return null;
            }
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                var resultPath = Path.Combine(HttpContext.Current.Server.MapPath(path), fileName);
                file.SaveAs(resultPath);
                return path + fileName;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
            }
            return null;
        }
        public static string UploadTagihanListrik(HttpPostedFileBase file)
        {
            var path = "/Content/Images/Upload/TagihanListrik/";
            if (file == null || file.ContentLength == 0)
            {
                return null;
            }
            try
            {
                var fileName = Path.GetFileName(file.FileName);
                var resultPath = Path.Combine(HttpContext.Current.Server.MapPath(path), fileName);
                file.SaveAs(resultPath);
                return path + fileName;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.TraceError(ex.Message);
            }
            return null;
        }
    }
}