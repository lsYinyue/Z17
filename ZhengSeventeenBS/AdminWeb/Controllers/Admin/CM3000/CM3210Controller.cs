using AdminWeb.MyClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Z17.MySql.Entities;
using Z17.MySql.Helpers;
using Z17.MySql.Services;

namespace AdminWeb.Controllers.Admin.CM3000
{
    public class CM3210Controller : Controller
    {
        // GET: CM3210
        public ActionResult CM3210()
        {
            return View();
        }
        public ActionResult CM3211(string Id, string PId)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                ViewData["StoreHouse"] = JsonHelper.Instance.ToJson(Tat0230Service.Proxy.GetStoreHouseById(Id));
            }
            if (!string.IsNullOrEmpty(PId))
            {
                ViewData["PId"] = PId;
            }
            return View();
        }

        /// <summary>
        /// 获取仓库仓位
        /// </summary>
        /// <returns></returns>
        public string GetStoreHouse()
        {
            string Token = GetCookieToken.GetToken();
            var ret = Tat0230Service.Proxy.GetStoreHouse(Token);
            return JsonHelper.Instance.ToJson(ret); ;
        }

        /// <summary>
        ///新增仓库仓位
        /// </summary>
        /// <param name="StoreHouse"></param>
        /// <returns></returns>
        [HttpPost]
        public int AddStoreHouse(Tat0230 StoreHouse)
        {
            try
            {
                string Token = GetCookieToken.GetToken();
                return Tat0230Service.Proxy.InsertStoreHouse(Token, StoreHouse);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 编辑仓库仓位
        /// </summary>
        /// <param name="StoreHouse"></param>
        /// <returns></returns>
        [HttpPost]
        public int EditStoreHouse(Tat0230 StoreHouse)
        {
            try
            {
                string Token = GetCookieToken.GetToken();
                return Tat0230Service.Proxy.EditStoreHouse(Token, StoreHouse);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 删除仓库仓位
        /// </summary>
        /// <param name="StoreHouse"></param>
        /// <returns></returns>
        [HttpPost]
        public int DelStoreHouse(Tat0230 StoreHouse)
        {
            try
            {
                string Token = GetCookieToken.GetToken();
                return Tat0230Service.Proxy.DeleteStoreHouse(Token, StoreHouse);
            }
            catch
            {
                return 0;
            }
        }
    }
}