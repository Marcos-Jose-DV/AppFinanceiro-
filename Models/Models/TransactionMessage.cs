namespace Models.Models;
public class TransactionMessage
{
    public Transaction Transaction { get; set; } = new();
    public string Message { get; set; }
}
