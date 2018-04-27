using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using LinqToDB.Data;
using Z17.MySql.Entities;
using Z17.MySql.Helpers;


namespace Z17.MySql.Base
{
    public class Db : DataContext
    {
        private string _connString;
        public Db(string connString = "default") : base(connString)
        {

        }

        private DataConnection _connection;
        public DataConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = CreateInternal(_connString);
                }
                return _connection;
            }
        }

        private DataConnection CreateInternal(string connName)
        {
            var connSettings = ConfigurationManager.ConnectionStrings[connName];
            if (connSettings == null || string.IsNullOrEmpty(connSettings.ConnectionString))
            {
                throw new Exception(string.Format("请配置数据库连接字符串：{0}", connName));
            }

            string connString = SimpleCipherHelper.Instance.AesDecrypt(connSettings.ConnectionString);
            var dataProvider = DataConnection.GetDataProvider(connName);
            var dataConnection = new DataConnection(dataProvider, connString);
            return dataConnection;
        }
    }
}
