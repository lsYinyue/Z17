using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z17.Core.Enums
{
    /// <summary>
    /// RBAC资源类型
    /// </summary>
    public enum RbacResourceType
    {
        /// <summary>
        /// 菜单
        /// </summary>
        Menu = 2,
        /// <summary>
        /// 界面元素
        /// </summary>
        Widget = 4,
        /// <summary>
        /// 后台服务（动作）
        /// </summary>
        Action = 8,
        /// <summary>
        /// 数据项
        /// </summary>
        DataItem = 16,
        /// <summary>
        /// 数据列
        /// </summary>
        DataColumn = 32,
        /// <summary>
        /// 移动端菜单
        /// </summary>
        AppMenu = 64
    }
}
