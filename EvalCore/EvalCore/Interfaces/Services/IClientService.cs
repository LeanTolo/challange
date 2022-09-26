using EvalCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalCore.Interfaces.Services
{
    public interface IClientService : IBaseService<Client>
    {
        Task UpdateV2(int id, Client entityToUpdate);

    }
}
