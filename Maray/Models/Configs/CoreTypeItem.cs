using Maray.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Models.Configs
{
    public class CoreTypeItem
    {
        public ProtocolEnum configType { get; set; }

        public CoreType coreType { get; set; }
    }
}