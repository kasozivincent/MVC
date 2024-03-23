using System.Collections;
using System.Text;
using BlazorApp1.Model.Accounts;
using BlazorApp1.Model.Repository;

namespace BlazorApp1.Model;

public class Bank : IObservable, IEnumerable<BankAccount>
{
    private readonly Dictionary<BankEvent, List<IBankObserver>> _bankObservers;
    private readonly IBankRepository _bankRepository;

    public Bank(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
        _bankObservers = new Dictionary<BankEvent, List<IBankObserver>>();
        foreach (var @event in Enum.GetValues(typeof(BankEvent)))
            _bankObservers[(BankEvent)@event] = new List<IBankObserver>();
        Console.WriteLine(_bankObservers.Count);
    }

    #region Iterator pattern
    public IEnumerator<BankAccount> GetEnumerator()
    {
        return _bankRepository.GetAccounts().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    #endregion
    

    public string CreateAccount(Category category, string type, decimal initialDeposit = 0m)
    {
        var accountNumber = Guid.NewGuid();
        BankAccount bankAccount;
        if (type == "savings")
            bankAccount = new SavingsAccount(accountNumber, initialDeposit, category);
        if (type == "checking")
            bankAccount = new RegularCheckingAccount(accountNumber, initialDeposit, category);
        else
            bankAccount = new InterestCheckingAccount(accountNumber, initialDeposit, category);

        _bankRepository.AddAccount(bankAccount);
        NotifyBankObservers(BankEvent.NewAccount, bankAccount, 0m);
        return $"Your new account number is {accountNumber}";
    }


    public void ChangeAccountState(Guid accountNumber, State state)
    {
        var account = _bankRepository.GetAccount(accountNumber);
        account.State = state;
    }

    public void ChangeAccountCategory(Guid accountNumber, Category category)
    {
        var account = _bankRepository.GetAccount(accountNumber);
        account.Category = category;
    }

    public decimal GetBalance(Guid accountNumber)
    {
        var account = _bankRepository.GetAccount(accountNumber);
        return account.Balance;
    }

    public string DepositMoney(Guid accountNumber, decimal amount)
    {
        var account = _bankRepository.GetAccount(accountNumber);
        var balance = account.DepositMoney(amount);
        NotifyBankObservers(BankEvent.Deposit, account, amount);
        return $"Your new account balance is {balance}";
    }

    public Category GetCategory(Guid accountNumber)
    {
        return _bankRepository.GetAccount(accountNumber).Category;
    }

    public string WithDrawMoney(Guid accountNumber, decimal amount)
    {
        var account = _bankRepository.GetAccount(accountNumber);
        var balance = account.WithDrawMoney(amount);
        return $"Your new account balance is {balance}";
    }

    public Status IsEligibleForLoan(Guid accountNumber, decimal loanAmount)
    {
        var account = _bankRepository.GetAccount(accountNumber);
        var result = account.HasEnoughCollateral(loanAmount);
        return result;
    }

    public string DisplayAccounts()
    {
        var builder = new StringBuilder();

        var accounts = _bankRepository.GetAccounts();
        builder.AppendLine($"The bank has {accounts.Count} accounts");
        foreach (var account in accounts)
            builder.AppendLine(account.ToString());
        return builder.ToString();
    }

    public void AddInterest()
    {
        var accounts = _bankRepository.GetAccounts();
        foreach (var account in accounts) 
            account.AddInterest();
        NotifyBankObservers(BankEvent.AddInterest, null, 0m);
    }

    public void TransferMoney(Guid senderAccountNumber, Guid recipientAccountNumber, decimal amount)
    {
        var senderAccount = _bankRepository.GetAccount(senderAccountNumber);
        var senderBalance = senderAccount.Balance;
        if (amount > senderBalance)
            throw new Exception("The sender doesn't have sufficient balance");

        senderAccount.WithDrawMoney(amount);
        _bankRepository.GetAccount(recipientAccountNumber).DepositMoney(amount);
    }

    #region Observable Props

    public void AttachBankObserver(BankEvent bankEvent, IBankObserver bankObserver)
    {
        _bankObservers[bankEvent].Add(bankObserver);
    }

    public void DetachBankObserver(BankEvent bankEvent, IBankObserver bankObserver)
    {
        _bankObservers[bankEvent].Remove(bankObserver);
    }

    public void NotifyBankObservers(BankEvent bankEvent, BankAccount ba, decimal amount)
    {
        foreach (var obs in _bankObservers[bankEvent]) 
            obs.Update(bankEvent, ba, amount);
    }

    #endregion
}