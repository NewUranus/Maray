using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maray.Actions
{
    public class AppearingTriggerAction : TriggerAction<VisualElement>
    {
        protected override void Invoke(VisualElement sender)
        {
            Debug.WriteLine("Invoke");
            Console.WriteLine("Invoke");
        }
    }
}