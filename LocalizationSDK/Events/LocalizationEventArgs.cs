using System;

namespace LocalizationSDK.Events
{
    public class LocalizationEventArgs : EventArgs
    {
        public string? Key { get; set; }
        public string? Value { get; set; }
    }
}