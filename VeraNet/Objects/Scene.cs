// -----------------------------------------------------------------------
// <copyright file="Scene.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects
{
    using System.Collections.Generic;

    /// <summary>
    /// Represent a Vera scene.
    /// </summary>
    public class Scene : VeraInteractiveObject
    {
        /// <summary>
        /// Gets a value indicating whether this scene is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this scene is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; internal set; }

        internal override void InitializeProperties(Dictionary<string, object> values)
        {
            base.InitializeProperties(values);
            this.IsActive = (bool)(values["active"].ToString() == "1");
        }

        internal override void UpdateProperties(Dictionary<string, object> values)
        {
            base.UpdateProperties(values);
            this.UpdateProperty(values, "active", "IsActive", (v) => { this.IsActive = (bool)(v.ToString() == "1"); return true; });
        }

        /// <summary>
        /// Runs this scene.
        /// </summary>
        /// <returns><c>true</c> if this scene is running; otherwise, <c>false</c>.</returns>
        public bool RunScene()
        {
            return this.DataRequest(DataRequestAction.Action,
                new Dictionary<string, string>()
                {
                    { "serviceId", "urn:micasaverde-com:serviceId:HomeAutomationGateway1" },
                    { "action", "RunScene" },
                    { "SceneNum", this.Id.ToString() }
                }).Contains("<OK>OK</OK>");
        }
    }
}
