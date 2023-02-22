using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _22._02._23_HW
{
    internal class Program
    {

        class Check
        {
            public void Give_Check(string buf)
            {
                Console.WriteLine("Withdraw a check?\n1. Yes\t2. No");
                int choose = Convert.ToInt32(Console.ReadLine());
                if (choose == 1)
                {
                    Console.Clear();
                    Console.WriteLine(buf);
                }
            }
        }
        interface Type
        {
            double Convert_Money(double money);
            string ShowType();
        }
        class EUR : Type
        {
            public double Convert_Money(double money)
            {
                return money * 39.15;
            }
            public string ShowType() { return "EUR"; }
        }
        class USD : Type
        {
            public double Convert_Money(double money)
            {
                return money * 36.74;
            }
            public string ShowType() { return "USD"; }
        }
        class GRN : Type
        {
            public double Convert_Money(double money)
            {
                return money;
            }
            public string ShowType() { return "GRN"; }
        }

        class Customer
        {
            public string Name { get; private set; }
            public string Card { get; private set; }
            public double Money { get; private set; }
            public Customer() { }
            public Customer(string name, string card, int money)
            {
                Name = name;
                Card = card;
                Money = money;
            }
            public void Init()
            {
                Console.Write($"Customer: ");
                Name = Console.ReadLine();
                Console.Write($"Card: ");
                Card = Console.ReadLine();
                Console.Write($"Money: ");
                Money = Convert.ToDouble(Console.ReadLine());
            }
            public void Add(double amount) => Money+= amount;
            public void Draw(double amount)
            {
                if (amount > Money)
                {
                    Console.WriteLine("Not enough in card");
                    return;
                }
                Money -= amount;
            }
            public override string ToString()
            {
                return $"Name: {Name}\nCard: {Card}";
            }
        }

        class Bank
        {
            public Customer customer { get; set; }
            public Type type { get; set; }
            public Check check { get; set; }
            private double amount;
            public Bank() { }
            public Bank(Customer customer, Type type, Check check)
            {
                this.customer = customer;
                this.type = type;
                this.check = check;
            }
            public void Init()
            {
                customer = new Customer();
                customer.Init();
            }
            public void Add(double amount) => customer.Add(amount);
            public void Draw()
            {
                Console.Write("Type (1. EUR\t2. USD\t3. GRN): ");
                int buf = Convert.ToInt32(Console.ReadLine());
                switch (buf)
                {
                    case 1:
                        type = new EUR(); break;
                    case 2:
                        type = new USD(); break;
                    case 3:
                        type = new GRN(); break;
                    default:
                        Console.WriteLine("Wrong answer");
                        return;
                }
                Console.Write("Count: ");
                amount = Convert.ToDouble(Console.ReadLine());
                customer.Draw(type.Convert_Money(amount));
            }
            public void Check()
            {
                check = new Check();
                string buf = customer.ToString() + $"Count draw: {amount:f2}\nType: " + type.ToString();
                check.Give_Check(buf);
            }
        }

        static void Main(string[] args)
        {
            Bank bank = new Bank();
            bank.Init();
            bank.Draw();
            bank.Check();
        }
    }
}
