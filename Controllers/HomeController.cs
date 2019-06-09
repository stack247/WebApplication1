using Dapper;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var storageConn = ConfigurationManager.AppSettings.Get("StorageConnectionString");
            var storageAccount = CloudStorageAccount.Parse(storageConn);
            var client = storageAccount.CreateCloudBlobClient();
            var container = client.GetContainerReference("index");
            var blobs = container.ListBlobs();

            //var sqlConn = ConfigurationManager.AppSettings.Get("SqlConnectionString");
            //var sites = GetAllSites(sqlConn);

            var model = new HomeModel
            {
                BlobCount = blobs.Count(),
                //SiteCount = sites.Count
                SiteCount = 0
            };

            return View(model);
        }
        public class HomeModel
        {
            public int BlobCount { get; set; }
            public int SiteCount {get;set;}
        }
        private List<Site> GetAllSites(string connectionString)
        {
            using (var conn = GetConnection(connectionString))
            {
                return conn.Query<Site>(@"SELECT * FROM Sites").ToList();
            }
        }

        private SqlConnection GetConnection(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    [DataContract]
    public class Site
    {
        [DataMember(Order = 1)]
        public int ChainId { get; set; }

        [DataMember(Order = 2)]
        public string ChainName { get; set; } // This needs to go away at some point!

        [DataMember(Order = 3)]
        public string SiteName { get; set; }

        [DataMember(Order = 4)]
        public string NPI { get; set; }

        [DataMember(Order = 5)]
        public string FacilityId { get; set; }

        [DataMember(Order = 6)]
        public int SiteNumber { get; set; }

        [DataMember(Order = 7)]
        public string DEA { get; set; }

        [DataMember(Order = 8)]
        public string Street { get; set; }

        [DataMember(Order = 9)]
        public string Street2 { get; set; }

        [DataMember(Order = 10)]
        public string City { get; set; }

        [DataMember(Order = 11)]
        public string State { get; set; }

        [DataMember(Order = 12)]
        public string Zip { get; set; }

        [DataMember(Order = 13)]
        public string Phone { get; set; }

        [DataMember(Order = 14)]
        public string Fax { get; set; }

        [DataMember(Order = 15)]
        public string Email { get; set; }

        [DataMember(Order = 16)]
        public double? Latitude { get; set; }

        [DataMember(Order = 17)]
        public double? Longitude { get; set; }

        [DataMember(Order = 18)]
        public byte ReorderPointLineFormatId { get; set; }

        [DataMember(Order = 19)]
        public bool IsInventoryAggregatedAndManageable { get; set; }

        [DataMember(Order = 20)]
        public bool IsCycleCountEnabled { get; set; }

        [DataMember(Order = 21)]
        public bool IsDeactivated { get; set; }

        [DataMember(Order = 22)]
        public bool IsRedistributionSendingAllowed { get; set; }

        [DataMember(Order = 23)]
        public bool IsRedistributionReceivingAllowed { get; set; }

        [DataMember(Order = 24)]
        public bool IsReturnToVendorAllowed { get; set; }

        [DataMember(Order = 25)]
        public bool IsSiteToSiteAllowed { get; set; }

        [DataMember(Order = 26)]
        public bool IsShippingEnabled { get; set; }

        [DataMember(Order = 27)]
        public bool IsWarehouseSite { get; set; }

        [DataMember(Order = 28)]
        public bool IsROPManualOrderDetectionEnabled { get; set; }

        [DataMember(Order = 29)]
        public bool IsROQManualOrderDetectionEnabled { get; set; }

        [DataMember(Order = 30)]
        public string CentralFillNPI { get; set; }

        [DataMember(Order = 31)]
        public string ShippingRef { get; set; }

        [DataMember(Order = 32)]
        public int PendingTransfersNotificationTypeId { get; set; }

        [DataMember(Order = 33)]
        public int LastModifiedByUserId { get; set; }

        [DataMember(Order = 34)]
        public DateTime LastModifiedUtcDate { get; set; }

        [DataMember(Order = 35)]
        public string TimeZone { get; set; }

        [DataMember(Order = 36)]
        public byte CycleCountDeliveryMethodId { get; set; }
       
        [DataMember(Order = 37)]
        public DateTime CreatedUtcDate { get; set; }

        [DataMember(Order = 38)]
        public int SiteId { get; set; }
    }
}