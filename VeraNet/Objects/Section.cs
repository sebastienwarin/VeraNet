// -----------------------------------------------------------------------
// <copyright file="Section.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represent a section (a set of rooms)
    /// </summary>
    public class Section : VeraBaseObject
    {
        /// <summary>
        /// Gets the rooms of this section.
        /// </summary>
        /// <value>
        /// The rooms of this section.
        /// </value>
        public List<Room> Rooms
        {
            get
            {
                if (this.VeraController != null)
                {
                    return this.VeraController.Rooms.Where(r => r.Section.Id == this.Id).ToList();
                }
                return null;
            }
        }
    }
}
