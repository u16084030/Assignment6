using AngularReport_Activity6.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using System.Data;
using System.IO;
using System.Dynamic;
using System.Web.Http.Cors;
using System.Net.Http;

namespace AngularReport_Activity6.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReportController : ApiController
    {
        [System.Web.Mvc.Route("api/Report/getReportData")]
        [HttpGet]
        public dynamic getReportData(int flightSelection)
        {
            TravelEntities db = new TravelEntities();
            db.Configuration.ProxyCreationEnabled = false;
            List<PassangerBooking> bookings;

            if(flightSelection == 1) //Local Flights
            {
                bookings = db.PassangerBookings.Include(zz => zz.Person).Include(cc => cc.Flight).Include(xx => xx.Flight.LocalFlight).Include(bb => bb.Flight.Airline).Where(b => db.LocalFlights.Any(aa => aa.FlightNumber == b.FlightNumber)).ToList();
            }

            else if(flightSelection == 2) // International Flight
            {
                bookings = db.PassangerBookings.Include(zz => zz.Person).Include(cc => cc.Flight).Include(xx => xx.Flight.InternationalFlight).Include(bb => bb.Flight.Airline).Where(b => db.InternationalFlights.Any(aa => aa.FlightNumber == b.FlightNumber)).ToList();
            }

            else //all flights
            {
                bookings = db.PassangerBookings.Include(zz => zz.Person).Include(cc => cc.Flight).Include(bb => bb.Flight.Airline).ToList();
            }

            return getExpandoReport(bookings);
        }

        private dynamic getExpandoReport(List<PassangerBooking> bookings)
        {
            dynamic outObject = new ExpandoObject();
            var airlineList = bookings.GroupBy(zz => zz.Flight.Airline.AirlineName);
            List<dynamic> airlines = new List<dynamic>();

            foreach(var group in airlineList)
            {
                dynamic airline = new ExpandoObject();
                airline.Name = group.Key;
                airline.Popularity = group.Count();
                airlines.Add(airline);

            }
            outObject.Airlines = airlines;

            var flightList = bookings.GroupBy(cc => cc.Flight.DepartureDate);
            List<dynamic> flightGroups = new List<dynamic>();

            foreach(var group in flightList)
            {
                dynamic flight = new ExpandoObject();
                flight.Number = group.Key;
                List<dynamic> flexiBookings = new List<dynamic>();

                foreach(var item in group)
                {
                    dynamic bookingObj = new ExpandoObject();
                    bookingObj.Passanger = item.Person.FirstName + " " + item.Person.LastName;
                    bookingObj.Number = item.BookingNumber;
                    flexiBookings.Add(bookingObj);
                }

                flight.Bookings = flexiBookings;
                flightGroups.Add(flight);
            }

            outObject.Flights = flightGroups;
            return outObject;
 
        }

        //[System.Web.Mvc.Route("api/Report/downloadReport")]
        //[HttpGet]
        //public HttpResponseMessage downloadReport(int flightSelection, int type)
        //{
        //    HttpResponseMessage httpResponseMessage = new HttpResponseMessage();

        //    TravelEntities db = new TravelEntities();
        //    db.Configuration.ProxyCreationEnabled = false;
        //    List<PassangerBooking> bookings;

        //    if (flightSelection == 1) //Local Flights
        //    {
        //        bookings = db.PassangerBookings.Include(zz => zz.Person).Include(cc => cc.Flight).Include(xx => xx.Flight.LocalFlight).Include(bb => bb.Flight.Airline).Where(b => db.LocalFlights.Any(aa => aa.FlightNumber == b.FlightNumber)).ToList();
        //    }

        //    else if (flightSelection == 2) // International Flight
        //    {
        //        bookings = db.PassangerBookings.Include(zz => zz.Person).Include(cc => cc.Flight).Include(xx => xx.Flight.InternationalFlight).Include(bb => bb.Flight.Airline).Where(b => db.InternationalFlights.Any(aa => aa.FlightNumber == b.FlightNumber)).ToList();
        //    }

        //    else //all flights
        //    {
        //        bookings = db.PassangerBookings.Include(zz => zz.Person).Include(cc => cc.Flight).Include(xx => xx.Flight.InternationalFlight).Include(yy => yy.Flight.LocalFlight).Include(bb => bb.Flight.Airline).ToList();
        //    }

        //    return getBookingsReportFile(bookings, type);
        //}

        //private HttpResponseMessage getBookingsReportFile(List<PassangerBooking> bookings, int fileType)
        //{
        //    ReportDocument report = new ReportDocument();
        //    report.Load
        //}
    }
}