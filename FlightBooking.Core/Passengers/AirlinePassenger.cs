using System;
namespace FlightBooking.Core.Passengers
{
  public class AirlinePassenger : IPassengerRule
  {
    public PassengerType Type => PassengerType.AirlineEmployee;
    public int SeatsTaken => 1;
    public int ExpectedBaggage => 1;
    public Func<Passenger, FlightRoute, double> Profit => (_,__) => 0;
    public Func<Passenger, FlightRoute, int> PointsAccrued => (_,__)=>0;
    public Func<Passenger, FlightRoute, int> PointsRedeemed => (_,__) =>0;
  }
}
