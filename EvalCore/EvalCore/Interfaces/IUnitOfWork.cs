using EvalCore.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalCore.Interfaces
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IClientRepository ClientRepository { get; }
        Task<int> CommitAsync();
    }
}
