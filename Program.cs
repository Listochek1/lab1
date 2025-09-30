using System;
using System.Collections.Generic;

namespace Cafe
{
    //интерфейс приготовления напитков
    public interface ICookable
    {
        void Prepare();
    }

    //абстрактный класс напитков
    public abstract class Drink : ICookable
    {
        public string Name;
        public DrinkSize Size;

        public abstract void Prepare();

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

        public override void Prepare()
        {
            Console.WriteLine("Приготовление напитка для клиента\n");
            Console.WriteLine("Приготовление американо: налить эспрессо и добавить горячую воду.");
        }
    }

    public class Latte : Drink
    {
        public Latte()
        {
            Name = "Латте";
        }

        public override void Prepare()
        {
            Console.WriteLine("Приготовление напитка для клиента\n");
            Console.WriteLine("Приготовление латте: налить эспрессо и добавить вспененное молоко.");
        }
    }

    // Перечисление ингредиентов
    public record Ingredient(IngridientType Type, double Quantity);

    public enum IngridientType
    {
        NaturalMilk,
        CoconutMilk,
        AlmondMilk,
        Water,
        Sugar,
        Cinnamon,
        Salt,
        CoffeeBeans
    }

    // Базовый класс персонажа
    public class Person
    {
        public string FirstName;
        public string LastName;
        public string Position;

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

        public void MakeDrink(Drink drink)
        {
            Console.WriteLine("Подготавка к приготовлению напитка");
            Console.WriteLine($"{FirstName} {LastName} начал приготовление :{drink}");
            drink.Prepare();
            Console.WriteLine($"{FirstName} {LastName} закончил приготовление :{drink}");
            Console.WriteLine($"Отдал заказ клиенту");
        }
    }

    public class Waiter : Person
    {
        public Waiter()
        {
            Position = "Официант";
        }

        public void TakeOrder(Customer customer, Drink drink)
        {
            Console.WriteLine($"{Position}:{FirstName} {LastName}");
            Console.WriteLine($"{customer}");
            Console.WriteLine($"{FirstName} {LastName} принимает заказ от {customer}:\n{drink}");
        }
    }

    public class Admin : Person
    {
        public Admin()
        {
            Position = "Администратор";
        }
    }

    public class Customer : Person
    {
        public Customer()
        {
            Position = "Клиент";
        }
    }



    public class Order
    {
        public Customer Customer;
        public Drink Drink;
        public bool IsCompleted = false;

        public Order(Customer customer, Drink drink)
        {
            Customer = customer;
            if (drink == null)
            {

                Console.WriteLine("Ошибка: напиток не может быть пустым");
            }
            else
            {
                Drink = drink;
            }
        }

        public void CompleteOrder()
        {
            IsCompleted = true;
        }

        public override string ToString()
        {
            return $"Заказ: {Drink}  для  {Customer}";
        }
    }

    class Program
    {
        static void Main()
        {
            // Создаем сотрудников и клиента
            var barista = new Barista
            {
                FirstName = "Иван",
                LastName = "Иванов"
            };

            var waiter = new Waiter
            {
                FirstName = "Станислав",
                LastName = "Петров"
            };

            var customer = new Customer
            {
                FirstName = "Александр",
                LastName = "Петренко"
            };

            //меню
            Drink[] menu = { new Americano(), new Latte() };

            //выбор случайного напитка
            var rnd = new Random();
            var chosenDrink = menu[rnd.Next(menu.Length)];
            
            //выбор случайного размера
            DrinkSize[] sizes = (DrinkSize[])Enum.GetValues(typeof(DrinkSize));
            int index = rnd.Next(sizes.Length);
            chosenDrink.Size = sizes[index];
            
            //работа
            Console.WriteLine("Работа кафе:");
            
            waiter.TakeOrder(customer, chosenDrink);

            try
            {
                barista.MakeDrink(chosenDrink);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}
