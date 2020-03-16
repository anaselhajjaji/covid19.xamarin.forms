using AutoMapper;
using Covid19.Models;
using Covid19.Services.Repository.Database;
using Covid19.Services.Repository.RestApi;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Covid19.Services
{
    public class CoronaService
    {
        static CoronaService _instance;
        const string ApiUrl = "https://corona.lmao.ninja";

        public static CoronaService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CoronaService();

                return _instance;
            }
        }

        private CoronaService()
        {
        }

        public async Task<IEnumerable<CountryCaseDto>> GetCountryCases()
        {
            var casesApi = RestService.For<ICasesApi>(ApiUrl);
            var cases = await casesApi.GetCountryCases();

            // Convert to DTO
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CountryCase, CountryCaseDto>());
            var mapper = new Mapper(config);
            var casesDto = mapper.Map<IEnumerable<CountryCase>, IEnumerable<CountryCaseDto>>(cases);

            // Check favorites
            var favs = await DatabaseService.Instance.GetMyFavorites();
            favs.ToList().ForEach(fav => {
                casesDto.Where(c => c.Country == fav.Country).ToList().ForEach(favc => {
                    favc.Favorite = true;
                });
            });

            // FIXME Do this beautifully
            List<CountryCaseDto> groupedList = new List<CountryCaseDto>();
            List<CountryCaseDto> nonFavList = new List<CountryCaseDto>();

            foreach (var group in casesDto.GroupBy(c => c.Favorite))
            {
                if (group.Key)
                {
                    groupedList.AddRange(group);
                }
                else
                {
                    nonFavList.AddRange(group);
                }
            }
            groupedList.AddRange(nonFavList);

            return groupedList;
        }

        public async Task AddToFavorites(string country)
        {
            await DatabaseService.Instance.AddToFavorites(country);
        }

        public async Task RemoveFromFavorites(string country)
        {
            await DatabaseService.Instance.RemoveFromFavorites(country);
        }
    }
}
