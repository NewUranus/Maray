using CommunityToolkit.Mvvm.ComponentModel;
using Maray.Enum;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace Maray.Models
{
    public partial class ServerM :ObservableObject
    {
        [ObservableProperty]
        public Node node;
        public bool isEnable;
        public ProtocolType type;
    }
    //public sealed record ServerM(
    //    [AllowNull]
    //    [property: JsonPropertyName("v")]
    //    string v,

    //    [AllowNull]
    //    [property: JsonPropertyName("v")]
    //    string url

    //    //Node node,

    //    //bool isEnable
    //    //string ps,
    //    //string add,
    //    //string port,
    //    //string type,
    //    //string id,
    //    //string aid,
    //    //string net,
    //    //string path,
    //    //string host,
    //    //string tls
    //    );
}