using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VeraNet.Objects.Devices
{
    [VeraDevice(DeviceCategory.Thermostat)]
    /// <summary>
    /// Represent a Thermostat
    /// </summary>
    public class Thermostat : PowerMeter
    {
        /// <summary>
        /// Termostats state
        /// </summary>
        public ModeState Hvacstate { get; internal set; }
        /// <summary>
        /// Thermostats mode
        /// </summary>
        public ModeTarget Mode { get; internal set; }
        /// <summary>
        /// Check if device is responding
        /// </summary>
        public bool CommFailure { get; internal set; }
        public Double Setpoint { get; internal set; }
        /// <summary>
        /// Heat Temperature
        /// </summary>
        public Double Heat { get; internal set; }
        /// <summary>
        /// Cool Temperature
        /// </summary>
        public Double Cool { get; internal set; }
        /// <summary>
        /// Get temperature of thermostats
        /// </summary>
        public Double Temperature { get; internal set; }

        internal override void InitializeProperties(Dictionary<string, object> values)
        {
            base.InitializeProperties(values);
            this.Hvacstate = (ModeState) Enum.Parse(typeof(ModeState), values["hvacstate"].ToString());
            this.Mode = (ModeTarget) Enum.Parse(typeof(ModeTarget), values["mode"].ToString());
            this.CommFailure = (bool) (values["commFailure"].ToString() == "1");
            this.Setpoint = double.Parse(values["setpoint"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
            this.Heat = double.Parse(values["heat"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
            this.Cool = double.Parse(values["cool"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
            this.Temperature = double.Parse(values["temperature"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
        }

        internal override void UpdateProperties(Dictionary<string, object> values)
        {
            base.UpdateProperties(values);
            this.UpdateProperty(values, "temperature", "Temperature", (v) => { this.Temperature = double.Parse(v.ToString(), System.Globalization.CultureInfo.InvariantCulture); return true; });

            this.UpdateProperty(values, "hvacstate", "Hvacstate", (v) => { this.Hvacstate = (ModeState)Enum.Parse(typeof(ModeState), v.ToString()); return true; });
            this.UpdateProperty(values, "mode", "Mode", (v) => { this.Mode = (ModeTarget)Enum.Parse(typeof(ModeTarget), v.ToString()); return true; });
            this.UpdateProperty(values, "commFailure", "CommFailure", (v) => { this.CommFailure = (bool) (v.ToString() == "1"); return true; });

            this.UpdateProperty(values, "setpoint", "Setpoint", (v) => { this.Setpoint = double.Parse(v.ToString(), System.Globalization.CultureInfo.InvariantCulture); return true; });
            this.UpdateProperty(values, "heat", "Heat", (v) => { this.Heat = double.Parse(v.ToString(), System.Globalization.CultureInfo.InvariantCulture); return true; });
            this.UpdateProperty(values, "cool", "Cool", (v) => { this.Cool = double.Parse(v.ToString(), System.Globalization.CultureInfo.InvariantCulture); return true; });
        }

        /// <summary>
        /// Set temperature
        /// </summary>
        /// <param name="temperature">Temperature</param>
        /// <returns></returns>
        public bool SetTemperature(double temperature)
        {
            return this.SetActionAndWaitJob("urn:upnp-org:serviceId:TemperatureSetpoint1", "SetCurrentSetpoint", "NewCurrentSetpoint", temperature);
        }

        /// <summary>
        /// Set Thermostats mode
        /// </summary>
        /// <param name="modeTarget">Thermostats Mode</param>
        /// <returns></returns>
        public bool SetModeTarget(String modeTarget)
        {
            return this.SetActionAndWaitJob("urn:upnp-org:serviceId:HVAC_UserOperatingMode1", "SetModeTarget", "NewModeTarget", modeTarget);
        }

        public enum ModeTarget 
        {
            Off = 0,
            HeatOn = 1,
            CoolOn = 2,
            AutoChangeOver = 3
        }

        public enum ModeState
        {
            Idle,
            Heating,
            Cooling
        }
    }
}
