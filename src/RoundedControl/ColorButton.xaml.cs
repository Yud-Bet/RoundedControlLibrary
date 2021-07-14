using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RoundedControl
{
    /// <summary>
    /// Interaction logic for ColorButton.xaml
    /// </summary>
    public partial class ColorButton : UserControl, ICommandSource
    {
        public ColorButton()
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
            DependencyProperty.Register("Command", typeof(ICommand), typeof(ColorButton), new PropertyMetadata((ICommand)null, CommandChanged));

        private static void CommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ColorButton cb = (ColorButton)d;
            cb.HookUpCommand((ICommand)e.OldValue, (ICommand)e.NewValue);
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
            EventHandler handler = new EventHandler(CanExecuteChanged);
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
                //if a not RoutedCommand
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

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(ColorButton), new PropertyMetadata(null));


        public IInputElement CommandTarget
        {
            get { return (IInputElement)GetValue(CommandTargetProperty); }
            set { SetValue(CommandTargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(ColorButton), new PropertyMetadata(null));


        public static RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColorButton));

        public event RoutedEventHandler Click
        {
            add { AddHandler(ClickEvent, value); }
            remove { RemoveHandler(ClickEvent, value); }
        }

        protected virtual void OnClick()
        {
            RoutedEventArgs args = new RoutedEventArgs(ClickEvent, this);
            RaiseEvent(args);
            Command?.Execute(CommandParameter);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            OnClick();
        }
    }
}
