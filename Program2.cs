using System;
using System.Collections.Generic;

namespace HotelApp
{
    class Customer
    {
        public string Name { get; set; }
        public int Nights { get; set; }
        public bool RoomService { get; set; }
        public double Cost { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\tWelcome to Sydney Hotel");

            List<Customer> customers = new List<Customer>();

            while (true)
            {
                Customer customer = new Customer();

                // Input: Name
                Console.Write("Enter Customer Name: ");
                customer.Name = Console.ReadLine().Trim();

                // Input: Number of nights with validation
                int nights;
                while (true)
                {
                    Console.Write("Enter Number of Nights (1–20): ");
                    if (int.TryParse(Console.ReadLine(), out nights) && nights >= 1 && nights <= 20)
                    {
                        customer.Nights = nights;
                        break;
                    }
                    Console.WriteLine("Invalid input. Nights must be between 1 and 20.");
                }

                // Input: Room service with validation
                while (true)
                {
                    Console.Write("Room service required? (yes/no): ");
                    string roomServiceInput = Console.ReadLine().Trim().ToLower();
                    if (roomServiceInput == "yes" || roomServiceInput == "no")
                    {
                        customer.RoomService = roomServiceInput == "yes";
                        break;
                    }
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }

                // Calculate cost
                customer.Cost = CalculateCost(customer.Nights, customer.RoomService);

                // Show cost and add to list
                Console.WriteLine($"Total price for {customer.Name} is ${customer.Cost:F2}");
                customers.Add(customer);

                // Exit option
                Console.WriteLine("________________________________________");
                Console.Write("Press 'q' to quit or any other key to continue: ");
                string choice = Console.ReadLine();
                if (choice.ToLower() == "q") break;
                Console.WriteLine("________________________________________");
            }

            // Display summary
            Console.WriteLine("\n\t\t\tSummary of Reservations");
            Console.WriteLine("{0,-20}{1,-10}{2,-15}{3,10}", "Name", "Nights", "Room Service", "Charge ($)");
            foreach (var c in customers)
            {
                Console.WriteLine("{0,-20}{1,-10}{2,-15}{3,10:F2}",
                    c.Name, c.Nights, c.RoomService ? "Yes" : "No", c.Cost);
            }

            // Find max and min spenders
            Customer maxCustomer = customers[0];
            Customer minCustomer = customers[0];

            foreach (var c in customers)
            {
                if (c.Cost > maxCustomer.Cost) maxCustomer = c;
                if (c.Cost < minCustomer.Cost) minCustomer = c;
            }

            Console.WriteLine($"\nCustomer spending the most is {maxCustomer.Name} (${maxCustomer.Cost:F2})");
            Console.WriteLine($"Customer spending the least is {minCustomer.Name} (${minCustomer.Cost:F2})");
        }

        // Method to calculate cost
        static double CalculateCost(int nights, bool roomService)
        {
            double rate = nights <= 3 ? 100 : nights <= 10 ? 80.5 : 75.3;
            double total = nights * rate;
            return roomService ? total * 1.10 : total;
        }
    }
}
