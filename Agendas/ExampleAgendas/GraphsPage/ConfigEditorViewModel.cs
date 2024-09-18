using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EasyITSystemCenter.Pages {

    //TODO Zjistit jestli by takto šlo genericky sledovat všechny propertychange na globální úrovni


    public class ConfigEditorViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null) {
            if (!(object.Equals(field, newValue))) {
                field = (newValue);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}