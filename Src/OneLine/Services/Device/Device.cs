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
        public bool IsDesktop => Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Desktop || DeviceDetector.IsDesktop();

        public bool IsTablet => Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Tablet || DeviceDetector.IsTablet();

        public bool IsMobile => Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Phone || DeviceDetector.IsMobile();
        
        public bool IsXamarinPlatform => Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.GTK) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.iOS) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.macOS) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Tizen) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.WPF);

        public bool IsAndroidDevice => Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android) || 
            DeviceDetector.GetOs().Match.Platform.Equals("Android");

        public bool IsGTKDevice => Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.GTK);

        public bool IsiOSDevice => Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.iOS) ||
            DeviceDetector.GetOs().Match.Platform.Equals("iOS");

        public bool IsMacOsDevice => Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.macOS) ||
            DeviceDetector.GetOs().Match.Platform.Equals("Mac");

        public bool IsTizenDevice => Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Tizen) ||
            DeviceDetector.GetOs().Match.Platform.Equals("Tizen");

        public bool IsWPFDevice => Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.WPF);

        public bool IsWindowsOSPlatform => RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
            DeviceDetector.GetOs().Match.Platform.Equals("Windows") ||
            DeviceDetector.GetOs().Match.Platform.Equals("Windows CE") ||
            DeviceDetector.GetOs().Match.Platform.Equals("Windows IoT") ||
            DeviceDetector.GetOs().Match.Platform.Equals("Windows Mobile") ||
            DeviceDetector.GetOs().Match.Platform.Equals("Windows Phone") ||
            DeviceDetector.GetOs().Match.Platform.Equals("Windows RT");

        public bool IsLinuxOSPlatform => RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
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

        public bool IsOSXOSPlatform => RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ||
            DeviceDetector.GetOs().Match.Platform.Equals("Mac")||
            DeviceDetector.GetOs().Match.Platform.Equals("iOS");

        public bool IsWebPlatform => RuntimeInformation.OSDescription.Equals("web") || JSRuntime.IsNotNull();

        public bool IsWebBlazorWAsmPlatform => IsWebPlatform && !Type.GetType("Mono.Runtime").Equals(null) || JSRuntime.IsNotNull();

        public bool IsWebBlazorServerPlatform => IsWebPlatform && Type.GetType("Mono.Runtime").Equals(null) || JSRuntime.IsNotNull();

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
