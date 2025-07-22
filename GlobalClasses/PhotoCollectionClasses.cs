using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace EasyITSystemCenter.GlobalClasses {

    internal class UriToBitmapConverter : IValueConverter {

        public object Convert
        (
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
        ) {
            using (var stream = File.OpenRead(value?.ToString())) {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                return value = image;
            }
        }

        public object ConvertBack
        (
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture
         ) {
            throw new Exception("The method or operation is not implemented.");
        }
    }

    public class PhotoCollection : ObservableCollection<Photo> {

        public PhotoCollection() {
        }

        public void Add(string fullName, int dbId, bool isPrimary) {
            Add(new Photo(fullName, dbId, isPrimary));
        }

        public new void Clear() {
            Items?.Clear();
        }
    }

    public class Photo {
        private string _path;
        private bool _isPrimary;
        private int _dbId;

        public Photo(string path, int dbId, bool isPrimary) {
            _path = path;
            _isPrimary = isPrimary;
            _dbId = dbId;
        }

        public int DbId { get { return _dbId; } set { _dbId = value; } }
        public bool IsPrimary { get { return _isPrimary; } set { _isPrimary = value; } }
        public string Source { get { return _path; } set { _path = value; } }
    }
}