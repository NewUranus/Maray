using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Models.Configs
{
    public class KcpItem
    {
        /// <summary> </summary>
        public int mtu { get; set; }

        /// <summary> </summary>
        public int tti { get; set; }

        /// <summary> </summary>
        public int uplinkCapacity { get; set; }

        /// <summary> </summary>
        public int downlinkCapacity { get; set; }

        /// <summary> </summary>
        public bool congestion { get; set; }

        /// <summary> </summary>
        public int readBufferSize { get; set; }

        /// <summary> </summary>
        public int writeBufferSize { get; set; }
    }
}