using LiteDB;
using Models.Models;

namespace Repositories.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly LiteDatabase _database;
    private readonly string collectionName = "Transactions";
    public TransactionRepository(LiteDatabase database)
    {
        _database = database;
    }
    public List<Transaction> Get()
    {
        return _database
              .GetCollection<Transaction>(collectionName)
              .Query()
              .OrderByDescending(x => x.CreatedDate)
              .ToList();
    }

    public Transaction GetById(int id)
    {
        return _database
               .GetCollection<Transaction>(collectionName)
               .FindById(id);
    }

    public void Post(Transaction transaction)
    {
        var col = _database.GetCollection<Transaction>(collectionName);
        col.Insert(transaction);
        col.EnsureIndex(x => x.UpdatedDate);
    }

    public void Put(Transaction transaction)
    {
        var col = _database.GetCollection<Transaction>(collectionName);
        col.Update(transaction);
    }
    public void Delete(Transaction transaction)
    {
        var col = _database.GetCollection<Transaction>(collectionName);
        col.Delete(transaction.Id);
    }

    public void DeleteAll()
    {
        _database.GetCollection(collectionName).DeleteAll();
    }
}
