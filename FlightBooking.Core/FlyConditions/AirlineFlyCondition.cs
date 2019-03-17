namespace FlightBooking.Core.FlyConditions
{
  /// <summary>
  /// Airline fly condition.
  /// ---
  /// More of us
  /// </summary>
  public class AirlineFlyCondition : IFlyCondition
  {
    public bool CanFly(ScheduledFlightSummary summary)
    {
      return summary.AirlinePassengers / (double)summary.AvailableSeats > summary.MinimumTakeOffPercentage;
    }
  }
}
