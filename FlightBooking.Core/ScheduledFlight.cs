using System;
using System.Collections.Generic;
using FlightBooking.Core.Report;

namespace FlightBooking.Core
{
  public class ScheduledFlight
  {
    public ScheduledFlight(FlightRoute flightRoute)
    {
      FlightRoute = flightRoute;
      Passengers = new List<Passenger>();
      Aircrafts = new List<Plane>();
    }

    public FlightRoute FlightRoute { get; }
    public List<Plane> Aircrafts { get; }
    public List<Passenger> Passengers { get; }

    public void AddPassenger(Passenger passenger)
    {
      Passengers.Add(passenger);
    }

    public void SetAircraftForRoute(Plane aircraft)
    {
      Aircrafts.Insert(0, aircraft);
    }

    public void AddAircraft(Plane aircraft)
    {
      Aircrafts.Add(aircraft);
    }

    public string GetSummary()
    {
      var eval = new FlightEvaluator();
      var summary = eval.Evaluate(this);
      return summary.BuildReport();
    }


  }
}
