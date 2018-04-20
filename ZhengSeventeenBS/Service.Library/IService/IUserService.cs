using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Library
{

    public interface ISerice { }

    public interface IUserService: ISerice
    {
        void Add(string user);

        string GetById(string id);
    }
}
