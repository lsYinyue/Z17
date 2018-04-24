using LinqToDB.Mapping;
using System;
using System.ComponentModel.DataAnnotations;
using Z17.Core.Base;

namespace Z17.Core.Entities
{
    /// <summary>
    /// 系统用户表
    /// </summary>
    [Table("TS_USER")]
    [Serializable]
    public class TsUser : BoneEntity<TsUser, string>
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [Display(Name = "用户名称"), Column("C_USERNAME")]
        public virtual string CUserName
        {
            get;
            set;
        }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码"), Column("C_PASSWORD")]
        public virtual string CPassword
        {
            get;
            set;
        }
        /// <summary>
        /// 所属部门
        /// </summary>
        [Display(Name = "所属部门"), Column("C_DEPARTMENT")]
        public virtual string CDepartment
        {
            get;
            set;
        }
        /// <summary>
        /// 管理者
        /// </summary>
        [Display(Name = "管理者"), Column("C_MASTER")]
        public virtual string CMaster
        {
            get;
            set;
        }
        /// <summary>
        /// 用户类型
        /// </summary>
        [Display(Name = "用户类型"), Column("C_USERTYPE")]
        public virtual int CUserType
        {
            get;
            set;
        }
        /// <summary>
        /// 电话
        /// </summary>
        [Display(Name = "电话"), Column("C_PHONE")]
        public virtual string CPhone
        {
            get;
            set;
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱"), Column("C_EMAIL")]
        public virtual string CEmail
        {
            get;
            set;
        }
        /// <summary>
        /// 社保号
        /// </summary>
        [Display(Name = "社保号"), Column("C_SBCARD")]
        public virtual string CSbcard
        {
            get;
            set;
        }
        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别"), Column("C_SEX")]
        public virtual string CSex
        {
            get;
            set;
        }
        /// <summary>
        /// 身份证
        /// </summary>
        [Display(Name = "身份证"), Column("C_IDCARDNO")]
        public virtual string CIdcardNo
        {
            get;
            set;
        }
        /// <summary>
        /// 职务
        /// </summary>
        [Display(Name = "职务"), Column("C_POSITION")]
        public virtual string CPosition
        {
            get;
            set;
        }
        /// <summary>
        /// 岗位
        /// </summary>
        [Display(Name = "岗位"), Column("C_POST")]
        public virtual string CPost
        {
            get;
            set;
        }
        /// <summary>
        /// 教育程度
        /// </summary>
        [Display(Name = "教育程度"), Column("C_EDUCATION")]
        public virtual string CEducation
        {
            get;
            set;
        }
        /// <summary>
        /// 民族
        /// </summary>
        [Display(Name = "民族"), Column("C_NATION")]
        public virtual string CNation
        {
            get;
            set;
        }
        /// <summary>
        /// 籍贯
        /// </summary>
        [Display(Name = "籍贯"), Column("C_NATIVEPLACE")]
        public virtual string CNativeplace
        {
            get;
            set;
        }
        /// <summary>
        /// 政治面貌
        /// </summary>
        [Display(Name = "政治面貌"), Column("C_POLITICSSTATUS")]
        public virtual string CPoliticsstatus
        {
            get;
            set;
        }
        /// <summary>
        /// N禁用Y启用
        /// </summary>
        [Display(Name = "N禁用Y启用"), Column("C_ENABLE")]
        public virtual string CEnable
        {
            get;
            set;
        }
        /// <summary>
        /// 登陆SessionID
        /// </summary>
        [Display(Name = "登陆SessionID"), Column("C_SESSION_ID", Length = 32)]
        public virtual string CSessionId
        {
            get;
            set;
        }
        /// <summary>
        /// 只允许一个客户端登陆
        /// </summary>
        [Display(Name = "只允许一个客户端登陆"), Column("C_ONLY_ONE_CLIENT", Length = 32)]
        public virtual string COnlyOneClient
        {
            get;
            set;
        }
        /// <summary>
        /// 登陆IP地址
        /// </summary>
        [Display(Name = "登陆IP地址"), Column("C_LOGINED_IP", Length = 32)]
        public virtual string CLoginedIp
        {
            get;
            set;
        }
        /// <summary>
        /// 登陆主机名
        /// </summary>
        [Display(Name = "登陆主机名"), Column("C_LOGINED_MACHINE", Length = 512)]
        public virtual string CLoginedMachine
        {
            get;
            set;
        }
        /// <summary>
        /// 登陆时间
        /// </summary>
        [Display(Name = "登陆时间"), Column("D_LOGINED_TIME")]
        public virtual DateTime? DLoginedTime
        {
            get;
            set;
        }
        /// <summary>
        /// Session更新时间
        /// </summary>
        [Display(Name = "Session更新时间"), Column("D_SESSION_UPDATE_TIME")]
        public virtual DateTime? DSessionUpdateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 时间戳
        /// </summary>
        [Display(Name = "时间戳"), Column("C_TIMESTAMP")]
        public virtual DateTime? TimeStamp
        {
            get;
            set;
        }
        public bool IsSessionTimeout(int timeout)
        {
            return (DateTime.Now - this.DSessionUpdateTime.GetValueOrDefault()).TotalMinutes > (double)timeout;
        }
    }
}
