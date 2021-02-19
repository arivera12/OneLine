using DeviceDetectorNET;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using OneLine.Extensions;
using System;
using System.Runtime.InteropServices;

namespace OneLine.Services
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
                DeviceDetector = new DeviceDetector(await JSRuntime.InvokeAsync<string>("eval", new[] { "window.navigator.userAgent" }));
                DeviceDetector.Parse();
            }).Invoke();
        }
        public bool IsDesktop
        {
            get
            {
                try
                {
                    return DeviceDetector?.IsDesktop() ?? Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Desktop;
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
                    return DeviceDetector?.IsTablet() ?? Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Tablet;
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
                    return DeviceDetector?.IsMobile() ?? Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Phone;
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
                    return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.GTK) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.iOS) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.macOS) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Tizen) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.WPF);
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
                    return DeviceDetector?.GetOs().Match.Name.Equals("Android") ?? Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsGTKDevice
        {
            get
            {
                try
                {
                    return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.GTK);
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
                    return DeviceDetector?.GetOs().Match.Name.Equals("iOS") ?? Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.iOS);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsMacOsDevice
        {
            get
            {
                try
                {
                    return DeviceDetector?.GetOs().Match.Name.Equals("Mac") ?? Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.macOS);
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
                    return DeviceDetector?.GetOs().Match.Name.Equals("Tizen") ?? Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Tizen);
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool IsWPFDevice
        {
            get
            {
                try
                {
                    return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.WPF);
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
        public bool IsWebPlatform
        {
            get
            {
                try
                {
                    //browser >= NET5 < web
                    return RuntimeInformation.OSDescription.Equals("web", StringComparison.InvariantCultureIgnoreCase) ||
                        RuntimeInformation.OSDescription.Equals("browser", StringComparison.InvariantCultureIgnoreCase) || 
                        JSRuntime.IsNotNull();
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
                    return IsWebPlatform && !Type.GetType("Mono.Runtime").Equals(null) || JSRuntime.IsNotNull();
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
                    return IsWebPlatform && Type.GetType("Mono.Runtime").Equals(null) || JSRuntime.IsNotNull();
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
