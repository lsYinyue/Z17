using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z17.Core.Dtos
{
    [Serializable]
    public class TreeDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id
        {
            get;
            set;
        }
        /// <summary>
        /// 父主键
        /// </summary>
        public string PId
        {
            get;
            set;
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 自定义属性值
        /// </summary>
        public object Tag
        {
            get;
            set;
        }
        public override bool Equals(object obj)
        {
            var treeDto = obj as TreeDto;
            return treeDto != null && treeDto.Id == this.Id;
        }
        public override int GetHashCode()
        {
            if (this.Id == null)
                return -1;
            return this.Id.GetHashCode();
        }
    }
}
