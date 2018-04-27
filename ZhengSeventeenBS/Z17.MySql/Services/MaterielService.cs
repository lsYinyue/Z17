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
    public class MaterielService : BaseService<MaterielService>
    {
        /// <summary>
        /// 增加原料
        /// </summary>
        /// <param name="Materiel"></param>
        public virtual int InsertMateriel(string token, Tat2010 Materiel)
        {
            using (var db = new Db())
            {
                TsUser tsUser = PermissionService.Proxy.GetTsUserByToken(token);
                Materiel.CCompId = tsUser.CCompany;
                var Id = SequenceService.Proxy.GenerateLocalId();
                Materiel.Id = Id;
                Materiel.CMtrlNo = Id;
                Materiel.TimeStamp = DateTime.Now;
                return db.Insert(Materiel);
            }
        }

        /// <summary>
        /// 更新原料
        /// </summary>
        /// <param name="Token"></param>
        /// <param name="Materiel"></param>
        public virtual int UpdateMateriel(string Token, Tat2010 Materiel)
        {
            using (var db = new Db())
            {
                return db.Update(Materiel);
            }
        }

        /// <summary>
        /// 获取所有原料
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual List<Tat2010> GetMateriels(string token)
        {
            using (var db = new Db())
            {
                TsUser tsUser = PermissionService.Proxy.GetTsUserByToken(token);
                var Materisels = db.GetTable<Tat2010>()
                          .Where(x => x.CCompId.Equals(tsUser.CCompany))
                          .ToList();
                return Materisels;
            }
        }

        /// <summary>
        /// 通过ID获取原料信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public virtual Tat2010 GetMaterielById(string Id)
        {
            using (var db = new Db())
            {
                Tat2010 Materiel = db.GetTable<Tat2010>()
                     .FirstOrDefault(x => x.Id.Equals(Id));
                return Materiel;
            }
        }

        /// <summary>
        /// 删除原料
        /// </summary>
        /// <param name="token"></param>
        /// <param name="Materil"></param>
        public virtual int DeleteMateriel(string token, Tat2010 Materil)
        {
            using (var db = new Db())
            {
                return db.Delete(Materil);
            }
        }
    }
}
