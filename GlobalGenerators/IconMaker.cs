using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EasyITSystemCenter.GlobalGenerators {

    /// <summary>
    /// System Online Icon Generator from custom Path Its for working with Icon over Database Dynamically
    /// </summary>
    public class IconMaker {

        /// <summary>
        /// Generate Custom Icon from Defined Path
        /// </summary>
        /// <param name="color">   The color.</param>
        /// <param name="iconPath">The icon path.</param>
        /// <returns></returns>
        public static BitmapImage Icon(Color color, string iconPath = null) {
            var canvas = new Canvas {
                Width = 256,
                Height = 256,
                Background = new SolidColorBrush(Colors.Transparent)
            };
            var path = new System.Windows.Shapes.Path() {
                Data = Geometry.Parse(string.IsNullOrWhiteSpace(iconPath)
                    ? "M38.8 5.1C28.4-3.1 13.3-1.2 5.1 9.2S-1.2 34.7 9.2 42.9l592 464c10.4 8.2 25.5 6.3 33.7-4.1s6.3-25.5-4.1-33.7L542.6 400c2.7-7.8 1.3-16.5-3.9-23l-14.9-18.6C495.5 322.9 480 278.8 480 233.4V200c0-75.8-55.5-138.6-128-150.1V32c0-17.7-14.3-32-32-32s-32 14.3-32 32V49.9c-43.9 7-81.5 32.7-104.4 68.7L38.8 5.1zM221.7 148.4C239.6 117.1 273.3 96 312 96h8 8c57.4 0 104 46.6 104 104v33.4c0 32.7 6.4 64.8 18.7 94.5L221.7 148.4zM406.2 416l-60.9-48H168.3c21.2-32.8 34.4-70.3 38.4-109.1L160 222.1v11.4c0 45.4-15.5 89.5-43.8 124.9L101.3 377c-5.8 7.2-6.9 17.1-2.9 25.4s12.4 13.6 21.6 13.6H406.2zM384 448H320 256c0 17 6.7 33.3 18.7 45.3s28.3 18.7 45.3 18.7s33.3-6.7 45.3-18.7s18.7-28.3 18.7-45.3z"
                    : iconPath
                    ),
                Stretch = Stretch.Fill,
                Fill = new SolidColorBrush(color),
                Width = 256,
                Height = 256,
            };
            canvas.Children.Add(path);

            var size = new Size(256, 256);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            var rtb = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(canvas);

            var png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));

            using (var memory = new MemoryStream()) {
                png.Save(memory);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }
    }
}