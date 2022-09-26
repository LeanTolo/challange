using EvalCore.Entities;
using EvalCore.Interfaces.Repositories;
using EvalCore.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class ClientService : BaseService<Client>, IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IBaseRepository<Client> _baseRepository, IClientRepository clientRepository) : base(_baseRepository)
        {
            _clientRepository = clientRepository;
        }
        public async Task UpdateV2(int id, Client entityToUpdate)
        {
            await _clientRepository.Update(entityToUpdate);
        }
    }
}
