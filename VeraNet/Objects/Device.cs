// -----------------------------------------------------------------------
// <copyright file="Device.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Web.Script.Serialization;

    /// <summary>
    /// Represent a Vera's device.
    /// </summary>
    public class Device : VeraInteractiveObject
    {
        private static Regex jobRx = new Regex(@"<JobID>(\d*)<\/JobID>", RegexOptions.Compiled);

        /// <summary>
        /// Gets the alternative identifier of this device.
        /// </summary>
        /// <value>
        /// The alternative identifier of this device.
        /// </value>
        public string AltId { get; internal set; }
        /// <summary>
        /// Gets the category identifier of this device.
        /// </summary>
        /// <value>
        /// The category identifier of this device.
        /// </value>
        public int CategoryId { get; internal set; }
        /// <summary>
        /// Gets the sub category identifier of this device.
        /// </summary>
        /// <value>
        /// The sub category identifier of this device.
        /// </value>
        public int SubCategoryId { get; internal set; }
        /// <summary>
        /// Gets the parent identifier of this device.
        /// </summary>
        /// <value>
        /// The parent identifier of this device.
        /// </value>
        public int ParentId { get; internal set; }
        /// <summary>
        /// Gets the battery level of this device.
        /// </summary>
        /// <value>
        /// The battery level of this device.
        /// </value>
        public int BatteryLevel { get; internal set; }

        /// <summary>
        /// Gets the category  of this device.
        /// </summary>
        /// <value>
        /// The category of this device.
        /// </value>
        public Category Category
        {
            get
            {
                if (this.VeraController != null)
                {
                    return this.VeraController.Categories.Where(c => c.Id == this.CategoryId).FirstOrDefault();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the sub category of this device.
        /// </summary>
        /// <value>
        /// The sub category of this device.
        /// </value>
        public Category SubCategory
        {
            get
            {
                if (this.VeraController != null)
                {
                    return this.VeraController.Categories.Where(c => c.Id == this.SubCategoryId).FirstOrDefault();
                }
                return null;
            }
        }

        /// <summary>
        /// Gets the parent device of this device.
        /// </summary>
        /// <value>
        /// The parent device of this device.
        /// </value>
        public Device Parent
        {
            get
            {
                if (this.VeraController != null)
                {
                    return this.VeraController.Devices.Where(d => d.Id == this.ParentId).FirstOrDefault();
                }
                return null;
            }
        }

        internal override void InitializeProperties(Dictionary<string, object> values)
        {
            base.InitializeProperties(values);
            this.AltId = values["altid"].ToString();
            this.CategoryId = Convert.ToInt32(values["category"]);
            this.SubCategoryId = Convert.ToInt32(values["subcategory"]);
            this.ParentId = Convert.ToInt32(values["parent"]);
            this.BatteryLevel = values.ContainsKey("batterylevel") ? Convert.ToInt32(values["batterylevel"]) : Int32.MinValue;
        }

        internal override void UpdateProperties(Dictionary<string, object> values)
        {
            base.UpdateProperties(values);
            this.UpdateProperty(values, "altid", "AltId", (v) => { this.AltId = v.ToString(); return true; });
            this.UpdateProperty(values, "category", "CategoryId", (v) => { this.CategoryId = Convert.ToInt32(v); return true; });
            this.UpdateProperty(values, "subcategory", "SubCategoryId", (v) => { this.SubCategoryId = Convert.ToInt32(v); return true; });
            this.UpdateProperty(values, "parent", "ParentId", (v) => { this.ParentId = Convert.ToInt32(v); return true; });
            this.UpdateProperty(values, "batterylevel", "BatteryLevel", (v) => { this.BatteryLevel = Convert.ToInt32(v); return true; });
        }

        protected bool SetAction(string serviceId, string action, string argument, object value)
        {
            return this.DataRequest(DataRequestAction.Action, new Dictionary<string, string>()
            {
                { "serviceId", serviceId },
                { "DeviceNum", this.Id.ToString() },
                { "action", action },
                { argument, value.ToString() }
            }).Contains("<u:SetTargetResponse xmlns:u=\"" + serviceId + "\">");
        }

        protected bool SetActionAndWaitJob(string serviceId, string action, string argument, object value)
        {
            var regex = jobRx.Match(this.DataRequest(DataRequestAction.Action, new Dictionary<string, string>()
            {
                { "serviceId", serviceId },
                { "DeviceNum", this.Id.ToString() },
                { "action", action },
                { argument, value.ToString() }
            }));
            if (regex.Success)
            {
                int limit = 0;
                Dictionary<string, object> jsonResponse;
                do
                {
                    if (limit++ > 0)
                    {
                        Thread.Sleep(1000);
                    }
                    jsonResponse = new JavaScriptSerializer().DeserializeObject(this.DataRequest(DataRequestAction.JobStatus, new Dictionary<string, string>()
                    {
                        {  "job",  regex.Groups[1].Value },
                        {  "plugin", "zwave" }
                    })) as Dictionary<string, object>;
                }
                while ((jsonResponse == null || !jsonResponse.ContainsKey("status") || jsonResponse["status"].ToString() == "0" || jsonResponse["status"].ToString() == "1") && limit < 5);
                return (jsonResponse != null && jsonResponse.ContainsKey("status") && jsonResponse["status"].ToString() == "4");
            }
            else
            {
                return false;
            }
        }
        
        protected bool SetVariable(string serviceId, string name, string value)
        {
            this.DataRequest(DataRequestAction.VariableSet, new Dictionary<string, string>()
            {
                { "serviceId", serviceId },
                { "DeviceNum", this.Id.ToString() },
                { "Variable", name },
                { "Value", value }
            });
            return true;
        }
    }
}
