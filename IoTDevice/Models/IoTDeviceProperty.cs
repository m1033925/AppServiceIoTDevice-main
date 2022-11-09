using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using IoTDevice.Models;
using Microsoft.AspNetCore.Mvc;

namespace IOTDevice.Models
{
    public class IoTDeviceProperty : IDeviceProperties
    {
        static RegistryManager registryManager;
        static DeviceClient deviceClient;
        static readonly string iotconnectionString = Constant.IOTDeviceConnString.ToString();
        static readonly string iotsvcconnectionString = Constant.IOTHubSvcConnString.ToString();
        public async Task UpdateDeviceProperties(ReportedProperties iOTReportedProperties)
        {
            deviceClient = DeviceClient.CreateFromConnectionString(iotconnectionString);
            TwinCollection reportedProperties, connectivity, temperature;
            reportedProperties = new TwinCollection();
            connectivity = new TwinCollection();
            temperature = new TwinCollection();

            connectivity["type"] = "Cellular";
            temperature["HighTemp"] = "27";

            reportedProperties["connectivity"] = connectivity;
            reportedProperties["temparature"] = temperature;
            reportedProperties["sensortype"] = iOTReportedProperties.SensorType;
            await deviceClient.UpdateReportedPropertiesAsync(reportedProperties);

        }

        public async Task UpdateDesiredProperties(string deviceName)
        {
            RegistryManager registryManager = RegistryManager.CreateFromConnectionString(iotsvcconnectionString);
            var client = await registryManager.GetTwinAsync(deviceName);
            TwinCollection desiredProperties, telemetryConfig;
            desiredProperties = new TwinCollection();
            telemetryConfig = new TwinCollection();

            telemetryConfig["frequency"] = "5Hz";
            desiredProperties["telemetryconfig"] = telemetryConfig;
            client.Properties.Desired["telemetryconfig"] = telemetryConfig;

            await registryManager.UpdateTwinAsync(client.DeviceId, client, client.ETag);


        }







    }
}