using System.Collections.Generic;
using Newtonsoft.Json;

namespace WhatIsMyBrowser.CommonTypesCore
{
    public class UserAgentSearchResponseParse
    {
        [JsonProperty("simple_software_string")]
        public string SimpleSoftwareString { get; set; }

        [JsonProperty("simple_sub_description_string")]
        public string SimpleSubDescriptionString { get; set; }

        [JsonProperty("simple_operating_platform_string")]
        public string SimpleOperatingPlatformString { get; set; }

        [JsonProperty("software")]
        public string Software { get; set; }

        [JsonProperty("software_name")]
        public string SoftwareName { get; set; }

        [JsonProperty("software_name_code")]
        public string SoftwareNameCode { get; set; }

        [JsonProperty("software_version")]
        public string SoftwareVersion { get; set; }

        [JsonProperty("software_version_full")]
        public string SoftwareVersionFull { get; set; }

        [JsonProperty("operating_system")]
        public string OperatingSystem { get; set; }

        [JsonProperty("operating_system_name")]
        public string OperatingSystemName { get; set; }

        [JsonProperty("operating_system_name_code")]
        public string OperatingSystemNameCode { get; set; }

        [JsonProperty("operating_system_flavour")]
        public string OperatingSystemFlavour { get; set; }

        [JsonProperty("operating_system_flavour_code")]
        public string OperatingSystemFlavourCode { get; set; }

        [JsonProperty("operating_system_version")]
        public string OperatingSystemVersion { get; set; }

        [JsonProperty("operating_system_version_full")]
        public string OperatingSystemVersionFull { get; set; }

        [JsonProperty("operating_platform")]
        public string OperatingPlatform { get; set; }

        [JsonProperty("operating_platform_code")]
        public string OperatingPlatformCode { get; set; }

        [JsonProperty("operating_platform_vendor_name")]
        public string OperatingPlatformVendorName { get; set; }

        [JsonProperty("software_type")]
        public string SoftwareType { get; set; }

        [JsonProperty("software_sub_type")]
        public string SoftwareSubType { get; set; }

        [JsonProperty("hardware_type")]
        public string HardwareType { get; set; }

        [JsonProperty("hardware_sub_type")]
        public string HardwareSubType { get; set; }

        [JsonProperty("hardware_sub_sub_type")]
        public string HardwareSubSubType { get; set; }

        [JsonProperty("software_type_specific")]
        public string SoftwareTypeSpecific { get; set; }

        [JsonProperty("hardware_type_specific")]
        public string HardwareTypeSpecific { get; set; }

        [JsonProperty("layout_engine_name")]
        public string LayoutEngineName { get; set; }

        [JsonProperty("operating_system_frameworks")]
        public string[] OperatingSystemFrameworks { get; set; }

        [JsonProperty("extra_info")]
        public Dictionary<string, object> ExtraInfo { get; set; }

        [JsonProperty("extra_info_dict")]
        public Dictionary<string, object> ExtraInfoDict { get; set; }

        [JsonProperty("detected_addons")]
        public string[] DetectedAddons { get; set; }

        [JsonProperty("capabilities")]
        public string[] Capabilities { get; set; }

        [JsonProperty("layout_engine_version")]
        public string[] LayoutEngineVersion { get; set; }
    }
}