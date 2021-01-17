using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Weather
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public string city { private set; get; }
        public Page1()
        {
            InitializeComponent();
            city = null;
            TextEditor.Focus();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            city = TextEditor.Text;
            Navigation.PopAsync();
        }
    }
}