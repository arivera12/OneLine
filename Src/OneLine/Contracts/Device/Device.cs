using DeviceDetectorNET;
using Microsoft.JSInterop;
using OneLine.Extensions;
using System.Runtime.InteropServices;

namespace OneLine.Contracts
{
    public class Device : IDevice
    {
        private IJSRuntime JSRuntime { get; set; }
        private DeviceDetector DeviceDetector { get; set; }
        public Device()
        {
        }
        public Device(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
            new Action(async () =>
            {
                try
                {
                    DeviceDetector = new DeviceDetector(await JSRuntime.InvokeAsync<string>("eval", new[] { "window.navigator.userAgent" }));
                    DeviceDetector.Parse();
                }
                catch (Exception)
                { }
            }).Invoke();
        }
        public bool IsDesktop
        {
            get
            {
                try
                {
                    return DeviceDetector?.IsDesktop() ?? DeviceInfo.Current.Idiom == DeviceIdiom.Desktop;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsTablet
        {
            get
            {
                try
                {
                    return DeviceDetector?.IsTablet() ?? DeviceInfo.Current.Idiom == DeviceIdiom.Tablet;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsMobile
        {
            get
            {
                try
                {
                    return DeviceDetector?.IsMobile() ?? DeviceInfo.Current.Idiom == DeviceIdiom.Phone;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsXamarinPlatform
        {
            get
            {
                try
                {
                    return DeviceInfo.Current.Platform.Equals(DevicePlatform.Android) ||
                  DeviceInfo.Current.Platform.Equals(DevicePlatform.Tizen) ||
                  DeviceInfo.Current.Platform.Equals(DevicePlatform.iOS) ||
                  DeviceInfo.Current.Platform.Equals(DevicePlatform.macOS) ||
                  DeviceInfo.Current.Platform.Equals(DevicePlatform.Tizen) ||
                  DeviceInfo.Current.Platform.Equals(DevicePlatform.WinUI) ||
                  DeviceInfo.Current.Platform.Equals(DevicePlatform.MacCatalyst) ||
                  DeviceInfo.Current.Platform.Equals(DevicePlatform.tvOS) ||
                  DeviceInfo.Current.Platform.Equals(DevicePlatform.watchOS);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsAndroidDevice
        {
            get
            {
                try
                {
                    return DeviceDetector?.GetOs().Match.Name.Equals("Android") ?? DeviceInfo.Current.Platform.Equals(DevicePlatform.Android);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IstvOSDevice
        {
            get
            {
                try
                {
                    return DeviceInfo.Current.Platform.Equals(DevicePlatform.tvOS);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsWinUIDevice
        {
            get
            {
                try
                {
                    return DeviceInfo.Current.Platform.Equals(DevicePlatform.WinUI);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsMacCatalystDevice
        {
            get
            {
                try
                {
                    return DeviceInfo.Current.Platform.Equals(DevicePlatform.MacCatalyst);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsiOSDevice
        {
            get
            {
                try
                {
                    return DeviceDetector?.GetOs().Match.Name.Equals("iOS") ?? DeviceInfo.Current.Platform.Equals(DevicePlatform.iOS);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsMacOSDevice
        {
            get
            {
                try
                {
                    return DeviceDetector?.GetOs().Match.Name.Equals("Mac") ?? DeviceInfo.Current.Platform.Equals(DevicePlatform.macOS);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsTizenDevice
        {
            get
            {
                try
                {
                    return DeviceDetector?.GetOs().Match.Name.Equals("Tizen") ?? DeviceInfo.Current.Platform.Equals(DevicePlatform.Tizen);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsWindowsOSPlatform
        {
            get
            {
                try
                {
                    return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows") ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows CE") ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows IoT") ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows Mobile") ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows Phone") ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows RT");
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsLinuxOSPlatform
        {
            get
            {
                try
                {
                    return RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Arch Linux") ||
                        DeviceDetector.GetOs().Match.Name.Equals("CentOS") ||
                        DeviceDetector.GetOs().Match.Name.Equals("Debian") ||
                        DeviceDetector.GetOs().Match.Name.Equals("Fedora") ||
                        DeviceDetector.GetOs().Match.Name.Equals("Kubuntu") ||
                        DeviceDetector.GetOs().Match.Name.Equals("GNU/Linux") ||
                        DeviceDetector.GetOs().Match.Name.Equals("Lubuntu") ||
                        DeviceDetector.GetOs().Match.Name.Equals("VectorLinux") ||
                        DeviceDetector.GetOs().Match.Name.Equals("OpenBSD") ||
                        DeviceDetector.GetOs().Match.Name.Equals("Red Hat") ||
                        DeviceDetector.GetOs().Match.Name.Equals("SUSE") ||
                        DeviceDetector.GetOs().Match.Name.Equals("Ubuntu") ||
                        DeviceDetector.GetOs().Match.Name.Equals("Xubuntu");
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsOSXOSPlatform
        {
            get
            {
                try
                {
                    return RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Mac") ||
                        DeviceDetector.GetOs().Match.Name.Equals("iOS");
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsPhysical
        {
            get
            {
                try
                {
                    return DeviceInfo.Current.DeviceType.Equals(DeviceType.Physical);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsVirtual
        {
            get
            {
                try
                {
                    return DeviceInfo.Current.DeviceType.Equals(DeviceType.Virtual);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsWebPlatform
        {
            get
            {
                try
                {
                    //browser >= NET5 < web
                    return JSRuntime.IsNotNull() ||
                        RuntimeInformation.OSDescription.Equals("web", StringComparison.InvariantCultureIgnoreCase) ||
                        RuntimeInformation.OSDescription.Equals("browser", StringComparison.InvariantCultureIgnoreCase);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsWebBlazorWAsmPlatform
        {
            get
            {
                try
                {
                    return JSRuntime.IsNotNull() && JSRuntime is JSInProcessRuntime;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsWebBlazorServerPlatform
        {
            get
            {
                try
                {
                    return JSRuntime.IsNotNull() && !(JSRuntime is JSInProcessRuntime);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsHybridPlatform
        {
            get
            {
                try
                {
                    return IsWebPlatform && IsXamarinPlatform;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool Is32BitsArmOSArquitecture => RuntimeInformation.OSArchitecture.Equals(Architecture.Arm);
        public bool Is64BitsArmOSArquitecture => RuntimeInformation.OSArchitecture.Equals(Architecture.Arm64);
        public bool Is32BitsOSArquitecture => RuntimeInformation.OSArchitecture.Equals(Architecture.X86);
        public bool Is64BitsOSArquitecture => RuntimeInformation.OSArchitecture.Equals(Architecture.X64);
        public bool Is32BitsArmProcessArquitecture => RuntimeInformation.ProcessArchitecture.Equals(Architecture.Arm);
        public bool Is64BitsArmProcessArquitecture => RuntimeInformation.ProcessArchitecture.Equals(Architecture.Arm64);
        public bool Is32BitsProcessArquitecture => RuntimeInformation.ProcessArchitecture.Equals(Architecture.X86);
        public bool Is64BitsProcessArquitecture => RuntimeInformation.ProcessArchitecture.Equals(Architecture.X64);
    }
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDevice(this IServiceCollection services)
        {
            return services.AddScoped<IDevice, Device>();
        }
    }
}
