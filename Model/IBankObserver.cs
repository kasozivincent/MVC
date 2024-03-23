namespace BlazorApp1.Model;

public interface IBankObserver
{
    void Update(BankEvent bankEvent, BankAccount bankAccount, decimal amount);
}