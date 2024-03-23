namespace BlazorApp1.Model.Accounts;

public class InterestCheckingAccount : CheckingAccount
{
    public InterestCheckingAccount(Guid accountNumber, decimal balance, Category category) : base(accountNumber,
        balance, category)
    {
    }

    protected override string AccountType()
    {
        return "Interest Checking Account";
    }

    protected override decimal InterestRate()
    {
        return 0.01m;
    }
}