using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Devices
{
    /// <summary>
    /// An interface for a device connector class.
    /// </summary>
    public interface IDeviceConnector
    {
        /*/// <summary>
        /// Gets registered variables by this device.
        /// </summary>
        /// <returns>Registered Variables</returns>
        VariableRegistry GetRegisteredVariables();*/

        /// <summary>
        /// Gets the device name.
        /// </summary>
        /// <returns>Device name</returns>
        string GetDeviceName();

        /// <summary>
        /// Gets specific details about the device instance.
        /// </summary>
        /// <returns>Details about the device instance</returns>
        string GetDeviceDetails();

        /// <summary>
        /// Attempts to initialize the device instance.
        /// </summary>
        /// <returns>A boolean value representing the success of this call</returns>
        bool Initialize();

        /// <summary>
        /// Shuts down the device instance.
        /// </summary>
        void Shutdown();

        /// <summary>
        /// Resets the device instance.
        /// </summary>
        void Reset();

        /// <summary>
        /// Gets the initialization status of this device instance.
        /// </summary>
        /// <returns>A boolean value representing the initialization status of this device</returns>
        bool IsInitialized();


        /// <summary>
        /// Get the all the devices.
        /// </summary>
        /// <returns>List of devices</returns>
        List<IAuroraDevice> GetDevices();

    }
}
