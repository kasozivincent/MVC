using System.Text;

namespace BlazorApp1.Model;

public abstract class BankAccount : IComparable<BankAccount>
{
    protected BankAccount(Guid accountNumber, decimal balance, Category category)
    {
        AccountNumber = accountNumber;
        Balance = balance;
        Category = category;
        State = State.Active;
        CreatedOn = DateTime.Now;
    }

    public Guid AccountNumber { get; init; }
    public decimal Balance { get; protected set; }
    public State State { get; set; }
    public Category Category { get; set; }
    public DateTime CreatedOn { get; init; }

    public decimal DepositMoney(decimal amount)
    {
        if (State != State.Active)
            throw new Exception("Account is not active");
        Balance += amount;
        return Balance;
    }

    public decimal WithDrawMoney(decimal amount)
    {
        if (State != State.Active)
            throw new Exception("Account is not active");
        if (amount > Balance)
            throw new Exception("Insufficient Funds...");
        Balance -= amount;
        return Balance;
    }

    public int CompareTo(BankAccount? other)
    {
        if (Balance > other!.Balance)
            return 1;
        if (Balance < other!.Balance)
            return -1;
        return 0;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"{AccountType()}, {AccountNumber} -- Balance: {Balance} -- Category: {Category}");
        builder.AppendLine("-----------------------------------------------------------------------------------------------------");
        return builder.ToString();
    }

    public void AddInterest()
    {
        Balance += (int)(Balance * InterestRate());
    }

    public Status HasEnoughCollateral(decimal loanAmount)
    {
        var ratio = CollateralRatio();
        return Balance >= loanAmount * ratio ? Status.Approved : Status.Rejected;
    }

    protected abstract string AccountType();
    protected abstract decimal InterestRate();
    protected abstract decimal CollateralRatio();
}