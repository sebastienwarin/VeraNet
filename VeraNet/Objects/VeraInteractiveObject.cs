// -----------------------------------------------------------------------
// <copyright file="VeraInteractiveObject.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class VeraInteractiveObject : VeraBaseObject
    {
        public VeraState State { get; internal set; }
        public string Comment { get; internal set; }
        public int RoomId { get; internal set; }

        public Room Room
        {
            get
            {
                if (this.VeraController != null)
                {
                    return this.VeraController.Rooms.Where(r => r.Id == this.RoomId).FirstOrDefault();
                }
                return null;
            }
        }

        internal override void InitializeProperties(Dictionary<string, object> values)
        {
            base.InitializeProperties(values);
            this.Comment = values.ContainsKey("comment") ? (string)values["comment"] : "";
            this.RoomId = Convert.ToInt32(values["room"]);
            this.State = values.ContainsKey("state") ? StateUtils.GetStateFromCode((int)values["state"]) : VeraState.None;
        }

        internal override void UpdateProperties(Dictionary<string, object> values)
        {
            base.UpdateProperties(values);
            this.UpdateProperty(values, "comment", "Comment", (v) => { this.Comment = v.ToString(); return true; });
            this.UpdateProperty(values, "state", "State", (v) => { this.State = StateUtils.GetStateFromCode(Convert.ToInt32(v)); return true; });
            this.UpdateProperty(values, "room", "RoomId", (v) => { this.RoomId = Convert.ToInt32(v); return true; });
        }

        protected string DataRequest(DataRequestAction action, Dictionary<string, string> parameters)
        {
            if (this.VeraController != null)
            {
                string urlCall = string.Format("{0}/data_request?id={1}&{2}",
                    this.VeraController.ConnectionInfo.ToString(), action.ToString().ToLower(),
                    string.Join("&", parameters.Select(p => string.Format("{0}={1}", p.Key, p.Value))));
                return this.VeraController.GetWebResponse(urlCall);
            }
            return string.Empty;
        }

        protected enum DataRequestAction
        {
            Action,
            VariableSet,
            JobStatus
        }
    }
}
