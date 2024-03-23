namespace BlazorApp1.Model.Repository;

public interface IBankRepository
{
    void AddAccount(BankAccount bankAccount);
    BankAccount GetAccount(Guid accountNumber);
    List<BankAccount> GetAccounts();
}