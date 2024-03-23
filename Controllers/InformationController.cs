using BlazorApp1.Model;
using BlazorApp1.Views;

namespace BlazorApp1.Controllers;

public class InformationController : IBankObserver
{
    private readonly Bank _bank;
    private InformationView _view = default!;
    private Guid selectedAccountNumber;

    public InformationController(Bank bank)
    {
        _bank = bank;
        _bank.AttachBankObserver(BankEvent.Deposit, this);
        _bank.AttachBankObserver(BankEvent.AddInterest, this);
        _bank.AttachBankObserver(BankEvent.SetForeign, this);
    }

    public void Update(BankEvent bankEvent, BankAccount bankAccount, decimal amount)
    {
        if (bankEvent == BankEvent.SetForeign && bankAccount.AccountNumber == selectedAccountNumber)
            _view.SetCategory(bankAccount.Category);
        else if (bankEvent == BankEvent.AddInterest || bankAccount.AccountNumber == selectedAccountNumber)
            _view.SetBalance(_bank.GetBalance(selectedAccountNumber).ToString());
    }

    public void SetView(InformationView view)
    {
        _view = view;
    }

    public void DepositButton(string depositAmount)
    {
        var amount = decimal.Parse(depositAmount);
        _bank.DepositMoney(selectedAccountNumber, amount);
    }

    public string LoanButton(string loanAmount)
    {
        var amount = decimal.Parse(loanAmount);
        var result = _bank.IsEligibleForLoan(selectedAccountNumber, amount);
        if (result == Status.Approved)
        {
            _bank.DepositMoney(selectedAccountNumber, amount);
        }
        return result.ToString();
    }

    /*public void ForeignButton(string category)
    {
        boolean b = s.equals("Foreign") ? true : false;
        _bank.(current, b);
    }*/

    public void SelectButton(string accountNumber)
    {
        selectedAccountNumber = Guid.Parse(accountNumber);
        _view.SetBalance(_bank.GetBalance(selectedAccountNumber).ToString());
        _view.SetCategory(_bank.GetCategory(selectedAccountNumber));
    }
}