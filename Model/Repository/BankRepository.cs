using BlazorApp1.Model.Accounts;

namespace BlazorApp1.Model.Repository;

public class BankRepository : IBankRepository
{
    private readonly Dictionary<Guid, BankAccount> _accounts = new();

    public BankRepository()
    {
        var accountNumber1 = Guid.NewGuid();
        var accountNumber2 = Guid.NewGuid();
        var accountNumber3 = Guid.NewGuid();
        var accountNumber4 = Guid.NewGuid();
        var accountNumber5 = Guid.NewGuid();
        var accountNumber6 = Guid.NewGuid();
        _accounts.Add(accountNumber1, new SavingsAccount(accountNumber1, 80m, Category.Foreign));
        _accounts.Add(accountNumber2, new RegularCheckingAccount(accountNumber2, 200m, Category.Foreign));
        _accounts.Add(accountNumber3, new SavingsAccount(accountNumber3, 100m, Category.Domestic));
        _accounts.Add(accountNumber4, new InterestCheckingAccount(accountNumber4, 20m, Category.Foreign));
        _accounts.Add(accountNumber5, new SavingsAccount(accountNumber5, 70m, Category.Foreign));
        _accounts.Add(accountNumber6, new RegularCheckingAccount(accountNumber6, 2000m, Category.Foreign));
    }

    public void AddAccount(BankAccount bankAccount)
    {
        _accounts.Add(bankAccount.AccountNumber, bankAccount);
    }

    public BankAccount GetAccount(Guid accountNumber)
    {
        return _accounts[accountNumber];
    }

    public List<BankAccount> GetAccounts()
    {
        return _accounts.Values.ToList();
    }
}