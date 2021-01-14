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
        public IJSRuntime JSRuntime { get; set; }
        public DeviceDetector DeviceDetector { get; set; }
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
                catch (Exception)
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
                catch (Exception)
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
                catch (Exception)
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
                catch (Exception)
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
                    return DeviceDetector?.GetOs().Match.Platform.Equals("Android") ?? Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android);
                }
                catch (Exception)
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
                catch (Exception)
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
                    return DeviceDetector?.GetOs().Match.Platform.Equals("iOS") ?? Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.iOS);
                }
                catch (Exception)
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
                    return DeviceDetector?.GetOs().Match.Platform.Equals("Mac") ?? Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.macOS);
                }
                catch (Exception)
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
                    return DeviceDetector?.GetOs().Match.Platform.Equals("Tizen") ?? Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Tizen);
                }
                catch (Exception)
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
                catch (Exception)
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
                        DeviceDetector.GetOs().Match.Platform.Equals("Windows") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("Windows CE") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("Windows IoT") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("Windows Mobile") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("Windows Phone") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("Windows RT");
                }
                catch (Exception)
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
                        DeviceDetector.GetOs().Match.Platform.Equals("Arch Linux") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("CentOS") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("Debian") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("Fedora") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("Kubuntu") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("GNU/Linux") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("Lubuntu") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("VectorLinux") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("OpenBSD") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("Red Hat") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("SUSE") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("Ubuntu") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("Xubuntu");
                }
                catch (Exception)
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
                        DeviceDetector.GetOs().Match.Platform.Equals("Mac") ||
                        DeviceDetector.GetOs().Match.Platform.Equals("iOS");
                }
                catch (Exception)
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
                    return RuntimeInformation.OSDescription.Equals("web") || JSRuntime.IsNotNull();
                }
                catch (Exception)
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
                catch (Exception)
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
                catch (Exception)
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
