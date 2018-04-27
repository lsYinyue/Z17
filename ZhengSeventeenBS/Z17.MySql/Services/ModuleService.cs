using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using Z17.MySql.Base;
using Z17.MySql.Entities;

namespace Z17.MySql.Services
{
    public class ModuleService : BaseService<ModuleService>
    {
        /// <summary>
        /// 纯净新增模块
        /// </summary>
        /// <param name="module"></param>
        public virtual void InsertModule(TsResource module)
        {
            using (var db = new Db())
            {
                db.Insert(module);
            }
        }

        /// <summary>
        /// 纯净更新模块
        /// </summary>
        /// <param name="module"></param>
        public virtual void UpdateModule(TsResource module)
        {
            using (var db = new Db())
            {
                db.Update(module);
            }
        }

        /// <summary>
        /// 纯净删除模块
        /// </summary>
        /// <param name="module"></param>
        public virtual void DeleteModule(TsResource module)
        {
            using (var db = new Db())
            {
                db.Delete(module);
            }
        }
    }
}
