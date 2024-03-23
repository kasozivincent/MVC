namespace BlazorApp1.Model.Accounts;

public class RegularCheckingAccount : CheckingAccount
{
    public RegularCheckingAccount(Guid accountNumber, decimal balance, Category category) : base(accountNumber, balance,
        category)
    {
    }

    protected override string AccountType()
    {
        return "Regular Checking Account";
    }

    protected override decimal InterestRate()
    {
        return 0m;
    }
}