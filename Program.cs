//Andrew Barrett CA3 09/04/2023
namespace CA3;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        List<Passenger> passengers = new List<Passenger>();
        string filePath = "faminefiletoanalyse.csv";
        Dictionary<string, List<Passenger>> shipPassengers = new Dictionary<string, List<Passenger>>();
        Dictionary<string, int> occupationCount = new Dictionary<string, int>();
        Dictionary<string, int> ageRangeCount = new Dictionary<string, int>
            {
                { "Infants (<1)", 0 },
                { "Children (1-12)", 0 },
                { "Teenage (13-19)", 0 },
                { "Young adult (20-29)", 0 },
                { "Adult (30-49)", 0 },
                { "Older adult (50+)", 0 },
                { "Unknown", -1 }
            };

        using (StreamReader reader = new StreamReader(filePath)) //read in the file and analyse
        {
            string[] headers = reader.ReadLine().Split(',');

            while (!reader.EndOfStream) //this ensures the entire file is read
            {
                string[] fields = reader.ReadLine().Split(',');//array for various fields

                Passenger passenger = new Passenger();
                passenger.LastName = fields[0];
                passenger.FirstName = fields[1];
                //passenger.Age = int.Parse(fields[2]); i couldnt figure this one out, hence its commented out
                passenger.SexCode = fields[3];
                passenger.OccupationCode = fields[4];
                passenger.NativeCountryCode = fields[5];
                passenger.Destination = fields[6];
                passenger.PortOfEmbarkationCode = fields[7];
                passenger.ManifestId = fields[8];
                passenger.ArrivalDate = DateTime.Parse(fields[9]);

                passengers.Add(passenger);
            }
        }

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Ship Reports");
            Console.WriteLine("2. Occupation Report");
            Console.WriteLine("3. Age Report");
            Console.WriteLine("4. Exit");
            Console.Write("Enter Choice : ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    //ship report
                    Console.WriteLine(ShipReport);
                    break;
                case "2":
                    //occupation report
                    break;
                case "3":
                    //age report
                    break;
                case "4":
                    //exit
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
        void ShipReport(string shipName, List<Passenger> passengers) //ship report method
        {
            var filteredPassengers = passengers.FindAll(p => p.ManifestId.StartsWith(shipName));

            if (filteredPassengers.Count == 0) //safety incase there is no passengers
            {
                Console.WriteLine($"No passengers found for ship {shipName}.");
                return;
            }

            var ship = filteredPassengers[0].ManifestId.Split(' ')[0];
            var count = filteredPassengers.Count;
            var date = filteredPassengers[0].ArrivalDate.ToString("MM/dd/yyyy");

            Console.WriteLine($"{ship} {count} {date} : leaving from {filteredPassengers[0].PortOfEmbarkationCode} Arrived : {date} with {count} passengers");

            foreach (var passenger in filteredPassengers)
            {
                Console.WriteLine($"First Name {passenger.FirstName} : Last Name {passenger.LastName}");
            }
        }
    }
}

class Passenger //class for passengers
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public int Age { get; set; }
    public string SexCode { get; set; }
    public string OccupationCode { get; set; }
    public string NativeCountryCode { get; set; }
    public string Destination { get; set; }
    public string PortOfEmbarkationCode { get; set; }
    public string ManifestId { get; set; }
    public DateTime ArrivalDate { get; set; }
}