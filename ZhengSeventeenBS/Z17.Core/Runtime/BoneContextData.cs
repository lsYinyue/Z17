using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z17.Core.Runtime
{
    public class BoneContextData
    {
        [ThreadStatic]
        private static ConcurrentDictionary<string, object> _items;
        private static ConcurrentDictionary<string, object> Items
        {
            get
            {
                if (_items == null)
                    _items = new ConcurrentDictionary<string, object>();
                return _items;
            }
        }
        public T Get<T>(string name, T defaultValue = default(T))
        {
            return Items.ContainsKey(name) ? ((T)Items[name]) : defaultValue;
        }
        public void Set<T>(string name, T value)
        {
            Items.AddOrUpdate(name, value, (string k, object v) => value);
        }
        public void Clear()
        {
            Items.Clear();
        }
    }
}
