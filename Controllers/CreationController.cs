using BlazorApp1.Model;
using BlazorApp1.Views;

namespace BlazorApp1.Controllers;

public class CreationController : IBankObserver
{
    private readonly Bank _bank;
    private CreationView _view = default!;

    public CreationController(Bank bank)
    {
        _bank = bank;
        _bank.AttachBankObserver(BankEvent.NewAccount, this);
    }

    public void Update(BankEvent bankEvent, BankAccount bankAccount, decimal amount)
    {
        _view.SetTitle($"Account {bankAccount.AccountNumber} created");
    }

    public void SetView(CreationView view)
    {
        _view = view;
    }

    public void CreateAccount(bool category, string accountType)
    {
        var accountCategory = category ? Category.Foreign : Category.Domestic;
        _bank.CreateAccount(accountCategory, accountType);
    }
}