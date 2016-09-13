using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VeraNet;
using VeraNet.Objects.Devices;

namespace WpfConsoleTest
{
    /// <summary>
    /// Interaction logic for GridWindow.xaml
    /// </summary>
    public partial class GridWindow : Window
    {
        private VeraController controller = null;

        public GridWindow()
        {
            InitializeComponent();
            this.controller = new VeraController(new VeraConnectionInfo("192.168.0.222"));
            this.controller.DataReceived += new EventHandler<VeraDataReceivedEventArgs>(controller_DataReceived);
            this.controller.DataSent += new EventHandler<VeraDataSentEventArgs>(controller_DataSent);
        }

        private void controller_DataSent(object sender, VeraDataSentEventArgs e)
        {

        }

        private void controller_DataReceived(object sender, VeraDataReceivedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.dgDevices.ItemsSource = this.controller.Devices;
                //this.lbScene.ItemsSource = this.controller.Scenes;
                //this.lbMessages.Items.Add(e);
                //this.lbMessages.ScrollIntoView(e);
                //tvHome.ItemsSource = this.controller.Sections;
            }), null);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.controller.StartListener();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.controller.Dispose();
        }
    }

    public class DeviceTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SwitchTemplate { get; set; }
        public DataTemplate DimmableTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var sw = item as Switch;
            if (sw == null)
            {
                return base.SelectTemplate(item, container);
            }

            return SwitchTemplate;
        }
    }
}
