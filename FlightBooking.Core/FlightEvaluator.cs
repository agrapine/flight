using System;
using System.Linq;
using FlightBooking.Core.FlyConditions;
using FlightBooking.Core.Passengers;

namespace FlightBooking.Core
{
  public class FlightEvaluator
  {
    public IPassengerRule[] PassengerRules { get; }
    public IFlyCondition[] FlyConditions { get; }

    public FlightEvaluator()
    {
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

    public FlightSummary Evaluate(ScheduledFlight flight)
    {
      if(!flight.Aircrafts.Any())
      {
        throw new Exception("No aircraft available");
      }

      var summary = new FlightSummary
      {
        Title = flight.FlightRoute.Title,
        MinimumTakeOffPercentage = flight.FlightRoute.MinimumTakeOffPercentage,
      };
      var passengerRule = flight.Passengers
        .Join(PassengerRules,
          x => x.Type,
          x => x.Type,
          (passenger, rule) => new { rule, passenger })
          .ToList();

      if (passengerRule.Count != flight.Passengers.Count)
      {
        throw new ArgumentOutOfRangeException();
      }

      summary = passengerRule

        .Aggregate(summary,
          (s, x) =>
          {
            s.SeatsTaken += x.rule.SeatsTaken;
            s.TotalExpectedBaggage += x.rule.ExpectedBaggage;

            s.ProfitFromFlight += x.rule.Profit(x.passenger, flight.FlightRoute);
            s.CostOfFlight += flight.FlightRoute.BaseCost;

            s.TotalLoyaltyPointsAcrrued += x.rule.PointsAccrued(x.passenger, flight.FlightRoute);
            s.TotalLoyaltyPointsRedeemed += x.rule.PointsRedeemed(x.passenger, flight.FlightRoute);

            s.GeneralPassengers += x.passenger.Type == PassengerType.General ? 1 : 0;
            s.LoyaltyPassengers += x.passenger.Type == PassengerType.LoyaltyMember ? 1 : 0;
            s.AirlinePassengers += x.passenger.Type == PassengerType.AirlineEmployee ? 1 : 0;
            s.DiscountedPassengers += x.passenger.Type == PassengerType.Discounted ? 1 : 0;

            return s;
          });



      summary.CanFly = FlyConditions.Any(x => x.CanFly(summary, flight.Aircrafts[0]));
      if (!summary.CanFly)
      {
        summary.AlternativeAircrafts = flight.Aircrafts
          .Skip(1) // skip the designated aircraft
          .Where(plane => FlyConditions.Any(x => x.CanFly(summary, plane)))
          .Select(x => x.Name)
          .ToArray();
      }

      return summary;
    }
  }
}
