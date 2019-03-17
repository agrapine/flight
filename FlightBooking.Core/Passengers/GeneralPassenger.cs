using System;
namespace FlightBooking.Core.Passengers
{
  public class GeneralPassenger : IPassengerRule
  {
    public PassengerType Type => PassengerType.General;
    public int SeatsTaken => 1;
    public int ExpectedBaggage => 1;
    public Func<Passenger, FlightRoute, double> Profit => (_,r)=> r.BasePrice;
    public Func<Passenger, FlightRoute, int> PointsAccrued => (_,__)=> 0;
    public Func<Passenger, FlightRoute, int> PointsRedeemed => (_,__) => 0;
  }
}
