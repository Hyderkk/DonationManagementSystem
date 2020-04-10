using System;
using System.Collections.Generic;
using System.Text;

namespace KSquare.DMS.Repositories.Interfaces
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork();
    }
}
