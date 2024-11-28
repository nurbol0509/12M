using System;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; } 

public class Booking
{
    public int Id { get; set; }
    public User User { get; set; }
    public Event Event { get; set; }
    public string Status { get; set; }
}

public void ViewEvents(List<Event> events)
{
    foreach (var ev in events)
    {
        Console.WriteLine($"ID: {ev.Id}, Name: {ev.Name}, Date: {ev.Date}, Location: {ev.Location}");
    }
}

public Booking BookEvent(User user, Event ev)
{
    if (user.Role != "User")
    {
        Console.WriteLine("Только зарегистрированные пользователи могут бронировать мероприятия.");
        return null;
    }

    var booking = new Booking
    {
        Id = new Random().Next(1, 1000),
        User = user,
        Event = ev,
        Status = "Active"
    };
    Console.WriteLine($"Мероприятие '{ev.Name}' успешно забронировано пользователем {user.Name}.");
    return booking;
}

public void CancelBooking(Booking booking)
{
    booking.Status = "Cancelled";
    Console.WriteLine($"Бронирование {booking.Id} отменено.");
}

public Event AddEvent(string name, DateTime date, string location)
{
    var ev = new Event
    {
        Id = new Random().Next(1, 1000),
        Name = name,
        Date = date,
        Location = location
    };
    Console.WriteLine($"Мероприятие '{name}' добавлено.");
    return ev;
}

public void EditEvent(Event ev, string newName, DateTime newDate, string newLocation)
{
    ev.Name = newName;
    ev.Date = newDate;
    ev.Location = newLocation;
    Console.WriteLine($"Мероприятие {ev.Id} обновлено.");
}

public void DeleteEvent(List<Event> events, Event ev)
{
    events.Remove(ev);
    Console.WriteLine($"Мероприятие {ev.Id} удалено.");
}

public void ViewBookings(List<Booking> bookings)
{
    foreach (var booking in bookings)
    {
        Console.WriteLine($"Booking ID: {booking.Id}, Event: {booking.Event.Name}, User: {booking.User.Name}, Status: {booking.Status}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var events = new List<Event>();
        var bookings = new List<Booking>();

        var admin = new User { Id = 1, Name = "Admin", Role = "Admin" };
        var user = new User { Id = 2, Name = "User1", Role = "User" };
        var guest = new User { Id = 3, Name = "Guest", Role = "Guest" };

        var event1 = new Event { Id = 1, Name = "Концерт", Date = DateTime.Now.AddDays(5), Location = "Москва" };
        events.Add(event1);

        Console.WriteLine("Мероприятия для гостя:");
        foreach (var ev in events)
            Console.WriteLine($"ID: {ev.Id}, Name: {ev.Name}, Date: {ev.Date}");

        var booking = new Booking { Id = 1, User = user, Event = event1, Status = "Active" };
        bookings.Add(booking);

        Console.WriteLine("\nВсе бронирования:");
        foreach (var bk in bookings)
            Console.WriteLine($"Booking ID: {bk.Id}, User: {bk.User.Name}, Event: {bk.Event.Name}, Status: {bk.Status}");
    }
}

