using System;

public interface IState
{
    void SelectTicket();
    void InsertMoney(decimal amount);
    void DispenseTicket();
    void CancelTransaction();
}

public class IdleState : IState
{
    private TicketMachine _machine;

    public IdleState(TicketMachine machine)
    {
        _machine = machine;
    }

    public void SelectTicket()
    {
        Console.WriteLine("Билет выбран. Внесите деньги.");
        _machine.SetState(_machine.WaitingForMoneyState);
    }

    public void InsertMoney(decimal amount) =>
        Console.WriteLine("Выберите билет перед внесением денег.");
    public void DispenseTicket() =>
        Console.WriteLine("Нет билета для выдачи.");
    public void CancelTransaction() =>
        Console.WriteLine("Нет активной транзакции для отмены.");
}

public class WaitingForMoneyState : IState
{
    private TicketMachine _machine;

    public WaitingForMoneyState(TicketMachine machine)
    {
        _machine = machine;
    }

    public void SelectTicket() =>
        Console.WriteLine("Билет уже выбран. Внесите деньги.");
    public void InsertMoney(decimal amount)
    {
        _machine.CurrentBalance += amount;
        Console.WriteLine($"Вы внесли {amount:C}. Текущий баланс: {_machine.CurrentBalance:C}.");

        if (_machine.CurrentBalance >= _machine.TicketPrice)
        {
            _machine.SetState(_machine.MoneyReceivedState);
        }
    }
    public void DispenseTicket() =>
        Console.WriteLine("Недостаточно средств.");
    public void CancelTransaction()
    {
        Console.WriteLine("Транзакция отменена. Возврат средств...");
        _machine.ResetTransaction();
        _machine.SetState(_machine.IdleState);
    }
}


public class MoneyReceivedState : IState
{
    private TicketMachine _machine;

    public MoneyReceivedState(TicketMachine machine)
    {
        _machine = machine;
    }

    public void SelectTicket() =>
        Console.WriteLine("Билет уже выбран и оплачен.");
    public void InsertMoney(decimal amount) =>
        Console.WriteLine("Средства уже внесены. Билет будет выдан.");
    public void DispenseTicket()
    {
        Console.WriteLine("Выдача билета...");
        _machine.DispenseTicket();
        _machine.SetState(_machine.IdleState);
    }
    public void CancelTransaction()
    {
        Console.WriteLine("Транзакция отменена. Возврат средств...");
        _machine.ResetTransaction();
        _machine.SetState(_machine.IdleState);
    }
}

public class TicketMachine
{
    public IState IdleState { get; private set; }
    public IState WaitingForMoneyState { get; private set; }
    public IState MoneyReceivedState { get; private set; }

    public IState CurrentState { get; private set; }
    public decimal CurrentBalance { get; set; }
    public decimal TicketPrice { get; private set; } = 50m;

    public TicketMachine()
    {
        IdleState = new IdleState(this);
        WaitingForMoneyState = new WaitingForMoneyState(this);
        MoneyReceivedState = new MoneyReceivedState(this);

        CurrentState = IdleState;
    }

    public void SetState(IState state) => CurrentState = state;

    public void ResetTransaction() => CurrentBalance = 0;

    public void SelectTicket() => CurrentState.SelectTicket();
    public void InsertMoney(decimal amount) => CurrentState.InsertMoney(amount);
    public void DispenseTicket() => CurrentState.DispenseTicket();
    public void CancelTransaction() => CurrentState.CancelTransaction();
}

class Program
{
    static void Main(string[] args)
    {
        var ticketMachine = new TicketMachine();

        ticketMachine.SelectTicket();
        ticketMachine.InsertMoney(20);
        ticketMachine.InsertMoney(30);
        ticketMachine.DispenseTicket();

        ticketMachine.SelectTicket();
        ticketMachine.CancelTransaction();
    }
}
