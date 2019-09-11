using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CubiscanUpload.com.netsuite.webservices;
using System.Net;
using System.Web.Script.Serialization;

namespace CubiscanUpload
{
    public class Item
    {
        public string itemName { get; set; }
        public string itemId { get; set; }
        /*
        public string itemid { get; set; }
        public string length { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        */

        public InventoryItem itemRecord { get; set; }

        public Item(string itemName)
        {
            this.itemName = itemName;
        }

        public bool getItemId()
        {
            var webClient = new WebClient();
            webClient.Headers.Add("Authorization", "NLAuth nlauth_account=1027615, nlauth_email=lphan@C2wireless.com, nlauth_signature=MinhTrang10");
            webClient.Headers.Add("Content-Type", "application/json");

            JavaScriptSerializer jS = new JavaScriptSerializer();
            dynamic result = "";
            try
            {
                result = jS.DeserializeObject(webClient.DownloadString("https://1027615.restlets.api.netsuite.com/app/site/hosting/restlet.nl?script=763&deploy=1&cmd=id&itemname=" + itemName));
            }
            catch (WebException ex)
            {
                Console.WriteLine("Could not find item. Please enter correct item SKU/UPC "+ex);
            }
            string itemid = result;
            if (result != "")
            {
                this.itemId = itemid;
                return true;
            }
            else
            {
                Console.WriteLine("Could not find item. Please enter correct item SKU/UPC");
                return false;
            }
            
        }

        public bool updateItemDimensions(string length, string width, string height, string weight)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("Authorization", "NLAuth nlauth_account=1027615, nlauth_email=lphan@C2wireless.com, nlauth_signature=MinhTrang10");
            webClient.Headers.Add("Content-Type", "application/json");

            JavaScriptSerializer jS = new JavaScriptSerializer();

            try
            {
                dynamic result = jS.DeserializeObject(webClient.DownloadString(string.Format("https://1027615.restlets.api.netsuite.com/app/site/hosting/restlet.nl?script=763&deploy=1&cmd=dims&itemid={0}&length={1}&width={2}&height={3}&weight={4}", itemId, length, width, height, weight)));
            }
            catch (WebException ex)
            {
                return false;
            }
            return true;


        }
            /*
            public void addItemRecord()
            {

                NetSuiteService service = new NetSuiteService();
                service.CookieContainer = new CookieContainer();
                service.applicationInfo = new ApplicationInfo();
                service.applicationInfo.applicationId = "9BFADB8A-938A-41D8-838F-F91785AF2ECF";
                NetsuiteUser user = new NetsuiteUser("1027615", "kng@c2wireless.com", "18", "C2Wireless");
                Passport passport = user.prepare(user);
                Status status = service.login(passport).status;


                string itemName = this.itemName;

                SearchStringField objItemName = new SearchStringField();
                objItemName.searchValue = itemName;
                objItemName.@operator = SearchStringFieldOperator.@is;
                objItemName.operatorSpecified = true;
                ItemSearch objItemSearch = new ItemSearch();
                objItemSearch.basic = new ItemSearchBasic();
                objItemSearch.basic.itemId = objItemName;

                SearchPreferences searchPreferences = new SearchPreferences();
                searchPreferences.bodyFieldsOnly = true;
                service.searchPreferences = searchPreferences;

                SearchResult objItemResult = service.search(objItemSearch);

                if (objItemResult.status.isSuccess != true) throw new Exception("Cannot find Item " + itemName + " " + objItemResult.status.statusDetail[0].message);
                if (objItemResult.recordList.Count() != 1) throw new Exception("More than one item found for item " + itemName);

                InventoryItem iRecord = new InventoryItem();

                iRecord = ((InventoryItem)objItemResult.recordList[0]);

                this.itemRecord = new InventoryItem();
                this.itemRecord = iRecord;
            }
            */

            /*
            public bool updateItemDimensions(string length, string width, string height, string weight)
            {
                NetSuiteService service = new NetSuiteService();
                service.CookieContainer = new CookieContainer();
                service.applicationInfo = new ApplicationInfo();
                service.applicationInfo.applicationId = "9BFADB8A-938A-41D8-838F-F91785AF2ECF";
                NetsuiteUser user = new NetsuiteUser("1027615", "kng@c2wireless.com", "18", "C2Wireless");
                Passport passport = user.prepare(user);
                Status status = service.login(passport).status;

                InventoryItem updateItem = new InventoryItem();
                updateItem.internalId = itemId;
                StringCustomFieldRef itemLength = new StringCustomFieldRef();
                itemLength.scriptId = "custitem_dims_length";
                itemLength.value = length;

                StringCustomFieldRef itemWidth = new StringCustomFieldRef();
                itemWidth.scriptId = "custitem_dims_width";
                itemWidth.value = width;

                StringCustomFieldRef itemHeight = new StringCustomFieldRef();
                itemHeight.scriptId = "custitem_dims_height";
                itemHeight.value = height;

                CustomFieldRef[] customFieldRef = new CustomFieldRef[3];
                customFieldRef[0] = itemLength;
                customFieldRef[1] = itemWidth;
                customFieldRef[2] = itemHeight;

                updateItem.customFieldList = customFieldRef;
                updateItem.weight = Convert.ToDouble(weight);

                WriteResponse response = service.update(updateItem);
                Console.WriteLine("Updating Dims...");

                if (response.status.isSuccess)
                {
                    Console.WriteLine("Item dims have been updated.");
                    return true;
                }
                else
                {
                    Console.WriteLine(response.status.statusDetail[0].message);
                    return false;
                }
                */

     }
    
 } 
        