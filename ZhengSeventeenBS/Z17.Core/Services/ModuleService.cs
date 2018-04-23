using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using Z17.Core.Base;
using Z17.Core.Dtos;
using Z17.Core.Entities;

namespace Z17.Core.Services
{
    public class ModuleService : BaseService<ModuleService>
    {
        /// <summary>
        /// 根据ID获取模块信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public virtual MenuItemDto GetById(string id)
        {
            using (var db = GetDbContext())
            {
                return db.GetTable<TsResource>()
                    .Select(x => MenuItemDto.Map(x))
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        /// <summary>
        /// 纯净新增模块
        /// </summary>
        /// <param name="module"></param>
        public virtual int InsertModule(TsResource module)
        {
            using (var db = GetDbContext())
            {
                module.Id = SequenceService.Proxy.GenerateLocalId();
                return db.Insert(module);
            }
        }

        /// <summary>
        /// 纯净更新模块
        /// </summary>
        /// <param name="module"></param>
        public virtual int UpdateModule(TsResource module)
        {
            using (var db = GetDbContext())
            {
                return db.Update(module);
            }
        }

        /// <summary>
        /// 纯净删除模块
        /// </summary>
        /// <param name="module"></param>
        public virtual int DeleteModule(TsResource module)
        {
            using (var db = GetDbContext())
            {
                return db.Delete(module);
            }
        }

        /// <summary>
        /// 根据ID删除模块
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public virtual int DeleteById(string id)
        {
            using (var db = GetDbContext())
            {
                return db.GetTable<TsResource>()
                    .Where(x => x.Id.Equals(id))
                    .Delete();
            }
        }
    }
}
