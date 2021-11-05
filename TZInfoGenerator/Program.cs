using System;
using TimeZoneNames;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace TZInfoGenerator
{
    class Program
    {
        record TimeZoneItem(string CountryCode, string Id, string DisplayName);
        static void Main(string[] args)
        {
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var countries = TZNames.GetCountryNames("en").Where(c => c.Key != "UN" && c.Key != "IL").Select(c => new
            {
                Code = c.Key,
                Name = (c.Key == "PS") ? "Palestine" : c.Value
            });
            var zones = new List<TimeZoneItem>();
            var threshold = new DateTime(2020, 1, 1);
            foreach(var country in countries)
            {
                var countryZones = TZNames.GetTimeZoneIdsForCountry(country.Code, threshold).Select(id => new TimeZoneItem
                (
                    CountryCode: country.Code,
                    Id: id,
                    DisplayName: TZNames.GetDisplayNameForTimeZone(id, "en")
                ));
                zones.AddRange(countryZones);
            }
            
            var uniqueZones = zones.GroupBy(z => z.Id).Select(g => g.Key).ToList();
            uniqueZones.Sort();
            using (StreamWriter s = new StreamWriter(desktop + "/countries.json"))
            {
                s.Write(JsonConvert.SerializeObject(countries, Formatting.Indented));
            }
            using (StreamWriter s = new StreamWriter(desktop + "/zones.json"))
            {
                s.Write(JsonConvert.SerializeObject(zones, Formatting.Indented));
            }
            using (StreamWriter s = new StreamWriter(desktop + "/uniquezones.json"))
            {
                s.Write(JsonConvert.SerializeObject(uniqueZones, Formatting.Indented));
            }
        }
    }
}
