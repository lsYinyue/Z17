using AdminWeb.MyClasses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Z17.MySql.Helpers;
using Z17.MySql.Entities;
using Z17.MySql.Services;


namespace AdminWeb.Controllers.Admin.CM3000
{
    public class CM3140Controller : Controller
    {
        // GET: CM3140
        public ActionResult CM3140()
        {
            return View();
        }

        public ActionResult CM3141(string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                ViewData["Materiel"] = GetMaterielsById(Id);
            }
            return View();
        }

        /// <summary>
        /// 保存物料
        /// </summary>
        /// <param name="Materiel"></param>
        /// <returns></returns>
        [HttpPost]
        public int SaveMateriel(string Materiel)
        {
            try
            {
                string Token = GetCookieToken.GetToken();
                int ret = MaterielService.Proxy.InsertMateriel(Token, JsonConvert.DeserializeObject<Tat2010>(Materiel));
                return ret;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新物料
        /// </summary>
        /// <param name="Materiel"></param>
        /// <returns></returns>
        [HttpPost]
        public int EditMateriel(string Materiel) {
            try
            {
                string Token = GetCookieToken.GetToken();
                int ret = MaterielService.Proxy.UpdateMateriel(Token, JsonConvert.DeserializeObject<Tat2010>(Materiel));
                return ret;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 查询所有物料
        /// </summary>
        /// <returns></returns>
        public string GetAllMateriels()
        {
            string Token = GetCookieToken.GetToken();
            var ret = MaterielService.Proxy.GetMateriels(Token);
            return JsonHelper.Instance.ToJson(ret); ;

        }

        /// <summary>
        /// 通过Id获取物料信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetMaterielsById(string Id)
        {
            var ret = MaterielService.Proxy.GetMaterielById(Id);
            return JsonHelper.Instance.ToJson(ret); ;
        }

        /// <summary>
        /// 删除物料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public int DeleteMateriel(string Materiel) {
            try
            {
                string Token = GetCookieToken.GetToken();
                int ret = MaterielService.Proxy.DeleteMateriel(Token, JsonConvert.DeserializeObject<Tat2010>(Materiel));
                return ret;
            }
            catch
            {
                return 0;
            }
        }
    }
}