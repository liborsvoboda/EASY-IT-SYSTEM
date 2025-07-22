using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace EasyITSystemCenter {

    internal abstract class DataResources : App {


        /// <summary>
        /// Get Uri from Images From Data/Images Folder
        /// Requested parameter is Filename 
        /// </summary>
        /// <param name="resourceFile"></param>
        /// <returns></returns>
        public static Uri GetImageResource(string resourceFile = "no_photo.png") {
            try {
                var uri = string.Format("pack://application:,,,/{0};component/Data/Images/{1}"
                , Assembly.GetExecutingAssembly().GetName().Name, resourceFile);
                return new Uri(uri);
            } catch { return null; }
        }



        /// <summary>
        /// Get Uri from Files From Data/Media Folder
        /// Requested parameter is Filename 
        /// </summary>
        /// <param name="resourceFile"></param>
        /// <returns></returns>
        public static Uri GetMediaResource(string resourceFile = "speed.mp4") {
            try {
                var uri = string.Format("pack://application:,,,/{0};component/Data/Media/{1}"
                , Assembly.GetExecutingAssembly().GetName().Name, resourceFile);
                return new Uri(uri);
            } catch { return null; }
        }

    }
}