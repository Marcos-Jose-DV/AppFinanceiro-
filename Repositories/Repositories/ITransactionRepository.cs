using Models.Models;

namespace Repositories.Repositories;

public interface ITransactionRepository
{
    List<Transaction> Get();
    Transaction GetById(int id);
    void Post(Transaction transaction);
    void Put(Transaction transaction);
    void Delete(Transaction transaction);
    void DeleteAll();
}
