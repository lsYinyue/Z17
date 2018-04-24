using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z17.Core.Runtime
{
    public class BoneContextRequest
    {
        private readonly BoneContextData _data = new BoneContextData();
        public string Id
        {
            get
            {
                return _data.Get("Id", string.Empty);
            }
            set
            {
                _data.Set("Id", value);
            }
        }
        public string DataPortalUrl
        {
            get
            {
                return _data.Get("DataPortalUrl", string.Empty);
            }
            set
            {
                _data.Set("DataPortalUrl", value);
            }
        }
        public string Token
        {
            get
            {
                return _data.Get("Token", string.Empty);
            }
            set
            {
                _data.Set("Token", value);
            }
        }
        public ClientInfo UserClient
        {
            get
            {
                return _data.Get("UserClient", new ClientInfo());
            }
            set
            {
                _data.Set("UserClient", value);
            }
        }
        public IBoneIdentity User
        {
            get
            {
                return _data.Get<IBoneIdentity>("User", null);
            }
            set
            {
                _data.Set("User", value);
            }
        }
        internal void Clear()
        {
            _data.Clear();
        }
    }
}
