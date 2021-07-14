using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundedControlExample
{
    class ClickCommand : BaseCommand
    {
        public override async Task ExecuteAsync(object parameter)
        {
            await Task.Delay(3000);
        }
    }
}
