namespace BlazorApp1.Model.Accounts;

public class SavingsAccount : BankAccount
{
    public SavingsAccount(Guid accountNumber, decimal balance, Category category) : base(accountNumber, balance,
        category)
    {
    }

    protected override string AccountType()
    {
        return "Savings Account";
    }

    protected override decimal InterestRate()
    {
        return 0.1m;
    }

    protected override decimal CollateralRatio()
    {
        return 0.5m;
    }
}