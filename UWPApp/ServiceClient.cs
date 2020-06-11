using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UWPApp
{
    public static class ServiceClient
    {
        #region ##### CATEGORY REQUESTS #####

        #region ### CATEGORY RETRIEVE ###
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
        #endregion

        #endregion

        #region ##### ITEM REQUESTS #####

        #region ### GET ITEM ###
        internal async static Task<List<clsItem>> GetItemsAsync(string prCategoryName)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<List<clsItem>>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/electrify/getitems?Category=" + prCategoryName));
        }

        internal async static Task<clsItem> GetItemAsync(string prId)
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<clsItem>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/electrify/getitem?Id=" + prId));
        }
        #endregion

        #region ### PUT ITEM ###
        internal async static Task<string> UpdateItemQuantityAsync(clsItem prItem)
        {
            return await InsertOrUpdateAsync(prItem, "http://localhost:60064/api/electrify/PutItemQuantity", "PUT");
        }
        #endregion

        #endregion

        #region ##### ORDER REQUESTS ####

        #region ### POST ORDER ###
        internal async static Task<string> InsertOrderAsync(clsOrder prOrder)
        {
            return await InsertOrUpdateAsync(prOrder, "http://localhost:60064/api/electrify/PostOrder", "POST");
        }
        #endregion

        #endregion

        #region ##### GENERIC METHODS #####
        private async static Task<string> InsertOrUpdateAsync<TItem>(TItem prItem, string prUrl, string prRequest)
        {
            using (HttpRequestMessage lcReqMessage = new HttpRequestMessage(new HttpMethod(prRequest), prUrl))
            using (lcReqMessage.Content = new StringContent(JsonConvert.SerializeObject(prItem), Encoding.UTF8, "application/json"))
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.SendAsync(lcReqMessage);
                return await lcRespMessage.Content.ReadAsStringAsync();
            }
        }
        #endregion      
    }
}
