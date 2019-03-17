using System;

namespace FlightBooking.Core
{
  public class PassengerRule
  {
    public PassengerType Type { get; set; }
    public int SeatsTaken { get; set; }
    public int ExpectedBaggage { get; set; }
    public Func<Passenger, FlightRoute, double> Profit { get; set; }
    public Func<Passenger, FlightRoute, int> PointsAccrued { get; set; }
    public Func<Passenger, FlightRoute, int> PointsRedeemed { get; set; }
  }
}
