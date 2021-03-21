﻿using BlazorMobile.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorMobileAuth.Common.Interfaces
{
    [ProxyInterface]
    public interface IXamarinBridge
    {
        Task<List<string>> DisplayAlert(string title, string msg, string cancel);
    }
}
