using Data.DataContexts;
using Data.Entities;

namespace Data.Repositories;
public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context)
{

}
