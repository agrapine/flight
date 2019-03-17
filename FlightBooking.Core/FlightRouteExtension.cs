using System;

namespace FlightBooking.Core
{
  public static class FlightRouteExtension
  {
    public static int PointBaseCost(this FlightRoute route) => Convert.ToInt32(Math.Ceiling(route.BasePrice));
  }
}
