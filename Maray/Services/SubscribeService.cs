using Maray.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Services
{
    public class SubscribeService
    {
        private List<ServerM> subscribeList = new();
        public SubscribeService()
        { }

        public void Subscribe(ServerM server)
        {
            subscribeList.Add(server);
        }

        public List<ServerM> GetSubscribeList()
        {
            subscribeList.Add(new ServerM("sub service 1"));
            return subscribeList;
        }
    }
}
