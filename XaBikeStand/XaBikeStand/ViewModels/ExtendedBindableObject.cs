using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XaBikeStand.ViewModels
{
    public abstract class ExtendedBindableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        protected void propertyIsChanged([CallerMemberName] string memberName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
        }
    }
}
