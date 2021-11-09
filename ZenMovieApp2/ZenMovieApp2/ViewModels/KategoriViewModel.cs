using System.Collections.Generic;
using ZenMovieApp2.Models;

namespace ZenMovieApp2.ViewModels
{
    public class KategoriViewModel : ViewModelBase
	{
		public IList<Kategori> Kategoriler { get; set; }

		Kategori selectedKategori;
		public Kategori SelectedKategori
		{
			get { return selectedKategori; }
			set
			{
				if (selectedKategori != value)
				{
					selectedKategori = value;
					OnPropertyChanged();
				}
			}
		}
	}
}
