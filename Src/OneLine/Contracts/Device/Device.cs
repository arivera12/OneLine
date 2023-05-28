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
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
                    return DeviceInfo.Current.Idiom == DeviceIdiom.Desktop;
#else
                    return DeviceDetector.IsDesktop();
#endif
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
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
                    return DeviceInfo.Current.Idiom == DeviceIdiom.Tablet;
#else
                    return DeviceDetector.IsTablet();
#endif
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
#if ANDROID || IOS || MACCATALYST || WINDOWS  || Linux
                    return DeviceInfo.Current.Idiom == DeviceIdiom.Phone;
#else
                    return DeviceDetector.IsMobile();
#endif                                                                                                 
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
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
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
#else
                return false;
#endif
            }
        }
        public bool IsMauiPlatform
        {
            get
            {
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
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
#else
                return false;
#endif
            }
        }
        public bool IsAndroidDevice
        {
            get
            {
                try
                {
#if ANDROID
                    return DeviceInfo.Current.Platform.Equals(DevicePlatform.Android);
#else
                    return DeviceDetector.GetOs().Match.Name.Equals("Android", StringComparison.InvariantCultureIgnoreCase);
#endif
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
#if IOS || MACCATALYST
                    return DeviceInfo.Current.Platform.Equals(DevicePlatform.tvOS);
#else
                    return DeviceDetector.GetOs().Match.Name.Equals("Apple TV", StringComparison.InvariantCultureIgnoreCase);
#endif
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
#if WINDOWS
                    return DeviceInfo.Current.Platform.Equals(DevicePlatform.WinUI);
#else
                    return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows CE", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows IoT", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows Mobile", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows Phone", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows RT", StringComparison.InvariantCultureIgnoreCase);
#endif
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
#if MACCATALYST
                    return DeviceInfo.Current.Platform.Equals(DevicePlatform.MacCatalyst);
#else
                    return false;
#endif
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
#if IOS
                    return DeviceInfo.Current.Platform.Equals(DevicePlatform.iOS);
#else
                    return DeviceDetector.GetOs().Match.Name.Equals("iOS", StringComparison.InvariantCultureIgnoreCase);
#endif
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
#if MACCATALYST
                    return DeviceInfo.Current.Platform.Equals(DevicePlatform.macOS);
#else
                    return DeviceDetector.GetOs().Match.Name.Equals("Mac", StringComparison.InvariantCultureIgnoreCase);
#endif
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
#if Linux
                    return (IsLinuxOSPlatform && DeviceInfo.Current.Platform.Equals(DevicePlatform.Tizen))            
#else
                    return DeviceDetector.GetOs().Match.Name.Equals("Tizen", StringComparison.InvariantCultureIgnoreCase);
#endif
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
                        DeviceDetector.GetOs().Match.Name.Equals("Windows", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows CE", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows IoT", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows Mobile", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows Phone", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Windows RT", StringComparison.InvariantCultureIgnoreCase);
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
                        DeviceDetector.GetOs().Match.Name.Equals("Arch Linux", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("CentOS", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Debian", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Fedora", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Kubuntu", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("GNU/Linux", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Lubuntu", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("VectorLinux", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("OpenBSD", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Red Hat", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("SUSE", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Ubuntu", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("Xubuntu", StringComparison.InvariantCultureIgnoreCase);
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
                        DeviceDetector.GetOs().Match.Name.Equals("Mac", StringComparison.InvariantCultureIgnoreCase) ||
                        DeviceDetector.GetOs().Match.Name.Equals("iOS", StringComparison.InvariantCultureIgnoreCase);
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
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
                    return IsXamarinPlatform && DeviceInfo.Current.DeviceType.Equals(DeviceType.Physical);
#else
                    return false;
#endif
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
#if ANDROID || IOS || MACCATALYST || WINDOWS || Linux
                    return IsXamarinPlatform && DeviceInfo.Current.DeviceType.Equals(DeviceType.Virtual);
#else
                    return false;
#endif
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
                    return JSRuntime.IsNotNull() && JSRuntime is not JSInProcessRuntime;
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
