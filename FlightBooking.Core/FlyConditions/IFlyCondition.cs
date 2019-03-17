namespace FlightBooking.Core.FlyConditions
{
  /// <summary>
  /// Fly condition.
  /// </summary>
  public interface IFlyCondition
  {
    bool CanFly(ScheduledFlightSummary summary);
  }
}
