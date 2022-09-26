using EvalCore.Entities;
using EvalCore.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context)
        {

        }

        public virtual async Task UpdateV2(Client entityToUpdate)
        {
            var entity = this.dbSet.Attach(entityToUpdate);
            entity.State = EntityState.Detached;
            await SaveAsync();
        }
    }
}
