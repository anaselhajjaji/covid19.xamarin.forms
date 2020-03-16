using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Covid19.Services.Repository.Database
{
    public class CountryCaseDb : RealmObject
    {
        [PrimaryKey]
        public string Country { get; set; }
    }
}
