using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
{
    public static class ServiceClient
    {
        // ##### CATEGORY #####
        internal async static Task<List<string>> GetCategoryNamesAsync()
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<List<string>>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/electrify/getcategorynames"));
        }

        internal async static Task<clsCategory> GetCategoryAsync(string prCategoryName)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<clsCategory>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/electrify/getcategory?Name=" + prCategoryName));
        }

        // ##### ITEM #####
        internal async static Task<List<string>> GetCategoryItemNamesAsync(string prCategoryName)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<List<string>>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/electrify/getcategoryitemnames?Category=" + prCategoryName));
        }
        internal async static Task<List<clsItem>> GetCategoryItemsAsync(string prCategoryName)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<List<clsItem>>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/electrify/getcategoryitems?Category=" + prCategoryName));
        }


            internal async static Task<clsItem> GetItemAsync(string prItemName)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<clsItem>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/electrify/getitem?Name=" + prItemName));
        }
    }
}
