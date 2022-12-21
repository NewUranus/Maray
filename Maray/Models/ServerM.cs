using CommunityToolkit.Mvvm.ComponentModel;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Maray.Models
{
    public sealed record ServerM(
        [AllowNull]
        [property: JsonPropertyName("v")]
        string v,

        [AllowNull]
        [property: JsonPropertyName("v")]
        string url
        //string ps,
        //string add,
        //string port,
        //string type,
        //string id,
        //string aid,
        //string net,
        //string path,
        //string host,
        //string tls
        );
}