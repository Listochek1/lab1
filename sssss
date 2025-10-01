
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Cafe
{
    //интерфейс приготовления напитков
    public interface ICookable
    {
        public void Prepare(string status)
        {

        }
    }

    //абстрактный класс напитков
    public abstract class Drink 
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
        public virtual void Prepare(List<Orders> orders, string status)
        {
            if (status == "В списке на приготовление")
            {
                var currentOrder = orders[0];
                Console.WriteLine($"(статус напитка:{status})");
                Console.WriteLine("Приготовление напитка");
                status = "Готово";
                Console.WriteLine($"(статус напитка:{status})");
                Console.WriteLine($"Напиток {currentOrder.drink_name}[{currentOrder.size}] для {currentOrder.customer}  готов");
                Console.WriteLine("(Удаление из списка заказов для бариста)");
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
        public string Position = "Официант";
        public string status ;
        //public Waiter()
        //{
        //    Position = "Официант";
        //    string status = "В списке готовки";      
        //}
        public void MakeOrder() {
            
            status = "В списке на приготовление";
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
        public void CreateOrder(List<Orders> orders)
        {
            Waiter waiter = new Waiter();
            List<Drink> menu = new List<Drink>() { new Americano(), new Latte() };
            Console.WriteLine("Меню:");
            foreach (var drink in menu)
            {
                Console.WriteLine(drink.Name);
            }

            Console.WriteLine("Введите название напитка:");
            string drink_name = Console.ReadLine();

            Console.WriteLine("Введите имя клиента:");
            string customer_name = Console.ReadLine();
            string[] sizes = Enum.GetNames(typeof(DrinkSize));
            foreach (var item in sizes)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("Введите размер:");

            string size = Console.ReadLine();
            waiter.MakeOrder();
            string status = waiter.status;
            var order = new Orders(drink_name, size, customer_name, status);
            orders.Add(order);
            Console.WriteLine("Официант добавил заказ в список");
        }
    }




    public class InvalidSizeException : Exception
    {
        public override string Message
        {
            get { return "Ошибка: Неверный размер напитка"; }
        }

        public class InvalidDrinknameException : Exception
        {
            public override string Message
            {
                get { return "Ошибка: такого напитка нет в меню"; }
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
                if ((DrinkSize.Small.ToString() == currentOrder.size || DrinkSize.Medium.ToString() == currentOrder.size || DrinkSize.Large.ToString() == currentOrder.size))
                {
                    if ((currentOrder.drink_name == "Американо" || currentOrder.drink_name == "Латте"))
                    {
                        string status = currentOrder.status;
                        barista.Prepare(orders, status);
                    }
                    else {
                    var ex = new InvalidDrinknameException();
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    var ex = new InvalidSizeException();
                    Console.WriteLine(ex.Message);
                }



                Console.ReadKey();




            }
        }
    }
}
