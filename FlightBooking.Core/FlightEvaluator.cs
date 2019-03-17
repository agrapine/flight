using System;
using System.Linq;
using FlightBooking.Core.Report;

namespace FlightBooking.Core
{

  public class FlightEvaluator
  {
    public PassengerRule[] Rules { get; }

    public FlightEvaluator()
    {
      Rules = new PassengerRule[]{
        new PassengerRule
        {
          Type = PassengerType.General,
          SeatsTaken = 1,
          ExpectedBaggage = 1,
          Profit = (p, r) => r.BaseCost,
          PointsAccrued = (p, r) => 0,
          PointsRedeemed = (p, r) => 0,
        },
       new PassengerRule
        {
          Type = PassengerType.LoyaltyMember,
          SeatsTaken = 1,
          ExpectedBaggage = 2,
          Profit = (p,r) => p.IsUsingLoyaltyPoints ? 0 : r.BaseCost,
          PointsAccrued = (p,r) => p.IsUsingLoyaltyPoints ? 0 : r.LoyaltyPointsGained,
          PointsRedeemed = (p,r) => p.IsUsingLoyaltyPoints ? r.PointBaseCost() : 0,
        },
        new PassengerRule
        {
          Type =PassengerType.AirlineEmployee,
          SeatsTaken = 1,
          ExpectedBaggage = 1,
          Profit = (p,r) => 0,
          PointsAccrued = (p,r) => 0,
          PointsRedeemed = (p,r) => 0,
        }};
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
        .Join(Rules,
          x => x.Type,
          x => x.Type,
          (passenger, rule) => new { rule, passenger })
          .ToList();

      if(passengerRule.Count != flight.Passengers.Count)
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


            return s;
          });

      summary.ProfitSurplus = summary.ProfitFromFlight - summary.CostOfFlight;
      summary.CanProceed = summary.ProfitSurplus > 0
      && summary.SeatsTaken < summary.AvailableSeats
      && summary.SeatsTaken / (double)summary.AvailableSeats > summary.MinimumTakeOffPercentage;

      return summary;
    }
  }
}
