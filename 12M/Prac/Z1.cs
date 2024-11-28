using System;

public interface IState
{
    void SelectCar();
    void ConfirmOrder();
    void CarArrives();
    void StartTrip();
    void CompleteTrip();
    void CancelOrder();
}

public class IdleState : IState
{
    private CarOrderSystem _system;

    public IdleState(CarOrderSystem system)
    {
        _system = system;
    }

    public void SelectCar()
    {
        Console.WriteLine("Вы выбрали автомобиль.");
        _system.SetState(_system.CarSelectedState);
    }

    public void ConfirmOrder() =>
        Console.WriteLine("Нельзя подтвердить заказ без выбора автомобиля.");

    public void CarArrives() =>
        Console.WriteLine("Автомобиль еще не заказан.");

    public void StartTrip() =>
        Console.WriteLine("Поездка не может быть начата.");

    public void CompleteTrip() =>
        Console.WriteLine("Поездка не завершена.");

    public void CancelOrder() =>
        Console.WriteLine("Нет активного заказа для отмены.");
}

public class CarSelectedState : IState
{
    private CarOrderSystem _system;

    public CarSelectedState(CarOrderSystem system)
    {
        _system = system;
    }

    public void SelectCar() =>
        Console.WriteLine("Автомобиль уже выбран.");

    public void ConfirmOrder()
    {
        Console.WriteLine("Заказ подтвержден. Автомобиль в пути.");
        _system.SetState(_system.OrderConfirmedState);
    }

    public void CarArrives() =>
        Console.WriteLine("Автомобиль еще не подтвержден.");

    public void StartTrip() =>
        Console.WriteLine("Поездка не может быть начата.");

    public void CompleteTrip() =>
        Console.WriteLine("Поездка не завершена.");

    public void CancelOrder()
    {
        Console.WriteLine("Заказ отменен.");
        _system.SetState(_system.IdleState);
    }
}


public class OrderConfirmedState : IState
{
    private CarOrderSystem _system;

    public OrderConfirmedState(CarOrderSystem system)
    {
        _system = system;
    }

    public void SelectCar() =>
        Console.WriteLine("Нельзя изменить автомобиль после подтверждения.");

    public void ConfirmOrder() =>
        Console.WriteLine("Заказ уже подтвержден.");

    public void CarArrives()
    {
        Console.WriteLine("Автомобиль прибыл.");
        _system.SetState(_system.CarArrivedState);
    }

    public void StartTrip() =>
        Console.WriteLine("Нельзя начать поездку до прибытия автомобиля.");

    public void CompleteTrip() =>
        Console.WriteLine("Поездка не завершена.");

    public void CancelOrder()
    {
        Console.WriteLine("Заказ отменен.");
        _system.SetState(_system.IdleState);
    }
}


public class CarOrderSystem
{
    public IState IdleState { get; private set; }
    public IState CarSelectedState { get; private set; }
    public IState OrderConfirmedState { get; private set; }
    public IState CarArrivedState { get; private set; }
    public IState InTripState { get; private set; }
    public IState TripCompletedState { get; private set; }
    public IState TripCancelledState { get; private set; }

    public IState CurrentState { get; private set; }

    public CarOrderSystem()
    {
        IdleState = new IdleState(this);
        CarSelectedState = new CarSelectedState(this);
        OrderConfirmedState = new OrderConfirmedState(this);
        CarArrivedState = new CarArrivedState(this);
        InTripState = new InTripState(this);
        TripCompletedState = new TripCompletedState(this);
        TripCancelledState = new TripCancelledState(this);

        CurrentState = IdleState;
    }

    public void SetState(IState state) => CurrentState = state;

    public void SelectCar() => CurrentState.SelectCar();
    public void ConfirmOrder() => CurrentState.ConfirmOrder();
    public void CarArrives() => CurrentState.CarArrives();
    public void StartTrip() => CurrentState.StartTrip();
    public void CompleteTrip() => CurrentState.CompleteTrip();
    public void CancelOrder() => CurrentState.CancelOrder();
}


class Program
{
    static void Main(string[] args)
    {
        var system = new CarOrderSystem();

        system.SelectCar();
        system.ConfirmOrder();
        system.CarArrives();
        system.StartTrip();
        system.CompleteTrip();
    }
}

