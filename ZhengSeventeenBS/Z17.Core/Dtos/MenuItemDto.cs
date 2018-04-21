using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z17.Core.Entities;

namespace Z17.Core.Dtos
{
    public class MenuItemDto
    {
        [NonSerialized]
        public Type ModuleDeclaringType;

        public string Id
        {
            get;
            set;
        }
        public string PId
        {
            get;
            set;
        }
        public string Code
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Order
        {
            get;
            set;
        }
        public string SubPath
        {
            get;
            set;
        }
        public string Path
        {
            get;
            set;
        }
        public string Url
        {
            get;
            set;
        }
        public string QueryString
        {
            get;
            set;
        }
        public DateTime Timestamp
        {
            get;
            set;
        }
        public bool Marked
        {
            get;
            set;
        }
        public virtual string Caption
        {
            get
            {
                string text = string.IsNullOrEmpty(this.Name) ? "" : this.Name.TrimStart("||".ToArray<char>());
                return string.IsNullOrEmpty(this.Code) ? text : (this.Code + "-" + text);
            }
        }
        public virtual string MockUrl
        {
            get
            {
                return this.SubPath + ((!string.IsNullOrEmpty(this.QueryString)) ? string.Format("?query={0}", this.QueryString) : string.Empty);
            }
        }
        /// <summary>
        /// 是否为系统模块菜单
        /// </summary> 
        public bool IsSystemModuleResource()
        {
            return this.SubPath.StartsWith("Z17.UI");
        }
        public static MenuItemDto Map(TsResource rsc)
        {
            return new MenuItemDto
            {
                Id = rsc.Id,
                PId = rsc.CPId,
                Code = rsc.CCode,
                Name = rsc.CName,
                Order = rsc.COrder,
                Path = rsc.CResourcePath,
                SubPath = rsc.CResourceSubPath,
                Url = rsc.CUrl,
                QueryString = rsc.CQueryString,
                Timestamp = rsc.TimeStamp.GetValueOrDefault()
            };
        }
        public MenuItemDto()
        {
            Timestamp = DateTime.Now;
        }
    }
}
