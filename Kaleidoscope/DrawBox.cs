using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Color = System.Windows.Media.Color;
using Image = System.Drawing.Image;
using Rectangle = System.Windows.Shapes.Rectangle;

namespace Kaleidoscope
{
    public class DrawBox
    {
        Random rnd = new Random();
        Canvas canvas;
        Line line;
        Ellipse ellipse;
        Rectangle rectangle;
        
        public DrawBox(Canvas canv)
        {
            canvas = canv;
            CreateLine();
            CreateEllipse();
            CreateRectangle();
            FillCanvas();
        }

        LinearGradientBrush MakeLinearBrush()
        {
            var brush = new LinearGradientBrush
            {
                GradientStops = new GradientStopCollection
                                            {
                                                new GradientStop
                                                    (Color.FromArgb
                                                         ((byte) rnd.Next(0, 256),
                                                          (byte) rnd.Next(0, 256),
                                                          (byte) rnd.Next(0, 256),
                                                          (byte) rnd.Next(0, 256)),
                                                     0.0),
                                                new GradientStop
                                                    (Color.FromArgb
                                                         ((byte) rnd.Next(0, 256),
                                                          (byte) rnd.Next(0, 256),
                                                          (byte) rnd.Next(0, 256),
                                                          (byte) rnd.Next(0, 256)),
                                                     1.0)
                                            }
            };
            return brush;
        }

        RadialGradientBrush MakeRadialBrush()
        {
            var brush = new RadialGradientBrush
            {
                GradientStops = new GradientStopCollection
                                            {
                                                new GradientStop
                                                    (Color.FromArgb
                                                         ((byte) rnd.Next(0, 256),
                                                          (byte) rnd.Next(0, 256),
                                                          (byte) rnd.Next(0, 256),
                                                          (byte) rnd.Next(0, 256)),
                                                     0.0),
                                                new GradientStop
                                                    (Color.FromArgb
                                                         ((byte) rnd.Next(0, 256),
                                                          (byte) rnd.Next(0, 256),
                                                          (byte) rnd.Next(0, 256),
                                                          (byte) rnd.Next(0, 256)),
                                                     1.0)
                                            }
            };
            return brush;
        }

        public SolidColorBrush MakeSolidBrush()
        {
            var brush = new SolidColorBrush
                (Color.FromArgb
                     (255,
                      (byte)rnd.Next(0, 256),
                      (byte)rnd.Next(0, 256),
                      (byte)rnd.Next(0, 256)));
            
            return brush;
        }

        void CreateLine()
        {
            line = new Line
                   {
                       X1 = canvas.ActualWidth * (rnd.NextDouble() / 2),
                       Y1 = canvas.ActualHeight * (rnd.NextDouble() / 2),
                       X2 = canvas.ActualWidth * (rnd.NextDouble() / 2 + 0.5),
                       Y2 = canvas.ActualHeight * (rnd.NextDouble() / 2 + 0.5),
                       StrokeThickness = rnd.Next(1, (int)(canvas.ActualWidth + canvas.ActualHeight) / 10)
                   };
            var brush = MakeLinearBrush();
            line.Stroke = brush;
        }

        void CreateEllipse()
        {
            ellipse = new Ellipse
            {
                Width = canvas.ActualWidth * (rnd.NextDouble() + 0.5) / Math.Sqrt(2),
                Height = canvas.ActualHeight * (rnd.NextDouble() + 0.5) / Math.Sqrt(2)
            };
            Canvas.SetLeft(ellipse, (canvas.ActualWidth - ellipse.Width) / 2);
            Canvas.SetTop(ellipse, (canvas.ActualHeight - ellipse.Height) / 2);
            var brushFill = MakeRadialBrush();
            brushFill.GradientOrigin = new System.Windows.Point
                (rnd.NextDouble(), rnd.NextDouble());
            ellipse.Fill = brushFill;
            var brushStroke = MakeRadialBrush();
            ellipse.Stroke = brushStroke;
            ellipse.StrokeThickness = rnd.Next(1, (int) (canvas.ActualWidth + canvas.ActualHeight) / 10);
            ellipse.RenderTransform = new RotateTransform(rnd.Next(0, 360), ellipse.Width / 2, ellipse.Height / 2);
        }

        void CreateRectangle()
        {
            rectangle = new Rectangle
            {
                Width = canvas.ActualWidth * (rnd.NextDouble() + 0.5) * Math.Sqrt(2),
                Height = canvas.ActualHeight * (rnd.NextDouble() + 0.5) * Math.Sqrt(2)
            };
            Canvas.SetLeft(rectangle, (canvas.ActualWidth - rectangle.Width)/2);
            Canvas.SetTop(rectangle, (canvas.ActualHeight - rectangle.Height)/2);
            var brushFill = MakeLinearBrush();
            brushFill.StartPoint = new System.Windows.Point
                (rnd.NextDouble() / 2, rnd.NextDouble() / 2);
            brushFill.EndPoint = new System.Windows.Point
                (rnd.NextDouble() / 2 + 0.5, rnd.NextDouble() / 2 + 0.5);
            rectangle.Fill = brushFill;
            var brushStroke = MakeLinearBrush();
            brushStroke.StartPoint = new System.Windows.Point(rnd.NextDouble() / 2, rnd.NextDouble() / 2);
            brushStroke.EndPoint = new System.Windows.Point(rnd.NextDouble() / 2 + 0.5, rnd.NextDouble() / 2 + 0.5);
            rectangle.Stroke = brushStroke;
            rectangle.StrokeThickness = rnd.Next(1, (int)(canvas.ActualWidth + canvas.ActualHeight) / 10);
            rectangle.RenderTransform = new RotateTransform(rnd.Next(0, 360), rectangle.Width/2, rectangle.Height/2);
        }

        void CreateBackground()
        {
            var backgroundBrush = MakeLinearBrush();
            backgroundBrush.Transform = new ScaleTransform(Math.Sqrt(2), Math.Sqrt(2), canvas.ActualWidth * 0.5, canvas.ActualHeight * 0.5);
            canvas.Background = backgroundBrush;
        }

        void FillCanvas()
        {
            CreateBackground();
            canvas.Children.Add(rectangle);
            canvas.Children.Add(ellipse);
            canvas.Children.Add(line);
        }
    }
}