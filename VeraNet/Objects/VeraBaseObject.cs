// -----------------------------------------------------------------------
// <copyright file="VeraBaseObject.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    /// <summary>
    /// Represent a Vera base object.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public abstract class VeraBaseObject : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets the identifier of this Vera object.
        /// </summary>
        /// <value>
        /// The identifier of this Vera object..
        /// </value>
        public int Id { get; internal set; }
        /// <summary>
        /// Gets the name of this Vera object.
        /// </summary>
        /// <value>
        /// The name of this Vera object.
        /// </value>
        public string Name { get; internal set; }
        /// <summary>
        /// Gets or sets the last update of this Vera object.
        /// </summary>
        /// <value>
        /// The last updateof this Vera object.
        /// </value>
        public DateTime LastUpdate { get; protected set; }

        /// <summary>
        /// Gets the vera controller of this Vera object.
        /// </summary>
        /// <value>
        /// The vera controller of this Vera object.
        /// </value>
        public VeraController VeraController { get; internal set; }

        internal virtual void InitializeProperties(Dictionary<string, object> values)
        {
            this.Id = (int)values["id"];
            this.Name = values["name"].ToString();
            this.ResfreshLastUpdate();
        }

        internal virtual void UpdateProperties(Dictionary<string, object> values)
        {
            this.UpdateProperty(values, "name", "Name", (v) => { this.Name = v.ToString(); return true; });
            this.ResfreshLastUpdate();
        }

        protected void UpdateProperty(Dictionary<string, object> values, string jsonProperty, Action<object> updateFunction)
        {
            if (values != null && values.ContainsKey(jsonProperty))
            {
                updateFunction(values[jsonProperty]);
            }
        }

        protected void UpdateProperty(Dictionary<string, object> values, string jsonProperty, string objectProperty, Func<object, bool> updateFunction)
        {
            if (values != null && values.ContainsKey(jsonProperty))
            {
                if (updateFunction(values[jsonProperty]))
                {
                    this.NotifyPropertyChanged(objectProperty);
                    if (objectProperty.EndsWith("Id") && objectProperty.Length > 2)
                    { 
                        this.NotifyPropertyChanged(objectProperty.Remove(objectProperty.Length - 2));
                    }
                }
            }
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void ResfreshLastUpdate()
        {
            this.LastUpdate = DateTime.Now;
            this.NotifyPropertyChanged("LastUpdate");
        }

        protected DateTime GetDateTime(double unixTimeStamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(unixTimeStamp).ToLocalTime();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
