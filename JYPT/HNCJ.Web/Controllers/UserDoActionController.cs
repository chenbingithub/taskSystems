using HNCJ.Common;
using HNCJ.IBLL;
using HNCJ.Model;
using HNCJ.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HNCJ.Web.Controllers
{
    [MyActionFilter(IsCheckUserLogin = false)]
    public class UserDoActionController : Controller
    {
        //
        // GET: /UserDoAction/

        #region 上传图片
        public ActionResult UploadImage()
        {
            HttpPostedFileBase postFile = Request.Files["Filedata"];//接收文件
            //文件保存目录路径
            string name = Request["name"];

            String savePath = "/Upload/Images/";
            string fileName = Path.GetFileName(postFile.FileName);//文件名称
            string fileExt = Path.GetExtension(fileName);//文件的扩展名
            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            savePath += ymd + "/";            
            String dirPath = Request.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            
            string newfileName = Guid.NewGuid().ToString();//新的文件名
            string fileDir = savePath + newfileName + fileExt;//完整的路径
            postFile.SaveAs(Request.MapPath(fileDir));//保存文件
            if (!string.IsNullOrEmpty(name)) {
                System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(name));//指定文件路径
                if (file.Exists)//判断文件是否存在
                {
                    file.Attributes = FileAttributes.Normal;//将文件属性设置为普通,比方说只读文件设置为普通
                    file.Delete();//删除文件
                }
            }
            return Json(new { path = fileDir }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 上传文件
        public ActionResult UploadFile()
        {
            HttpPostedFileBase postFile = Request.Files["Filedata"];//接收文件
            string name = Request["name"];
            //文件保存目录路径
            String savePath = "/Upload/Files/";

            string fileName = Path.GetFileName(postFile.FileName);//文件名称
            if (string.IsNullOrEmpty(name))
            {
                string[] dd = fileName.Split('.');
                name = dd[0];
            }
            string fileExt = Path.GetExtension(fileName);//文件的扩展名
            String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
            savePath += ymd + "/";
            string dirpath = Request.MapPath(savePath);
            if (!Directory.Exists(dirpath))
            {
                Directory.CreateDirectory(dirpath);
            }

            string newfileName = name;//新的文件名
            string fileDir = savePath + newfileName + fileExt;//完整的路径
            postFile.SaveAs(Request.MapPath(fileDir));//保存文件

            return Json(new { path = fileDir, name = newfileName, format = fileExt }, JsonRequestBehavior.AllowGet);
        } 
        #endregion

        public ActionResult Upload()
        {
            HttpContext context = System.Web.HttpContext.Current;
            //上传配置
            int size = 2;           //文件大小限制,单位MB                             //文件大小限制，单位MB
            string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };         //文件允许格式
            //上传图片
            Hashtable info = new Hashtable();
            Uploader up = new Uploader();
            string path = "/upload/images";

            info = up.upFile(context, path + '/', filetype, size);                   //获取上传状态
            return Json(info["url"]); //向浏览器返回数据json数据
        }

      
    }
}
