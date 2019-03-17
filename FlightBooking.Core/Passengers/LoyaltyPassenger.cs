using System;
namespace FlightBooking.Core.Passengers
{
  public class LoyaltyPassenger : IPassengerRule
  {
    public PassengerType Type => PassengerType.LoyaltyMember;
    public int SeatsTaken => 1;
    public int ExpectedBaggage => 2;
    public Func<Passenger, FlightRoute, double> Profit => (p,r) => p.IsUsingLoyaltyPoints ? 0 : r.BasePrice;
    public Func<Passenger, FlightRoute, int> PointsAccrued => (p,r) => p.IsUsingLoyaltyPoints? 0 : r.LoyaltyPointsGained;
    public Func<Passenger, FlightRoute, int> PointsRedeemed => (p,r) => p.IsUsingLoyaltyPoints? r.PointsBasePrice() : 0;
  }
}
