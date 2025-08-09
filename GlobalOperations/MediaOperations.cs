using HelixToolkit.Wpf.SharpDX;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EasyITSystemCenter.GlobalOperations {

    internal class MediaOperations {

        public static ImageSource ByteToImage(byte[] imageData) {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }

        /// <summary>
        /// ScreenShot Function For Application Menu Tool If Return Array True, Image is returned in
        /// response Other Save Dialog is Opened
        /// </summary>
        /// <param name="MainWindow">     The main window.</param>
        /// <param name="returnArrayOnly"></param>
        public static async Task<byte[]> SaveAppScreenShot(MainWindow MainWindow, bool returnArrayOnly = false) {
            RenderTargetBitmap bmp;
            bmp = new RenderTargetBitmap((int)MainWindow.ActualWidth, (int)MainWindow.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            try {
                PngBitmapEncoder encoder = new PngBitmapEncoder(); bmp.Render(MainWindow); encoder.Frames.Add(BitmapFrame.Create(bmp));
                if (!returnArrayOnly) {
                    SaveFileDialog dlg = new SaveFileDialog() { DefaultExt = ".png", Filter = "Image files |*.png;" };
                    if (dlg.ShowDialog() == true) { FileStream fs = new FileStream(dlg.FileName, FileMode.Create); encoder.Save(fs); fs.Close(); }
                }
            } catch (Exception ex) { await MainWindow.ShowMessageOnMainWindow(true, ex.Message + Environment.NewLine + ex.StackTrace); }
            return bmp.ToByteArray();
        }

        /// <summary>
        /// Important Closing connections of openned files by Form and binding this is solution for
        /// close oppened file after load Solution for All Files
        /// </summary>
        /// <param name="path"></param>
        public static BitmapImage GetImageImmediatelly(string path) {
            var image = new BitmapImage();
            using (var stream = File.OpenRead(path)) {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
            }
            return image;
        }

        /// <summary>
        /// Cresate Bitmap Image from DB array to Image for show preview
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static BitmapImage ArrayToImage(byte[] array) {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = new MemoryStream(array);
            image.EndInit();
            return image;
        }

        /// <summary>
        /// Create More Image File Types NotUsed
        /// </summary>
        /// <param name="file">    </param>
        /// <param name="frame">   </param>
        /// <param name="fileType"></param>
        public void CreateImageFile(FileStream file, BitmapFrame frame, string fileType) {
            if (file == null) return;
            BitmapEncoder encoder = null;
            switch (fileType.ToLower()) {
                case "bmp": encoder = new BmpBitmapEncoder(); break;
                case "gif": encoder = new GifBitmapEncoder(); break;
                case "tiff":
                case "tif":
                    encoder = new TiffBitmapEncoder() { Compression = TiffCompressOption.Default }; break;
                case "jpg":
                case "jpeg":
                    encoder = new JpegBitmapEncoder() { QualityLevel = 100 }; break;
                default:
                    encoder = new PngBitmapEncoder(); break;
            }
            encoder.Frames.Add(frame);
            encoder.Save(file);
        }
    }
}