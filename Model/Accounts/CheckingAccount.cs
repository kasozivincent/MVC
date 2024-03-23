namespace BlazorApp1.Model.Accounts;

public abstract class CheckingAccount : BankAccount
{
    protected CheckingAccount(Guid accountNumber, decimal balance, Category category) : base(accountNumber, balance,
        category)
    {
    }

    protected override decimal CollateralRatio()
    {
        return (decimal)(2.0 / 3.0);
    }
}