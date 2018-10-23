using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Lights_Out_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private LightsOutGame game;
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            game = new LightsOutGame();
            game.NewGame();
            CreateGrid();
        }

        private void CreateGrid()
        {
            int rectSize = (int)CanvasMain.Width / game.GridSize;
            CanvasMain.Children.Clear();
            // Create rectangles for grid
            for (int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    Rectangle rect = new Rectangle();
                    rect.Fill = new SolidColorBrush(Windows.UI.Colors.White);
                    rect.Width = rectSize + 1;
                    rect.Height = rectSize + 1;
                    rect.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                    // Store each row and col as a Point
                    rect.Tag = new Point(r, c);
                    // Register event handler
                    rect.Tapped += Rectangle_Tapped;
                    // Put the rectangle at the proper location within the canvas
                    Canvas.SetTop(rect, r * rectSize);
                    Canvas.SetLeft(rect, c * rectSize);
                    // Add the new rectangle to the canvas' children
                    CanvasMain.Children.Add(rect);
                }
            }
            UpdateGridColors();
        }

        private void UpdateGridColors()
        {
            int index = 0;

            // Set the colors of the rectangles
            for (int r = 0; r < game.GridSize; r++)
            {
                for (int c = 0; c < game.GridSize; c++)
                {
                    Rectangle rect = CanvasMain.Children[index] as Rectangle;
                    index++;
                    if (game.GetGridValue(r, c))
                    {
                        rect.Fill = new SolidColorBrush(Windows.UI.Colors.White);
                        rect.Stroke = new SolidColorBrush(Windows.UI.Colors.Black);
                    }
                    else
                    {
                        rect.Fill = new SolidColorBrush(Windows.UI.Colors.Black);
                        rect.Stroke = new SolidColorBrush(Windows.UI.Colors.White);
                    }
                }
            }
            CheckForWin();
        }

        private async void CheckForWin()
        {
            if (game.IsGameOver())
            {
                MessageDialog messageDialog = new MessageDialog("Congratulations! You've won!", "Lights Out!");
                messageDialog.Commands.Add(new UICommand("OK"));

                IUICommand command = await messageDialog.ShowAsync();
            }
        }

        private void Rectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            if (rect != null)
            {
                var move = (Point)rect.Tag;
                game.Move((int)move.X, (int)move.Y);
                UpdateGridColors();
            }
        }

        private void New_Game_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            game.NewGame();
            UpdateGridColors();
        }

        private void About_Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AboutPage));

        }
    }
}
