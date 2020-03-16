using Covid19.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Covid19.Services.Repository.RestApi
{
    public interface ICasesApi
    {
        [Get("/countries")]
        Task<IEnumerable<CountryCase>> GetCountryCases();
    }
}
