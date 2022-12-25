using Maray.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Models.Configs
{
    internal class MarayConfigM
    {
        public MarayConfigM()
        { }

        public string Loglevel { get; set; }
        public ServerM DefaultServer { get; set; }
    }
}