using Maray.ViewModels;

namespace Maray.Models
{
    public class SubscribeItemM
    {
        public bool IsEnable { get; set; }
        public string Note { get; set; }
        public string SubscribeUrl { get; set; }

        public List<string> ServerList { get; set; } = new List<string>();

        public SubscribeItemVM ToVM()
        {
            SubscribeItemVM vm = new SubscribeItemVM();

            vm.isEnable = IsEnable;
            vm.note = Note;
            vm.subscribeUrl = SubscribeUrl;
            return vm;
        }
    }
}