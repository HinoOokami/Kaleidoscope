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
using System.Windows.Threading;

namespace Kaleidoscope
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DrawBox drawBox;
        VisualBrush drawBrush;
        int angle = 0;
        DispatcherTimer timer = new DispatcherTimer();
        Canvas drawCanvas = new Canvas();
        public MainWindow()
        {
            InitializeComponent();
            FillCanvas();
            MakeBrush();
            drawGrid.Background = drawBox.MakeSolidBrush();
            FillGrid(0);
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Counter;
            timer.Start();
        }

        void FillCanvas()
        {
            drawCanvas.Width = 100;
            drawCanvas.Height = 100;
            drawCanvas.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            drawCanvas.Arrange(new Rect(0, 0, drawCanvas.DesiredSize.Width, drawCanvas.DesiredSize.Height));
            drawBox = new DrawBox(drawCanvas);
        }

        void MakeBrush()
        {
            drawBrush = new VisualBrush
                            (drawCanvas)
                            {
                                Transform = new ScaleTransform
                                    (Math.Sqrt(2), Math.Sqrt(2), drawCanvas.ActualWidth * 0.5, drawCanvas.ActualHeight * 0.5)
                            };
        }

        internal void FillGrid(int angle)
        {
            drawGrid.Children.Clear();
            for (var row = 0; row < 4; row++)
            {
                for (var col = 0; col < 4; col++)
                {
                    var rotate = new RotateTransform(angle, 50, 50);
                    var scale = new ScaleTransform
                        (Math.Sqrt(2) * ((col % 2 == 0) ? 1 : -1),
                         Math.Sqrt(2) * ((row % 2 == 0) ? 1 : -1),
                         50,
                         50);

                    //var tranform = new TransformGroup();
                    //tranform.Children.Add(scale);
                    //tranform.Children.Add(rotate);
                    var canv = new Canvas()
                                    {
                                        Background = drawBrush,
                                        ClipToBounds = true,
                                        //RenderTransform = tranform
                                        LayoutTransform = rotate,
                                        RenderTransform = scale
                                    };
                    var border = new Border
                                    {
                                        Width = drawCanvas.ActualWidth,
                                        Height = drawCanvas.ActualHeight,
                                        ClipToBounds = true,
                                        BorderThickness = new Thickness(0),
                                        Child = canv
                                    };
                    Grid.SetRow(border, row);
                    Grid.SetColumn(border, col);
                    drawGrid.Children.Add(border);
                }
            }
        }

        void Counter(object sender, EventArgs e)
        {
            angle = (++angle < 360) ? angle : 0;
            FillGrid(angle);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            drawGrid.Children.Clear();
            drawCanvas.Children.Clear();
            FillCanvas();
            MakeBrush();
            drawGrid.Background = drawBox.MakeSolidBrush();
            FillGrid(0);
        }
    }
}
