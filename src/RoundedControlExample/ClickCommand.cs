using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RoundedControlExample
{
    class ClickCommand : BaseCommand
    {
        public override async Task ExecuteAsync(object parameter)
        {
            string param = (string)parameter;
            MessageBox.Show(param);
            await Task.Delay(3000);
        }
    }
}
