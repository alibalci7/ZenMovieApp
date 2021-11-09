using System.Collections.ObjectModel;
using System.ComponentModel;
using ZenMovieApp2.Models;

namespace ZenMovieApp2.ViewModels
{
    public class FilmModelView: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<Film> _items;
        public ObservableCollection<Film> Items
        {
            get
            {
                if (_items == null)
                    _items = new ObservableCollection<Film>();

                return _items;
            }
            set
            {
                _items = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Items"));
            }
        }

        bool isLoading;
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                if (isLoading != value)
                {
                    isLoading = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsLoading"));
                }
            }
        }

        public async void LoadData(int sonid)
        {
            this.IsLoading = true;

            this.IsLoading = false;
        }
    }
}
