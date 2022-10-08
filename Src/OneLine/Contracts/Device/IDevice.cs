namespace OneLine.Contracts
{
    /// <summary>
    /// A service which identifies the current device
    /// </summary>
    public interface IDevice
    {
        bool IsDesktop { get; }
        bool IsTablet { get; }
        bool IsMobile { get; }
        bool IsXamarinPlatform { get; }
        bool IsAndroidDevice { get; }
        bool IstvOSDevice { get; }
        bool IsiOSDevice { get; }
        bool IsMacOSDevice { get; }
        bool IsMacCatalystDevice { get; }
        bool IsTizenDevice { get; }
        bool IsWinUIDevice { get; }
        bool IsWindowsOSPlatform { get; }
        bool IsLinuxOSPlatform { get; }
        bool IsOSXOSPlatform { get; }
        bool IsPhysical { get; }
        bool IsVirtual { get; }
        bool IsWebPlatform { get; }
        bool IsWebBlazorWAsmPlatform { get; }
        bool IsWebBlazorServerPlatform { get; }
        bool IsHybridPlatform { get; }
        bool Is32BitsArmOSArquitecture { get; }
        bool Is64BitsArmOSArquitecture { get; }
        bool Is32BitsOSArquitecture { get; }
        bool Is64BitsOSArquitecture { get; }
        bool Is32BitsArmProcessArquitecture { get; }
        bool Is64BitsArmProcessArquitecture { get; }
        bool Is32BitsProcessArquitecture { get; }
        bool Is64BitsProcessArquitecture { get; }
    }
}
