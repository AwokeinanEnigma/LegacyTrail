#region

using System.Runtime.InteropServices;

#endregion

internal class Win32
{
    [Flags]
    public enum ProtectFlags
    {
        NCRYPT_SILENT_FLAG = 0x00000040
    }

    [UnmanagedFunctionPointer(CallingConvention.Winapi)]
    public delegate int PFNCryptStreamOutputCallback(IntPtr pvCallbackCtxt, IntPtr pbData, int cbData,
        [MarshalAs(UnmanagedType.Bool)] bool fFinal);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct NCRYPT_PROTECT_STREAM_INFO
    {
        public PFNCryptStreamOutputCallback pfnStreamOutput;
        public IntPtr pvCallbackCtxt;
    }

    [Flags]
    public enum UnprotectSecretFlags
    {
        NCRYPT_UNPROTECT_NO_DECRYPT = 0x00000001,
        NCRYPT_SILENT_FLAG = 0x00000040
    }

    [DllImport("ncrypt.dll")]
    public static extern uint NCryptStreamOpenToUnprotect(in NCRYPT_PROTECT_STREAM_INFO pStreamInfo,
        ProtectFlags dwFlags, IntPtr hWnd, out IntPtr phStream);

    [DllImport("ncrypt.dll")]
    public static extern uint NCryptStreamUpdate(IntPtr hStream, IntPtr pbData, int cbData,
        [MarshalAs(UnmanagedType.Bool)] bool fFinal);

    [DllImport("ncrypt.dll")]
    public static extern uint NCryptUnprotectSecret(out IntPtr phDescriptor, int dwFlags, IntPtr pbProtectedBlob,
        uint cbProtectedBlob, IntPtr pMemPara, IntPtr hWnd, out IntPtr ppbData, out uint pcbData);

    [DllImport("ncrypt.dll", CharSet = CharSet.Unicode)]
    public static extern uint NCryptGetProtectionDescriptorInfo(IntPtr hDescriptor, IntPtr pMemPara, int dwInfoType,
        out string ppvInfo);
}