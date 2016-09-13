using System;
using System.Windows;
using System.Windows.Input;
using VeraNet;
using VeraNet.Objects;
using VeraNet.Objects.Devices;
using System.Windows.Controls;
using System.Linq;

namespace WpfConsoleTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VeraController controller = null;

        public MainWindow()
        {
            InitializeComponent();
            this.controller = new VeraController(new VeraConnectionInfo("192.168.0.222"));
            this.controller.DataReceived += new EventHandler<VeraDataReceivedEventArgs>(controller_DataReceived);
            this.controller.DataSent += new EventHandler<VeraDataSentEventArgs>(controller_DataSent);
            this.controller.ErrorOccurred += controller_ErrorOccurred;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.controller.StartListener();
        }

        private void controller_DataSent(object sender, VeraDataSentEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.lbMessages.Items.Add(e);
                this.lbMessages.ScrollIntoView(e);
            }), null);
        }

        private void controller_DataReceived(object sender, VeraDataReceivedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.RawData);
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.lbDevices.ItemsSource = this.controller.Devices;
                this.lbScene.ItemsSource = this.controller.Scenes;
                this.lbMessages.Items.Add(e);
                this.lbMessages.ScrollIntoView(e);
                tvHome.ItemsSource = this.controller.Sections;
            }), null);
        }

        private void controller_ErrorOccurred(object sender, VeraErrorOccurredEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Error : " + e.Exception.ToString());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.controller.Dispose();
        }

        private void lbScene_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.lbScene.SelectedItem != null)
            {
                var scene = this.lbScene.SelectedItem as Scene;
                scene.RunScene();
            }
        }

        private void lbDevices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.lbDevices.SelectedItem != null)
            {
                var device = this.lbDevices.SelectedItem as Switch;
                if (device != null)
                {
                    if (device.Status)
                    {
                        device.SwitchOff();
                    }
                    else
                    {
                        device.SwitchOn();
                    }
                }
            }
        }

        private void Slider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            var light = ((Slider)sender).Tag as DimmableLight;
            if (light != null)
            {
                light.SetLevel((int)((Slider)sender).Value);
            }
        }
    }
}
