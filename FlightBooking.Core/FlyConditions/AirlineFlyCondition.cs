namespace FlightBooking.Core.FlyConditions
{
  /// <summary>
  /// Airline fly condition.
  /// ---
  /// More of us
  /// </summary>
  public class AirlineFlyCondition : IFlyCondition
  {
    public bool CanFly(FlightSummary summary, Plane plane)
    {
      return summary.AirlinePassengers / (double)plane.NumberOfSeats > summary.MinimumTakeOffPercentage;
    }
  }
}
