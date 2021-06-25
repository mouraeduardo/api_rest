using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_rest.Domain
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
