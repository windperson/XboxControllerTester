using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace XboxControllerTester
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Gamepad _gamepad = null;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            Gamepad.GamepadAdded += GamepadAdded;
            Gamepad.GamepadRemoved += GamepadRemoved;

            while(true)
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, 
                    () => 
                    {
                        if(_gamepad == null) { return; }

                        var reading = _gamepad.GetCurrentReading();

                        tbLeftTrigger.Text = reading.LeftTrigger.ToString();
                        tbRightTrigger.Text = reading.RightTrigger.ToString();

                        tbLeftThumbstickX.Text = reading.LeftThumbstickX.ToString();
                        tbLeftThumbstickY.Text = reading.LeftThumbstickY.ToString();

                        tbRightThumbstickX.Text = reading.RightThumbstickX.ToString();
                        tbRightThumbstickY.Text = reading.RightThumbstickY.ToString();

                        tbButtons.Text = string.Empty;

                        tbButtons.Text += (reading.Buttons & GamepadButtons.A) == GamepadButtons.A ? "A" : "";
                        tbButtons.Text += (reading.Buttons & GamepadButtons.B) == GamepadButtons.B ? "B" : "";
                        tbButtons.Text += (reading.Buttons & GamepadButtons.X) == GamepadButtons.X ? "X" : "";
                        tbButtons.Text += (reading.Buttons & GamepadButtons.Y) == GamepadButtons.Y ? "Y" : "";
                        tbButtons.Text += (reading.Buttons & GamepadButtons.LeftShoulder) == GamepadButtons.LeftShoulder ? "LeftShoulder" : "";
                        tbButtons.Text += (reading.Buttons & GamepadButtons.RightShoulder) == GamepadButtons.RightShoulder ? "RightShoulder" : "";
                        tbButtons.Text += (reading.Buttons & GamepadButtons.LeftThumbstick) == GamepadButtons.LeftThumbstick ? "LeftThumbstick" : "";
                        tbButtons.Text += (reading.Buttons & GamepadButtons.RightThumbstick) == GamepadButtons.RightThumbstick ? "RightThumbstick" : "";
                        tbButtons.Text += (reading.Buttons & GamepadButtons.DPadLeft) == GamepadButtons.DPadLeft ? "DPadLeft" : "";
                        tbButtons.Text += (reading.Buttons & GamepadButtons.DPadRight) == GamepadButtons.DPadRight ? "DPadRight" : "";
                        tbButtons.Text += (reading.Buttons & GamepadButtons.DPadUp) == GamepadButtons.DPadUp ? "DPadUp" : "";
                        tbButtons.Text += (reading.Buttons & GamepadButtons.DPadDown) == GamepadButtons.DPadDown ? "DPadDown" : "";
                    });

                await Task.Delay(TimeSpan.FromMilliseconds(5));
            }
        }

        private async void GamepadAdded(object sender, Gamepad e)
        {
            _gamepad = e;

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    tbConnected.Text = "Controller Added!";
                });
        }
        
        private async void GamepadRemoved(object sender, Gamepad e)
        {
            _gamepad = null;

            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal,
                () =>
                {
                    tbConnected.Text = "Controller Removed!";
                });
        }
    }
}
