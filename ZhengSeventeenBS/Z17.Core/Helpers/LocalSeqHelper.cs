using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Z17.Core.Base;

namespace Z17.Core.Helpers
{
    public class LocalSeqHelper : BaseHelper<LocalSeqHelper>
    {
        private ulong _seq = 1uL;
        private ulong _record_seq = 0uL;
        private Timer _tmr;
        public ulong NextVal(string seqName = "default")
        {
            bool flag = _tmr == null;
            if (flag)
            {
                _tmr = new Timer(new TimerCallback(TimerCallback), null, 5000, 5000);
            }
            ulong expr_35 = _seq;
            _seq = expr_35 + 1uL;
            return expr_35;
        }
        private void TimerCallback(object state)
        {
            if (_record_seq == _seq) return;

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "sequence.log");
            using (var fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Write))
            {
                _record_seq = _seq;
                byte[] bytes = Encoding.UTF8.GetBytes(_record_seq.ToString());
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
