using EvalCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalCore.Interfaces.Repositories
{
    public interface IClientRepository : IBaseRepository<Client>
    {
        Task UpdateV2(Client entityToUpdate);

    }
}
