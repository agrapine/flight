using System;
namespace FlightBooking.Core.Passengers
{
  public interface IPassengerRule
  {
    PassengerType Type { get; }
    int SeatsTaken { get; }
    int ExpectedBaggage { get; }
    Func<Passenger, FlightRoute, double> Profit { get; }
    Func<Passenger, FlightRoute, int> PointsAccrued { get; }
    Func<Passenger, FlightRoute, int> PointsRedeemed { get; }
  }
}
