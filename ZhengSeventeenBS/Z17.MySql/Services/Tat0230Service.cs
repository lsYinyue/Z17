using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z17.MySql.Base;
using Z17.MySql.Entities;

namespace Z17.MySql.Services
{
    public class Tat0230Service : BaseService<Tat0230Service>
    {
        /// <summary>
        /// 新增库位
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StroeHouse"></param>
        /// <returns></returns>
        public virtual int InsertStoreHouse(string token, Tat0230 StroeHouse)
        {
            using (var db = new Db())
            {
                TsUser tsUser = PermissionService.Proxy.GetTsUserByToken(token);
                StroeHouse.CCompId = tsUser.CCompany;
                var Id = SequenceService.Proxy.GenerateLocalId();
                StroeHouse.Id = Id;
                StroeHouse.TimeStamp = DateTime.Now;
                return db.Insert(StroeHouse);
            }
        }

        /// <summary>
        /// 更新库位信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StoreHouse"></param>
        /// <returns></returns>
        public virtual int EditStoreHouse(string token, Tat0230 StoreHouse)
        {
            using (var db = new Db())
            {
                //TsUser tsUser = PermissionService.Proxy.GetTsUserByToken(token);
                return db.Update(StoreHouse);
            }
        }

        /// <summary>
        /// 删除仓库
        /// </summary>
        /// <param name="token"></param>
        /// <param name="StoreHouse"></param>
        /// <returns></returns>
        public virtual int DeleteStoreHouse(string token, Tat0230 StoreHouse)
        {
            using (var db = new Db())
            {
                return db.Delete(StoreHouse);
            }
        }

        /// <summary>
        /// 获取所有仓库仓位信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual List<Tat0230> GetStoreHouse(string token)
        {
            using (var db = new Db())
            {
                TsUser tsUser = PermissionService.Proxy.GetTsUserByToken(token);
                var StoreHouses = db.GetTable<Tat0230>()
                    .Where(x => x.CCompId.Equals(tsUser.CCompany))
                    .ToList();
                return StoreHouses;
            }
        }

        /// <summary>
        /// 根据Id获取仓库仓位信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual Tat0230 GetStoreHouseById(string Id)
        {
            using (var db = new Db())
            {
                var StoreHouse = db.GetTable<Tat0230>()
                    .FirstOrDefault(x => x.Id.Equals(Id));
                return StoreHouse;
            }
        }
    }
}
