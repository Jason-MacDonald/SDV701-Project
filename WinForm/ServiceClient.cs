using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WinForm
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

        #region ### ITEM CREATE ###
        internal async static Task<string> InsertItemAsync(clsItem prItem)
        {
            return await InsertOrUpdateAsync(prItem, "http://localhost:60064/api/electrify/PostItem", "POST");
        }
        #endregion

        #region ### ITEM RETRIEVE ###

        // --removed-- Get Item Names
        //internal async static Task<List<string>> GetCategoryItemNamesAsync(string prCategoryName)
        //{
        //    using (HttpClient lcHttpClient = new HttpClient())
        //        return JsonConvert.DeserializeObject<List<string>>
        //            (await lcHttpClient.GetStringAsync("http://localhost:60064/api/electrify/getcategoryitemnames?Category=" + prCategoryName));
        //}      

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

        #region ### ITEM UPDATE ###
        internal async static Task<string> UpdateItemAsync(clsItem prItem)
        {
            return await InsertOrUpdateAsync(prItem, "http://localhost:60064/api/electrify/PutItem", "PUT");
        }

        internal async static Task<string> UpdateItemQuantityAsync(clsItem prItem)
        {
            return await InsertOrUpdateAsync(prItem, "http://localhost:60064/api/electrify/PutItemQuantity", "PUT");
        }
        #endregion

        #region ### ITEM DELETE ###
        internal async static Task<string> DeleteItemAsync(string prId)
        {
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.DeleteAsync
                    ($"http://localhost:60064/api/electrify/DeleteItem?Id={prId}");
                return await lcRespMessage.Content.ReadAsStringAsync();
            }

        }
        #endregion

        #endregion

        #region ##### ORDER REQUESTS ####
        internal async static Task<List<clsOrder>> GetOrdersAsync()
        {
            using (HttpClient lcHttpClient = new HttpClient())
                return JsonConvert.DeserializeObject<List<clsOrder>>
                    (await lcHttpClient.GetStringAsync("http://localhost:60064/api/electrify/GetOrders"));
        }

        internal async static Task<string> DeleteOrderAsync(string prInvoiceNumber)
        {
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.DeleteAsync
                    ($"http://localhost:60064/api/electrify/DeleteOrder?Id={prInvoiceNumber}");
                return await lcRespMessage.Content.ReadAsStringAsync();
            }
        }
        #endregion

        #region ##### GENERIC METHODS #####
        private async static Task<string> InsertOrUpdateAsync<TItem>(TItem prItem, string prUrl, string prRequest)
        {
            using (HttpRequestMessage lcReqMessage = new HttpRequestMessage(new HttpMethod(prRequest), prUrl))
            using (lcReqMessage.Content =
        new StringContent(JsonConvert.SerializeObject(prItem), Encoding.UTF8, "application/json"))
            using (HttpClient lcHttpClient = new HttpClient())
            {
                HttpResponseMessage lcRespMessage = await lcHttpClient.SendAsync(lcReqMessage);
                return await lcRespMessage.Content.ReadAsStringAsync();
            }
        }
        #endregion      
    }
}
