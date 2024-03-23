namespace BlazorApp1.Model;

public interface IObservable
{
    void AttachBankObserver(BankEvent bankEvent, IBankObserver bankObserver);
    void DetachBankObserver(BankEvent bankEvent, IBankObserver bankObserver);
    void NotifyBankObservers(BankEvent bankEvent, BankAccount ba, decimal amount);
}