using BlazorApp1.Controllers;
using BlazorApp1.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Views;

public partial class AccountsView : ComponentBase
{
    private List<BankAccount> _accounts = new();
    [Inject] public AccountsController AccountsController { get; set; } = default!;

    protected override void OnInitialized()
    {
        AccountsController.SetView(this);
    }

    public void SetAccounts(List<BankAccount> accounts)
    {
        _accounts = accounts;
        StateHasChanged();
    }

    public void AddInterest()
    {
        AccountsController.AddInterest();
    }
}