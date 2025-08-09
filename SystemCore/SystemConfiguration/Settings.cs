using System.Collections;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace EasyITSystemCenter.Properties {
    /*
    internal sealed partial class Settings {
        public Settings() {
            // // Pro přidávání obslužných rutin událostí určených pro ukládání a změnu nastavení
            // odkomentujte prosím níže uvedené řádky:
            //
            // this.SettingChanging += this.SettingChangingEventHandler;
            //
            // this.SettingsSaving += this.SettingsSavingEventHandler;
        }

        private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e) {
            // Kód pro zpracování události SettingChangingEvent přidejte sem.
        }

        private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e) {
            // Kód pro zpracování události SettingsSaving přidejte sem.
        }
    }*/

    public static class ResourceManagerHelper {

        public static string GetResourceName(this ResourceManager resourceManager, string value, CultureInfo cultureInfo, bool ignoreCase = false) {
            var comparisonType = ignoreCase ? System.StringComparison.OrdinalIgnoreCase : System.StringComparison.Ordinal;
            var entry = resourceManager.GetResourceSet(cultureInfo, true, true)
                                       .OfType<DictionaryEntry>()
                                       .FirstOrDefault(dictionaryEntry => dictionaryEntry.Value.ToString().Equals(value, comparisonType));

            if (entry.Key == null)
                throw new System.Exception("Key and value not found in the resource file");

            return entry.Key.ToString();
        }
    }

    ///Using
    //var key = Resources.ResourceManager.GetResourceName(value, CultureInfo.InvariantCulture, true);
    //var keyValue = Resources.ResourceManager.GetString("AboutTitle");
}