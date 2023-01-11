using Maray.ViewModels;

namespace Maray.Models
{
    public class SubscribeItemM
    {
        public int Index { get; set; }
        public bool IsEnable { get; set; }
        public string Note { get; set; }
        public string SubscribeUrl { get; set; }

        public List<string> ServerList { get; set; } = new List<string>();

        public SubscribeItemVM ToVM()
        {
            SubscribeItemVM vm = new SubscribeItemVM();

            vm.Index = Index;
            vm.IsEnable = IsEnable;
            vm.Note = Note;
            vm.SubscribeUrl = SubscribeUrl;
            return vm;
        }
    }
}