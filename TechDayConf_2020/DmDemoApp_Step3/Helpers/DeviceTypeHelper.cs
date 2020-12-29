using DmDemoApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DmDemoApp.Helpers
{
    public static class DeviceTypeHelper
    {
        public static DeviceFormFactorType GetDeviceType()
        {
            switch (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily)
            {
                case "Windows.Desktop":
                    return DeviceFormFactorType.Tablet;
                case "Windows.Mobile":
                    return DeviceFormFactorType.Phone;
                case "Windows.Universal":
                    return DeviceFormFactorType.IoT;
                case "Windows.Team":
                    return DeviceFormFactorType.SurfaceHub;
                case "Windows.Xbox":
                    return DeviceFormFactorType.Xbox;
                default:
                    return DeviceFormFactorType.Other;
            }
        }
    }
}
