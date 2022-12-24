using Maray.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Models.Configs
{
    internal class ConfigM
    {
        public ConfigM()
        { }

        public List<SubscribeItemM> SubscribeItemList { get; set; } = new List<SubscribeItemM>();
    }
}