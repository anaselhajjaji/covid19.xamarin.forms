using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19.Services.Repository.RestApi
{

    public class CountryCase
    {
        public string Country { get; set; }
        public int Cases { get; set; }
        public int TodayCases { get; set; }
        public int Deaths { get; set; }
        public int TodayDeaths { get; set; }
        public int Recovered { get; set; }
        public int Critical { get; set; }
        public CountryInfo CountryInfo { get; set; }
    }

}
