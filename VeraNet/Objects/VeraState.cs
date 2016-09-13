// -----------------------------------------------------------------------
// <copyright file="VeraState.cs" company="Sebastien.warin.Fr">
//  Copyright 2012 - Sebastien.warin.fr
// </copyright>
// <author>Sebastien Warin</author>
// -----------------------------------------------------------------------

namespace VeraNet.Objects
{

    /// <summary>
    /// Represent the Vera's state
    /// </summary>
    public enum VeraState
    {
        None,
        Pending,
        Error,
        Success
    }

    internal static class StateUtils
    {
        public static VeraState GetStateFromCode(int code)
        {
            switch (code)
            { 
                case -1:
                    return VeraState.None;
                case 0:
                case 1:
                case 5:
                case 6:
                    return VeraState.Pending;
                case 4:
                    return VeraState.Success;
                default:
                return VeraState.Error;
            }
        }
    }
}
