using BingGeocoder;
using System;
using CRM_Final.Business.Models;

namespace CRM_Final.Business.Helpers
{

    public class LatLong
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public static class GeocodeHelper
    {
        public static LatLong Geocode(Address address)
        {
            var geocoder = new BingGeocoderClient("AlpNZq6Dq5_7gFOinuqRbe4DNRB9foCCBotHO0gz1Wn1wbyjwP95SrSp4rIxFels");
            var result = new BingGeocoderResult();
            result = geocoder.Geocode(address.Line1, address.City, address.State, address.PostalCode);

            return new LatLong()
            {
                Latitude = Convert.ToDouble(result.Latitude),
                Longitude = Convert.ToDouble(result.Longitude)
            };
        }
    }
}