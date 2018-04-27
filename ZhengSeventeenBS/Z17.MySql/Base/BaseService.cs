using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Z17.MySql.Base
{
    public abstract class BaseService
    {
        private static DataConnection _connection;

        /// <summary>
        /// 获取数据库连接上下文
        /// </summary> 
        protected virtual DataConnection GetDbContext(string connStr = "default")
        {
            if (_connection == null)
            {
                using (var db = new Db(connStr))
                {
                    _connection = db.Connection;
                }
            }
            return _connection;
        }
    }

    /// <summary>
    /// Service基类
    /// public方法一定要带 virtual 关键字
    /// 需要添加针对非虚方法的处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    //[Obsolete("没写代理")]
    public abstract class BaseService<T> : BaseService where T : BaseService, new()
    {
        public BaseService()
        {

        }

        private static T _proxy = null;
        public static T Proxy => _proxy ?? (_proxy = new T());
    }

}
