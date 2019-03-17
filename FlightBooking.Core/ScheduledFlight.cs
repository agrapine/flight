using System;
using System.Collections.Generic;
using System.Linq;
using FlightBooking.Core.FlyConditions;
using FlightBooking.Core.Passengers;
using FlightBooking.Core.Report;

namespace FlightBooking.Core
{
  public class ScheduledFlight
  {
    public IPassengerRule[] PassengerRules { get; }
    public IFlyCondition[] FlyConditions { get; }

    public ScheduledFlight(FlightRoute flightRoute)
    {
      FlightRoute = flightRoute;
      Passengers = new List<Passenger>();
      Aircrafts = new List<Plane>();
      FlyConditions = new IFlyCondition[]
      {
        new AirlineFlyCondition(),
        new StrictlyProfitableFlyCondition()
      };

      PassengerRules = new IPassengerRule[]
      {
        new AirlinePassenger(),
        new LoyaltyPassenger(),
        new GeneralPassenger(),
        new DiscountedPassenger(),
      };
    }

    public FlightRoute FlightRoute { get; }
    public List<Plane> Aircrafts { get; }
    public List<Passenger> Passengers { get; }

    public void AddPassenger(Passenger passenger)
    {
      Passengers.Add(passenger);
    }

    public void SetAircraftForRoute(Plane aircraft)
    {
      Aircrafts.Insert(0, aircraft);
    }

    public void AddAircraft(Plane aircraft)
    {
      Aircrafts.Add(aircraft);
    }

    public string GetSummary()
    {
      var summary = Summarize();
      return summary.BuildReport();
    }

    public FlightSummary Summarize()
    {
      if (!Aircrafts.Any())
      {
        throw new Exception("No aircraft available");
      }

      var summary = new FlightSummary
      {
        Title = FlightRoute.Title,
        MinimumTakeOffPercentage = FlightRoute.MinimumTakeOffPercentage,
      };
      var passengerRule = Passengers
        .Join(PassengerRules,
          x => x.Type,
          x => x.Type,
          (passenger, rule) => new { rule, passenger })
          .ToList();

      if (passengerRule.Count != Passengers.Count)
      {
        throw new ArgumentOutOfRangeException();
      }

      summary = passengerRule

        .Aggregate(summary,
          (s, x) =>
          {
            s.SeatsTaken += x.rule.SeatsTaken;
            s.TotalExpectedBaggage += x.rule.ExpectedBaggage;

            s.ProfitFromFlight += x.rule.Profit(x.passenger, FlightRoute);
            s.CostOfFlight += FlightRoute.BaseCost;

            s.TotalLoyaltyPointsAcrrued += x.rule.PointsAccrued(x.passenger, FlightRoute);
            s.TotalLoyaltyPointsRedeemed += x.rule.PointsRedeemed(x.passenger, FlightRoute);

            s.GeneralPassengers += x.passenger.Type == PassengerType.General ? 1 : 0;
            s.LoyaltyPassengers += x.passenger.Type == PassengerType.LoyaltyMember ? 1 : 0;
            s.AirlinePassengers += x.passenger.Type == PassengerType.AirlineEmployee ? 1 : 0;
            s.DiscountedPassengers += x.passenger.Type == PassengerType.Discounted ? 1 : 0;

            return s;
          });



      summary.CanFly = FlyConditions.Any(x => x.CanFly(summary, Aircrafts[0]));
      if (!summary.CanFly)
      {
        summary.AlternativeAircrafts = Aircrafts
          .Skip(1) // skip the designated aircraft
          .Where(plane => FlyConditions.Any(x => x.CanFly(summary, plane)))
          .Select(x => x.Name)
          .ToArray();
      }

      return summary;
    }
  }
}
