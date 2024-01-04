using Models.Models.Enuns;

namespace Models.Models;

public class Transaction
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Amout { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public PaymentType? PaymentType { get; set; }
    public TransactionType TransactionType { get; set; }
    public TitleValue TitleValue { get; set; }
}
