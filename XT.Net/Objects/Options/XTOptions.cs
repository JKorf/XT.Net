using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace XT.Net.Objects.Options
{
    /// <summary>
    /// XT options
    /// </summary>
    public class XTOptions : LibraryOptions<XTRestOptions, XTSocketOptions, ApiCredentials, XTEnvironment>
    {
    }
}
