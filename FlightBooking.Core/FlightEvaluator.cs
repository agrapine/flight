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

    public ScheduledFlightSummary Evaluate(ScheduledFlight flight)
    {
      var summary = new ScheduledFlightSummary
      {
        Title = flight.FlightRoute.Title,
        AvailableSeats = flight.Aircraft.NumberOfSeats,
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

      summary = passengerRule.Aggregate(summary,
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

      summary.CanFly = FlyConditions.Any(x => x.CanFly(summary));
             
      return summary;
    }
  }
}
