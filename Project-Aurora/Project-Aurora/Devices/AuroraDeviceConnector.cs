using Aurora.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Devices
{
    public abstract class AuroraDeviceConnector
    {
        //private Dictionary<string, AuroraDevice> Devices = new Dictionary<string, AuroraDevice>();
        protected List<AuroraDevice> devices = new List<AuroraDevice>();
        public IReadOnlyList<AuroraDevice> Devices => devices.AsReadOnly();
        private bool isInitialized;
        public event EventHandler NewSuccessfulInitiation;
        private int DisconnectedDeviceCount = 0;
        private int UpdatedDeviceCount = 0;
        private SemaphoreSlim InitIsOngoing = new SemaphoreSlim(1, 1);


        protected abstract string ConnectorName { get; }
        public string GetConnectorName() => ConnectorName;
        public virtual void Reset()
        {
            Shutdown();
            Initialize();
        }



        /// <summary>
        /// Is called first. Initialize the device here
        /// </summary>
        public async void Initialize()
        {
            await InitIsOngoing.WaitAsync();

            if (IsInitialized() || Global.Configuration.devices_disabled.Contains(GetType()))
                return;
            Global.logger.Info("Start initializing Connector: " + GetConnectorName());
            if (await Task.Run(() => InitializeImpl()))
            {
                DisconnectedDeviceCount = 0;
                devices = GetDevices();
                foreach (var device in Devices)
                {
                    device.ConnectionHandler += ConnectionHandling;
                    device.UpdateFinished += DeviceUpdated;
                    device.Connect();
                }
                isInitialized = true;
                NewSuccessfulInitiation?.Invoke(this, new EventArgs());

            }
            Global.logger.Info("Connector, " + GetConnectorName() + ", was" + (IsInitialized() ? "" : " not") + " initialized");

            InitIsOngoing.Release();

        }

        protected abstract bool InitializeImpl();

        private void ConnectionHandling(object sender, EventArgs args)
        {
            AuroraDevice device = sender as AuroraDevice;
            if (device.IsConnected())
            {
                DisconnectedDeviceCount--;
            }
            else
            {
                DisconnectedDeviceCount++;
            }
            if (DisconnectedDeviceCount == 0)
            {
                Shutdown();
            }
        }
        private void DeviceUpdated(object sender, EventArgs args)
        {
            AuroraDevice device = sender as AuroraDevice;
            UpdatedDeviceCount++;
            if (UpdatedDeviceCount == Devices.Count)
            {
                UpdateDevices();
            }
        }
        protected virtual void UpdateDevices()
        {

        }
        /// <summary>
        /// Is called last. Dispose of the devices here
        /// </summary>
        public void Shutdown()
        {
            if (IsInitialized())
            {
                devices.Clear();
                ShutdownImpl();
                isInitialized = false;
                Global.logger.Info("Connector, " + GetConnectorName() + ", was shutdown");
            }
        }

        protected abstract void ShutdownImpl();


        /// <summary>
        /// Get the all the devices.
        /// </summary>
        /// <returns>List of devices</returns>
        protected abstract List<AuroraDevice> GetDevices();

        public bool IsInitialized() => isInitialized;

        public string GetConnectorDetails() => isInitialized ?
                                                    ConnectorName + ": " + ConnectorSubDetails :
                                                    ConnectorName + ": Not Initialized";
        protected virtual string ConnectorSubDetails => "Initialized";
        protected void LogError(string s) => Global.logger.Error(s);

        private VariableRegistry variableRegistry;
        public virtual VariableRegistry GetRegisteredVariables()
        {
            if (variableRegistry == null)
            {
                variableRegistry = new VariableRegistry();
                RegisterVariables(variableRegistry);
            }
            return variableRegistry;
        }
        /// <summary>
        /// Only called once when registering variables. Can be empty if not needed
        /// </summary>
        protected virtual void RegisterVariables(VariableRegistry local)
        {
            //purposefully empty, if varibles are needed, this should be overridden
        }
        protected VariableRegistry GlobalVarRegistry => Global.Configuration.VarRegistry;

    }
}

