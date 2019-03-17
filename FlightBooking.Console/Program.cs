using System;
using System.Linq;
using FlightBooking.Core;
using FlightBooking.Core.Passengers;

namespace FlightBooking.Console
{
  internal class Program
  {
    private static ScheduledFlight _scheduledFlight;

    private static string[] SampleTest = new string[]
    {
      "add general Steve 30",
      "add general Mark 12",
      "add general James 36",
      "add general Jane 32",
      "add loyalty John 29 1000 true",
      "add loyalty Sarah 45 1250 false",
      "add loyalty Jack 60 50 false",
      "add airline Trevor 47",
      "add general Alan 34",
      "add general Suzy 21",
      "add discounted Zack 62",
      "print summary"
    };

    private static void Main(string[] args)
    {
      SetupAirlineData();

      var stage = SampleTest.ToList();

      string command;
      do
      {
        System.Console.WriteLine("Please enter command.");
        if (stage.Any())
        {
          command = stage.First();
          System.Console.WriteLine(command);
          stage.RemoveAt(0);
        }
        else
          command = System.Console.ReadLine() ?? "";

        var enteredText = command.ToLower();
        if (enteredText.Contains("print summary"))
        {
          System.Console.WriteLine();
          System.Console.WriteLine(_scheduledFlight.GetSummary());
        }
        else if (enteredText.Contains("add general"))
        {
          var passengerSegments = enteredText.Split(' ');
          _scheduledFlight.AddPassenger(new Passenger
          {
            Type = PassengerType.General,
            Name = passengerSegments[2],
            Age = Convert.ToInt32(passengerSegments[3])
          });
        }
        else if (enteredText.Contains("add loyalty"))
        {
          var passengerSegments = enteredText.Split(' ');
          _scheduledFlight.AddPassenger(new Passenger
          {
            Type = PassengerType.LoyaltyMember,
            Name = passengerSegments[2],
            Age = Convert.ToInt32(passengerSegments[3]),
            LoyaltyPoints = Convert.ToInt32(passengerSegments[4]),
            IsUsingLoyaltyPoints = Convert.ToBoolean(passengerSegments[5]),
          });
        }
        else if (enteredText.Contains("add airline"))
        {
          var passengerSegments = enteredText.Split(' ');
          _scheduledFlight.AddPassenger(new Passenger
          {
            Type = PassengerType.AirlineEmployee,
            Name = passengerSegments[2],
            Age = Convert.ToInt32(passengerSegments[3]),
          });
        }
        else if(enteredText.Contains("add discounted"))
        {
          var passengerSegments = enteredText.Split(' ');
          _scheduledFlight.AddPassenger(new Passenger
          {
            Type = PassengerType.Discounted,
            Name = passengerSegments[2],
            Age = Convert.ToInt32(passengerSegments[3])
          });
        }
        else if (enteredText.Contains("exit"))
        {
          Environment.Exit(1);
        }
        else
        {
          System.Console.ForegroundColor = ConsoleColor.Red;
          System.Console.WriteLine("UNKNOWN INPUT");
          System.Console.ResetColor();
        }
      } while (command != "exit");
    }

    private static void SetupAirlineData()
    {
      var londonToParis = new FlightRoute("London", "Paris")
      {
        BaseCost = 50,
        BasePrice = 100,
        LoyaltyPointsGained = 5,
        MinimumTakeOffPercentage = 0.7
      };

      _scheduledFlight = new ScheduledFlight(londonToParis);
      _scheduledFlight.SetAircraftForRoute(new Plane { Id = 123, Name = "Antonov AN-2", NumberOfSeats = 4 });
      _scheduledFlight.AddAircraft(new Plane { Id = 124, Name = "Q400", NumberOfSeats = 12 });
    }
  }
}
