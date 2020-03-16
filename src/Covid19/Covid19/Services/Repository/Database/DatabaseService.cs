using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid19.Services.Repository.Database
{
    public class DatabaseService
    {
        static DatabaseService _instance;
        
        public static DatabaseService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DatabaseService();

                return _instance;
            }
        }

        public async Task<IEnumerable<CountryCaseDb>> GetMyFavorites()
        {
            var realm = await Realm.GetInstanceAsync();
            return realm.All<CountryCaseDb>();
        }

        public async Task AddToFavorites(string country)
        {
            var realm = await Realm.GetInstanceAsync();

            await realm.WriteAsync(tempRealm =>
            {
                tempRealm.Add(new CountryCaseDb
                {
                    Country = country
                });
            });
        }

        public async Task RemoveFromFavorites(string country)
        {
            var realm = await Realm.GetInstanceAsync();

            await realm.WriteAsync(tempRealm =>
            {
                var countryToDelete = tempRealm.All<CountryCaseDb>().First(b => b.Country == country);
                tempRealm.Remove(countryToDelete);
            });
        }
    }
}
