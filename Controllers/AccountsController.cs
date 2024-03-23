using System.Text;
using BlazorApp1.Model;
using BlazorApp1.Views;

namespace BlazorApp1.Controllers;

public class AccountsController : IBankObserver
{
    private readonly Bank _bank;
    private AccountsView _view;

    public AccountsController(Bank bank)
    {
        _bank = bank;
        _bank.AttachBankObserver(BankEvent.Deposit, this);
        _bank.AttachBankObserver(BankEvent.AddInterest, this);
        _bank.AttachBankObserver(BankEvent.SetForeign, this);
        _bank.AttachBankObserver(BankEvent.NewAccount, this);
    }

    public void Update(BankEvent bankEvent, BankAccount bankAccount, decimal amount)
    {
        RefreshAccounts();
    }

    public void SetView(AccountsView view)
    {
        _view = view;
        RefreshAccounts();
    }

    private void RefreshAccounts()
    {
        var accounts = new List<BankAccount>();
        foreach (var account in _bank)
        {
           accounts.Add(account);
        }
        _view.SetAccounts(accounts);
    }

    public void AddInterest()
    {
        _bank.AddInterest();
    }
}