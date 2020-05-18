Imports Newtonsoft.Json

Public Class UserAgentSearchResponseParse
    <JsonProperty("simple_software_string")>
    Public Property SimpleSoftwareString As String

    <JsonProperty("simple_sub_description_string")>
    Public Property SimpleSubDescriptionString As String

    <JsonProperty("simple_operating_platform_string")>
    Public Property SimpleOperatingPlatformString As String

    <JsonProperty("software")>
    Public Property Software As String

    <JsonProperty("software_name")>
    Public Property SoftwareName As String

    <JsonProperty("software_name_code")>
    Public Property SoftwareNameCode As String

    <JsonProperty("software_version")>
    Public Property SoftwareVersion As String

    <JsonProperty("software_version_full")>
    Public Property SoftwareVersionFull As String

    <JsonProperty("operating_system")>
    Public Property OperatingSystem As String

    <JsonProperty("operating_system_name")>
    Public Property OperatingSystemName As String

    <JsonProperty("operating_system_name_code")>
    Public Property OperatingSystemNameCode As String

    <JsonProperty("operating_system_flavour")>
    Public Property OperatingSystemFlavour As String

    <JsonProperty("operating_system_flavour_code")>
    Public Property OperatingSystemFlavourCode As String

    <JsonProperty("operating_system_version")>
    Public Property OperatingSystemVersion As String

    <JsonProperty("operating_system_version_full")>
    Public Property OperatingSystemVersionFull As String

    <JsonProperty("operating_platform")>
    Public Property OperatingPlatform As String

    <JsonProperty("operating_platform_code")>
    Public Property OperatingPlatformCode As String

    <JsonProperty("operating_platform_vendor_name")>
    Public Property OperatingPlatformVendorName As String

    <JsonProperty("software_type")>
    Public Property SoftwareType As String

    <JsonProperty("software_sub_type")>
    Public Property SoftwareSubType As String

    <JsonProperty("hardware_type")>
    Public Property HardwareType As String

    <JsonProperty("hardware_sub_type")>
    Public Property HardwareSubType As String

    <JsonProperty("hardware_sub_sub_type")>
    Public Property HardwareSubSubType As String

    <JsonProperty("software_type_specific")>
    Public Property SoftwareTypeSpecific As String

    <JsonProperty("hardware_type_specific")>
    Public Property HardwareTypeSpecific As String

    <JsonProperty("layout_engine_name")>
    Public Property LayoutEngineName As String

    <JsonProperty("operating_system_frameworks")>
    Public Property OperatingSystemFrameworks As String()

    <JsonProperty("extra_info")>
    Public Property ExtraInfo As Dictionary(Of String, Object)

    <JsonProperty("extra_info_dict")>
    Public Property ExtraInfoDict As Dictionary(Of String, Object)

    <JsonProperty("detected_addons")>
    Public Property DetectedAddons As String()

    <JsonProperty("capabilities")>
    Public Property Capabilities As String()

    <JsonProperty("layout_engine_version")>
    Public Property LayoutEngineVersion As String()
End Class