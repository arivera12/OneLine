using System;
using System.Runtime.InteropServices;

namespace OneLine.Extensions
{
    public static class XamarinFormsDeviceExtensions
    {
        public static bool IsDesktop
        {
            get
            {
                try
                {
                    return Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Desktop;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsTablet
        {
            get
            {
                try
                {
                    return Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Tablet;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsMobile
        {
            get
            {
                try
                {
                    return Xamarin.Forms.Device.Idiom == Xamarin.Forms.TargetIdiom.Phone;
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsXamarinPlatform
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
        public static bool IsAndroidDevice
        {
            get
            {
                try
                {
                    return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android);
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsGTKDevice
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
        public static bool IsiOSDevice
        {
            get
            {
                try
                {
                    return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.iOS);
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsMacOsDevice
        {
            get
            {
                try
                {
                    return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.macOS);
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsTizenDevice
        {
            get
            {
                try
                {
                    return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Tizen);
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsWPFDevice
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
        public static bool IsWindowsOSPlatform
        {
            get
            {
                try
                {
                    return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsLinuxOSPlatform
        {
            get
            {
                try
                {
                    return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsOSXOSPlatform
        {
            get
            {
                try
                {
                    return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsWebPlatform
        {
            get
            {
                try
                {
                    //browser >= NET5 < web
                    return RuntimeInformation.OSDescription.Equals("web", StringComparison.InvariantCultureIgnoreCase) || 
                        RuntimeInformation.OSDescription.Equals("browser", StringComparison.InvariantCultureIgnoreCase);
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsWebBlazorWAsmPlatform
        {
            get
            {
                try
                {
                    return IsWebPlatform && !Type.GetType("Mono.Runtime").Equals(null);
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsWebBlazorServerPlatform
        {
            get
            {
                try
                {
                    return IsWebPlatform && Type.GetType("Mono.Runtime").Equals(null);
                }
                catch
                {
                    return false;
                }
            }
        }
        public static bool IsHybridPlatform
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
        public static bool Is32BitsArmOSArquitecture => RuntimeInformation.OSArchitecture.Equals(Architecture.Arm);
        public static bool Is64BitsArmOSArquitecture => RuntimeInformation.OSArchitecture.Equals(Architecture.Arm64);
        public static bool Is32BitsOSArquitecture => RuntimeInformation.OSArchitecture.Equals(Architecture.X86);
        public static bool Is64BitsOSArquitecture => RuntimeInformation.OSArchitecture.Equals(Architecture.X64);
        public static bool Is32BitsArmProcessArquitecture => RuntimeInformation.ProcessArchitecture.Equals(Architecture.Arm);
        public static bool Is64BitsArmProcessArquitecture => RuntimeInformation.ProcessArchitecture.Equals(Architecture.Arm64);
        public static bool Is32BitsProcessArquitecture => RuntimeInformation.ProcessArchitecture.Equals(Architecture.X86);
        public static bool Is64BitsProcessArquitecture => RuntimeInformation.ProcessArchitecture.Equals(Architecture.X64);
    }
}
