
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Cafe
{
    //интерфейс приготовления напитков
    public interface ICookable
    {
        public void Prepare(string status) {

        }
    }

    //абстрактный класс напитков
    public abstract class Drink : ICookable
    {
        public string Name { get; set; }
        public DrinkSize Size;



        public override string ToString()
        {
            return $"{Name} {Size}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Drink d)
            {
                return Name == d.Name && Size == d.Size;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Size);
        }
    }

    public enum DrinkSize
    {
        Small,
        Medium,
        Large
    }

    public class Americano : Drink
    {
        public Americano()
        {
            Name = "Американо";
        }


    }

    public class Latte : Drink
    {
        public Latte()
        {
            Name = "Латте";
        }

       
    }

    // Перечисление ингредиентов
   


    //базовый класс персонажа
    public class Person : ICookable
    {
        public string FirstName;
        public string LastName;
        public string Position;
        public virtual void Prepare(List<Orders> orders,string status) {
            if (status == "В списке готовки")
            {
                var currentOrder = orders[0];
                Console.WriteLine($"(статус напитка:{status})");
                Console.WriteLine("Приготовление напитка");
                status = "Готово";
                Console.WriteLine($"(статус напитка:{status})");
                Console.WriteLine($"Напиток {currentOrder.drink_name} для {currentOrder.customer} [{currentOrder.size}] готов");
                orders.Remove(currentOrder);
            }
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName} {Position}";
        }
    }

    public class Barista : Person
    {
        public Barista()
        {
            Position = "Бариста";
        }
        

       
    }

    public class Waiter : Person
    {
        public Waiter()
        {
            Position = "Официант";
        }


    }
    public record Orders(string drink_name, string size, string customer, string status)
    {

    }


    public class Customer : Person
    {
        public Customer()
        {
            Position = "Клиент";
        }
        public void CreateOrder(List<Orders> orders) {
            
            List<Drink> menu = new List<Drink>() {new Americano(),new Latte () };
            Console.WriteLine("Меню:");
            foreach (var drink in menu)
            {
                Console.WriteLine(drink.Name);
            }

            Console.WriteLine("Введите название напитка:");
            string drink_name = Console.ReadLine();

            Console.WriteLine("Введите имя клиента:");
            string customer_name = Console.ReadLine();
            string[] sizes = Enum.GetNames(typeof(DrinkSize)) ;
            foreach (var item in sizes) {
                Console.WriteLine(item);
            }

            Console.WriteLine("Введите размер:");
            
            string size = Console.ReadLine();
            string status = "В списке готовки";
            var order = new Orders (drink_name,size,customer_name,status);
            orders.Add(order);
            Console.WriteLine("Заказ добавлен в список готовки");
        }
    }




public class InvalidSizeException : Exception
{
    public override string Message 
    { 
        get { return "Ошибка: Неверный размер напитка"; } 
    }
}
    class Program
    {
        static void Main()
        {
            Barista barista = new Barista();

            List<Orders> orders = new List<Orders>();
            Customer customer = new Customer();
            customer.CreateOrder(orders);

            var currentOrder = orders[0];
            if (DrinkSize.Small.ToString() == currentOrder.size || DrinkSize.Medium.ToString() == currentOrder.size || DrinkSize.Large.ToString() == currentOrder.size)
            {
                string status = currentOrder.status;
                barista.Prepare(orders, status);
            }
            else {
                var ex = new InvalidSizeException();
                Console.WriteLine(ex.Message);
            }



            Console.ReadKey();
            
            
             
          
        }
    }
}

