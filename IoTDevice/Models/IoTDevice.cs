﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using IoTDevice.Models;

namespace IOTDevice.Models
{
    public class IoTDevice
    {
        static RegistryManager registryManager;
        static string connectionString = Constant.IOTHubConnString.ToString();
        static Device device;

        public static object Models { get; internal set; }

        public static async Task<Device> AddDeviceAsync(string deviceID)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.AddDeviceAsync(new Device(deviceID));
            return device;
        }

        public static async Task<Device> GetDeviceAsync(string deviceID)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.GetDeviceAsync(deviceID);
            return device;
        }

        public static async Task RemoveDeviceAsync(string deviceID)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            await registryManager.RemoveDeviceAsync(deviceID);
        }

        public static async Task UpdateDeviceAsync(string deviceID)
        {
            Device device;
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            device = await registryManager.GetDeviceAsync(deviceID);
            device.StatusReason = "East US";
            await registryManager.UpdateDeviceAsync(device);

        }

    }
}