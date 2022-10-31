using NotariusBack.Repository;
using NotariusBack.Repository.Entity;
using NotariusBack.Service.ModelDto;

namespace NotariusBack.Service
{
    public class DealService
    {
        DealRepository repository;
        ClientRepository clientRepository;
        ServiceRepository serviceRepository;

        public DealService()
        {
            repository = new DealRepository();
            clientRepository = new ClientRepository();
            serviceRepository = new ServiceRepository();
        }

        public async Task Add(DealDto dealDto)
        {
            Client client = await clientRepository.Get(dealDto.ClientId);
            Repository.Entity.Service service = await serviceRepository.Get(dealDto.ServiceId);
            Deal deal = new Deal()
            {
                Status = Repository.Entity.Enums.DealStatusEnum.Open,
                Date = DateTime.Now,
                Client = client,
                Service = service,
                Description = dealDto.Description,
                TransactionAmount = null,
                RealPrice = service.Price,
                RealCommission = service.Commission,
            };
            await repository.Add(deal);
        }

        public async Task ToInProgress(int id)
        {
            if ((await repository.GetAll()).FirstOrDefault(t => t.Status == Repository.Entity.Enums.DealStatusEnum.InProgress) == null)
                await repository.ChangeStatus(id, Repository.Entity.Enums.DealStatusEnum.InProgress);
            else throw new Exception();
        }

        public async Task ToDone(int id, int transactionAmount)
        {
            if ((await repository.Get(id)).Status == Repository.Entity.Enums.DealStatusEnum.InProgress)
            {
                await repository.ChangeStatus(id, Repository.Entity.Enums.DealStatusEnum.Done);
                await repository.UpdateTransaction(id, transactionAmount);
            }
        }

        public async Task ToCanceld(int id)
        {
            if ((await repository.Get(id)).Status == Repository.Entity.Enums.DealStatusEnum.InProgress)
            {
                await repository.ChangeStatus(id, Repository.Entity.Enums.DealStatusEnum.Canceld);
            }
        }

        public async Task ToClouse(int id)
        {
            await repository.ChangeStatus(id, Repository.Entity.Enums.DealStatusEnum.Cloused);
        }

        public async Task<Deal> Get()
        {
            return (await repository.GetAll()).FirstOrDefault(t => t.Status == Repository.Entity.Enums.DealStatusEnum.InProgress);
        }

        public async Task<List<Deal>> GetOpen()
        {
            return (await repository.GetAll()).Where(t => t.Status == Repository.Entity.Enums.DealStatusEnum.Open).ToList();
        }

        public async Task<List<Deal>> GetDone()
        {
            return (await repository.GetAll()).Where(t => t.Status == Repository.Entity.Enums.DealStatusEnum.Done).ToList();
        }

        public async Task<List<Deal>> GetAll()
        {
            return (await repository.GetAll()).ToList();
        }

        public async Task<double?> GetSum(DateTime start, DateTime end)
        {
            if (start.Date <= end.Date)
            {
                return (await repository.GetAll()).Where(t => t.Date.Date >= start.Date && t.Date.Date <= end.Date && t.Status == Repository.Entity.Enums.DealStatusEnum.Cloused)
                    .Sum(t => t.RealCommission * t.TransactionAmount + t.RealPrice);
            }
            else
            {
                return (await repository.GetAll()).Where(t => t.Date.Date <= start.Date && t.Date.Date >= end.Date && t.Status == Repository.Entity.Enums.DealStatusEnum.Cloused)
                    .Sum(t => t.RealCommission * t.TransactionAmount + t.RealPrice);
            }
        }
    }
}
