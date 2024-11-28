using System;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class Cart
{
    public List<Product> Products { get; private set; } = new List<Product>();

    public void AddProduct(Product product)
    {
        Products.Add(product);
        Console.WriteLine($"Товар {product.Name} добавлен в корзину.");
    }

    public decimal CalculateTotal()
    {
        return Products.Sum(p => p.Price);
    }
}

public class Order
{
    public int Id { get; set; }
    public Cart Cart { get; set; }
    public string CustomerName { get; set; }
    public string Address { get; set; }
    public bool IsPaid { get; set; } = false;

    public Order(Cart cart, string customerName, string address)
    {
        Cart = cart;
        CustomerName = customerName;
        Address = address;
    }
}

public class PaymentProcessor
{
    public bool ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Проверка оплаты: сумма {amount}");
        return amount <= 1000; // Условие успешной оплаты
    }
}

public class OrderProcessor
{
    public void ProcessOrder(Order order)
    {
        if (order.IsPaid)
        {
            Console.WriteLine($"Заказ {order.Id} обрабатывается и будет отправлен.");
        }
        else
        {
            Console.WriteLine($"Заказ {order.Id} не может быть обработан, так как он не оплачен.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        var cart = new Cart();
        var product1 = new Product { Id = 1, Name = "Телефон", Price = 500 };
        var product2 = new Product { Id = 2, Name = "Наушники", Price = 100 };

        cart.AddProduct(product1);
        cart.AddProduct(product2);

        Console.WriteLine($"Итоговая сумма: {cart.CalculateTotal()}");

        var order = new Order(cart, "Иван Иванов", "г. Москва, ул. Пушкина");

        var paymentProcessor = new PaymentProcessor();
        order.IsPaid = paymentProcessor.ProcessPayment(cart.CalculateTotal());

        if (order.IsPaid)
        {
            Console.WriteLine("Оплата успешно выполнена.");
        }
        else
        {
            Console.WriteLine("Ошибка оплаты. Попробуйте снова.");
            return;
        }

        var orderProcessor = new OrderProcessor();
        orderProcessor.ProcessOrder(order);
    }
}

