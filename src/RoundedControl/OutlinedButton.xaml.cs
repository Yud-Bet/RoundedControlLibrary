using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RoundedControl
{
    /// <summary>
    /// Interaction logic for OutlinedButton.xaml
    /// </summary>
    public partial class OutlinedButton : UserControl, ICommandSource
    {
        public OutlinedButton()
        {
            InitializeComponent();
        }


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(OutlinedButton), new PropertyMetadata((ICommand)null, CommandChanged));

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            OutlinedButton ob = (OutlinedButton)d;
            ob.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
        }

        private void HookUpCommand(ICommand oldValue, ICommand newValue)
        {
            if (oldValue != null)
            {
                RemoveCommand(oldValue, newValue);
            }
            AddCommand(oldValue, newValue);
        }

        private void AddCommand(ICommand oldValue, ICommand newValue)
        {
            EventHandler handler = CanExecuteChanged;
            if (newValue != null)
            {
                newValue.CanExecuteChanged += handler;
            }
        }

        private void RemoveCommand(ICommand oldValue, ICommand newValue)
        {
            EventHandler handler = CanExecuteChanged;
            oldValue.CanExecuteChanged -= handler;
        }

        private void CanExecuteChanged(object sender, EventArgs e)
        {
            if (this.Command != null)
            {
                RoutedCommand command = this.Command as RoutedCommand;

                //if a RoutedCommand
                if (command != null)
                {
                    if (command.CanExecute(CommandParameter, CommandTarget))
                    {
                        this.IsEnabled = true;
                    }
                    else this.IsEnabled = false;
                }
                else
                {
                    if (Command.CanExecute(CommandParameter))
                    {
                        this.IsEnabled = true;
                    }
                    else this.IsEnabled = false;
                }
            }
        }

        public object CommandParameter { get; }

        public IInputElement CommandTarget { get; }

        public static RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(OutlinedButton));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        protected virtual void OnClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent, this);
            RaiseEvent(args);
            Command?.Execute(args);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            OnClick();
        }
    }
}
