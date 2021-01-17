using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App7
{
    public class Good
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string PicPath { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {
        public static SortedDictionary<string, Good> goods = new SortedDictionary<string, Good>();
        public Cart()
        {
            InitializeComponent();
            var buf = goods.Select((a) => { return a.Value; }).ToList();
            goods_list.ItemsSource = buf;
            if(goods.Count < 1)
            {
                OrderBtn.IsEnabled = false;
            }
            else
            {
                OrderBtn.IsEnabled = true;
            }


            //goods_list.ItemsSource = buf;

            //foreach(var i in goods)
            //{
            //    StackLayout goodStack = new StackLayout();
            //    //goodStack.Orientation = StackOrientation.Horizontal;
            //    //goodStack.Children.Add(new Image());
            //    //goodStack.Children.Add(new Label() { Text = i.Value.name });

            //    Label countLabel = new Label() { Text = i.Value.count.ToString()};

            //    goodStack.Children.Add(countLabel);

            //    Stepper itemCounter = new Stepper();
            //    itemCounter.Value = Cart.goods[i.Key].count;
            //    goodStack.Children.Add(itemCounter);

            //    Frame goodFrame = new Frame() { Content = goodStack };

            //    itemCounter.ValueChanged += async (a, b) => {
            //        Cart.goods[i.Key].count = (int)itemCounter.Value;
            //        countLabel.Text = Cart.goods[i.Key].count.ToString();
            //        if(Cart.goods[i.Key].count == 0)
            //        {
            //            if (await DisplayAlert("Attention", "You sure?", "Yes", "No"))
            //            {
            //                cart.Children.Remove(goodFrame);
            //                goods.Remove(i.Key);
            //            }
            //            else
            //            {
            //                Cart.goods[i.Key].count = 1;
            //                countLabel.Text = Cart.goods[i.Key].count.ToString();
            //                itemCounter.Value = 1;
            //            }
            //        }
            //    };

            //    Button frameRemover = new Button() { Text = "X" };

            //    goodStack.Children.Add(frameRemover);

            //    frameRemover.Clicked += (a,b) => {

            //        cart.Children.Remove(goodFrame);
            //        goods.Remove(i.Key);
            //    };




            //    cart.Children.Add(goodFrame);
            //}
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (goods.Count() > 0)
            {
                if (await DisplayAlert("Order", "Do u really need it?", "Yes", "No"))
                {
                    await DisplayAlert("Order", "U did it. What now?", "Ok");
                    goods.Clear();
                    await Navigation.PopAsync();
                }
            }
        }

        public void DeleteClicked(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Button)sender;
            var listitem = (from itm in goods
                            where itm.Value.Name == item.CommandParameter.ToString()
                            select itm).ToList().First();


            goods.Remove(listitem.Key);

            var buf = goods.Select((a) => { return a.Value; }).ToList();
            goods_list.ItemsSource = buf;

        }
    }
}