using System;
using System.Collections.Generic;
using System.Text;

namespace XaBikeStand.Services
{
    public interface ISettingsService
    {
        string AuthAccessToken { get; set; }
        string AuthIdToken { get; set; }
    }
}
