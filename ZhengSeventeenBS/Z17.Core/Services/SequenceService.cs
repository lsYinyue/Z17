using System;
using System.Linq;
using LinqToDB;
using Z17.Core.Base;
using Z17.Core.Entities;
using Z17.Core.Extensions;
using Z17.Core.Helpers;

namespace Z17.Core.Services
{
    public class SequenceService : BaseService<SequenceService>
    {
        /// <summary>
		/// 生成主键(10位):yyMMddHH+2位本地流水(适用于一小时产生100条数据的频率)
		/// </summary>  
        public virtual string GenerateId()
        {
            var result = GenerateLocalId(2, "yyMMddHH");
            return result;
        }

        /// <summary>
		/// 生成主键(18位):yyMMddHHmmss+6位本地流水(适用于一秒产生100万条数据的频率)
		/// </summary> 
		/// <param name="length">序列位数</param>
		/// <param name="format">时间格式化字符串</param>
        public virtual string GenerateLocalId(int length = 6, string format = "yyMMddHHmmss")
        {
            DateTime now = DateTime.Now;
            ulong num = NextVal();
            var result = now.ToString(format) + num.ToString().TakeRight(length, '0');
            return result;
        }

        /// <summary>
		/// 获取有序GUID
		/// </summary> 
        //public virtual Guid NewGuid()
        //{
        //    return GuidHelper.Instance.NewGuid();
        //}

        /// <summary>
        /// 按序列名称获取下一个序列值
        /// </summary>
        /// <param name="seqName">序列名称</param>
        /// <returns>序列值</returns>
        public virtual ulong NextVal(string seqName = "default")
        {
            if (string.IsNullOrEmpty(seqName) || seqName == "default")
            {
                return LocalSeqHelper.Instance.NextVal(seqName);
            }

            ulong result;
            try
            {
                using (var db = GetDbContext())
                {
                    //using (var uow = UnitOfWorkManager.RequiresNew())
                    {
                        int num = 18;
                        ulong num2 = 1uL;

                        var keyId = "SEQ_" + seqName.ToUpper();
                        var tsKeyValue = db.GetTable<TsKeyValue>()
                            .FirstOrDefault(x => x.Id == keyId);
                        if (tsKeyValue == null)
                        {
                            tsKeyValue = new TsKeyValue
                            {
                                Id = keyId,
                                CName = num.ToString(),
                                CCode = seqName,
                                CPCode = "SystemSequence",
                                CValue = "1"
                            };
                            db.Insert(tsKeyValue);
                        }
                        else
                        {
                            num2 = Convert.ToUInt64(tsKeyValue.CValue) + 1uL;
                            var newValue = num2.ToString();
                            int num4 = db.GetTable<TsKeyValue>().Where(x => x.Id == keyId).Set(x => x.CValue, newValue).Update();
                            if (num4 < 1)
                            {
                                throw new Exception(string.Format("序列发生器:{0},出现并发冲突,当前值:{1}，准备重试！", seqName, newValue));
                            }
                        }

                        //uow.Complete();
                        result = num2;
                    }
                }
            }
            catch (Exception ex)
            {
                //base.Log.Error(ex);
                //Thread.Sleep(50);
                result = NextVal(seqName);
            }
            return result;
        }
    }
}
