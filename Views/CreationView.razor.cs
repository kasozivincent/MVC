using BlazorApp1.Controllers;
using BlazorApp1.Model;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Views;

public partial class CreationView : ComponentBase
{
    
    private string _title = "Create New Account";
    private bool _ownership;
    private string accountCategory { get; set; } = default!;

    private List<string> categories = new()
    {
        "Savings",
        "RegularChecking",
        "InterestChecking"
    };

    [Inject] public CreationController CreationController { get; set; } = default!;

    public void SetTitle(string msg)
    {
        _title = msg;
    }

    protected override void OnInitialized()
    {
        CreationController.SetView(this);
    }

    private void CreateAccount()
    {
        CreationController.CreateAccount(_ownership, accountCategory);
    }
    
}