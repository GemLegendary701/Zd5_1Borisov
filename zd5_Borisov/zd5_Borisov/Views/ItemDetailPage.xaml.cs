using System.ComponentModel;
using Xamarin.Forms;
using zd5_Borisov.ViewModels;

namespace zd5_Borisov.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}