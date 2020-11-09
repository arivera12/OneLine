using System;
using System.Runtime.InteropServices;

namespace OneLine.Helpers
{
    public static class Device
    {
        public static bool IsXamarinPlatform
        {
            get
            {
                return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.GTK) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.iOS) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.macOS) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Tizen) ||
                  Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.WPF);
            }
        }
        public static bool IsAndroidDevice
        {
            get 
            {
                return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Android);
            }
        }
        public static bool IsGTKDevice
        {
            get
            {
                return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.GTK);
            }
        }
        public static bool IsiOSDevice
        {
            get
            {
                return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.iOS);
            }
        }
        public static bool IsMacOsDevice
        {
            get
            {
                return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.macOS);
            }
        }
        public static bool IsTizenDevice
        {
            get
            {
                return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.Tizen);
            }
        }
        public static bool IsWPFDevice
        {
            get
            {
                return Xamarin.Forms.Device.RuntimePlatform.Equals(Xamarin.Forms.Device.WPF);
            }
        }
        public static bool IsWindowsOSPlatform
        {
            get
            {
                return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            }
        }
        public static bool IsLinuxOSPlatform
        {
            get
            {
                return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            }
        }
        public static bool IsOSXOSPlatform
        {
            get
            {
                return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            }
        }
        public static bool IsWebPlatform
        {
            get
            {
                return RuntimeInformation.OSDescription.Equals("web");
            }
        }
        public static bool IsWebBlazorWAsmPlatform
        {
            get
            {
                return IsWebPlatform && !Type.GetType("Mono.Runtime").Equals(null);
            }
        }
        public static bool IsWebBlazorServerPlatform
        {
            get
            {
                return IsWebPlatform && Type.GetType("Mono.Runtime").Equals(null);
            }
        }
        public static bool Is32BitsArmOSArquitecture
        {
            get
            {
                return RuntimeInformation.OSArchitecture.Equals(Architecture.Arm);
            }
        }
        public static bool Is64BitsArmOSArquitecture
        {
            get
            {
                return RuntimeInformation.OSArchitecture.Equals(Architecture.Arm64);
            }
        }
        public static bool Is86BitsOSArquitecture
        {
            get
            {
                return RuntimeInformation.OSArchitecture.Equals(Architecture.X86);
            }
        }
        public static bool Is64BitsOSArquitecture
        {
            get
            {
                return RuntimeInformation.OSArchitecture.Equals(Architecture.X64);
            }
        }
        public static bool Is32BitsArmpProcessArquitecture
        {
            get
            {
                return RuntimeInformation.ProcessArchitecture.Equals(Architecture.Arm);
            }
        }
        public static bool Is64BitsArmProcessArquitecture
        {
            get
            {
                return RuntimeInformation.ProcessArchitecture.Equals(Architecture.Arm64);
            }
        }
        public static bool Is32BitsProcessArquitecture
        {
            get
            {
                return RuntimeInformation.ProcessArchitecture.Equals(Architecture.X86);
            }
        }
        public static bool Is64BitsProcessArquitecture
        {
            get
            {
                return RuntimeInformation.ProcessArchitecture.Equals(Architecture.X64);
            }
        }
    }
}
