using BlazorApp1.Controllers;
using BlazorApp1.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Views;

public partial class InformationView
{
    private string _accountNumber = string.Empty;
    private string _approveStatus = string.Empty;
    private string _balance = string.Empty;
    private string _category = string.Empty;
    private string _depositAmount = string.Empty;
    private string _loanAmount = string.Empty;

    [Inject] public InformationController InformationController { get; set; } = default!;

    protected override void OnInitialized()
    {
        InformationController.SetView(this);
    }

    private void SelectAccount()
    {
        InformationController.SelectButton(_accountNumber);
    }

    private void DepositMoney()
    {
        InformationController.DepositButton(_depositAmount);
    }

    private void ApproveLoan()
    {
        _approveStatus = InformationController.LoanButton(_loanAmount);
    }

    public void SetBalance(string balance)
    {
        _balance = balance;
    }

    public void SetCategory(Category category)
    {
        _category = category.ToString();
    }
}