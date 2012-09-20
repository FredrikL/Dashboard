using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace Dashboard.Dev
{
    public class DashboardModule :NancyModule
    {
        public DashboardModule()
        {
            Get["/"] = x =>
                {
                    var model = new Model();
                    var t = new Trains();
                    model.Trains =  t.GetTrains();
                    return View["index", model];
                };
        }
    }

    public class Model
    {
        public IEnumerable<Trains.Deps> Trains { get; set; }
    }

    public class Trains
    {
        public class Deps
        {
            public string Time { get; set; }
            public string EstTime { get; set; }
            public string Destination { get; set; }

        }

        public IEnumerable<Deps> GetTrains()
        {
            var wc = new WebClient();
            string data = wc.DownloadString("http://www5.trafikverket.se/taginfo/WapPages/StationShow.aspx?JF=-1&station=74,Åk&arrivals=0");
            var r = new Regex(">[\\W]+(?<TIME>.+?) till (?<DEST>.+?)<br>");
            
            MatchCollection mc = r.Matches(data);
            foreach (Match m in mc)
            {
                var t = m.Groups["TIME"].Captures[0].ToString();
                var d = m.Groups["DEST"].Captures[0].ToString();
                yield return new Deps { Destination = d, Time = t};
            }
        }
    }
}