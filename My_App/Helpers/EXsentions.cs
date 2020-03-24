using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_App.Helpers
{
    public static class EXsentions
    {
        public static StringValues Jsonconvert { get; private set; }

        public static void AddApplicationError(this HttpResponse response,string message)
        {
            response.Headers.Add("Application-Error", message);
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Aiiow-Origin", "*");
        }

        public static void AddPagination(this HttpResponse response,
            int currentPage, int itemsPerpage,int totalItems,int TotalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerpage, totalItems, TotalPages);
            var camelcaseformater = new JsonSerializerSettings();
            camelcaseformater.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("pagination", JsonConvert.SerializeObject(paginationHeader, camelcaseformater));
            response.Headers.Add("Access-Control-Expose-Headers", "pagination");
        }
        

        public static int calculatage(this DateTime dateTime)
        {
            var age = DateTime.Today.Year - dateTime.Year;
            if(dateTime.AddYears(age)> DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}
