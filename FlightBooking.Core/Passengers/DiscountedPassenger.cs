using System;

namespace FlightBooking.Core.Passengers
{
  public class DiscountedPassenger : IPassengerRule
  {
    public PassengerType Type => PassengerType.Discounted;
    public int SeatsTaken => 1;
    public int ExpectedBaggage => 0;
    public Func<Passenger, FlightRoute, double> Profit => (_,r)=> r.BasePrice * 0.5;
    public Func<Passenger, FlightRoute, int> PointsAccrued => (_,__)=> 0;
    public Func<Passenger, FlightRoute, int> PointsRedeemed => (_,__) => 0;
  }
}