using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            EditorPage editor = new EditorPage();
            editor.Disappearing += (s, _e) => {
                if (editor.text == null) return;
                Frame frame = new Frame
                {
                    BorderColor = Color.FromHex("#777"),
                    BackgroundColor = Color.FromHex("#fff")
                };

                Label label = new Label();
                label.Text = editor.text;
                frame.Content = label;
                TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (tapsender, tape) =>
                {
                    EditorPage editor2 = new EditorPage(label.Text);
                    editor2.Disappearing += (a, b) =>
                    {
                        if (editor2.text == "")
                        {
                            left.Children.Remove(frame);
                            right.Children.Remove(frame);
                        }
                        else
                        {
                            label.Text = editor2.text;
                        }
                    };
                    Navigation.PushAsync(editor2);
                };
                frame.GestureRecognizers.Add(tapGestureRecognizer);
                SwipeGestureRecognizer swipe = new SwipeGestureRecognizer();

                if (left.Height > right.Height)
                {
                    swipe.Direction = SwipeDirection.Right; 
                    swipe.Swiped += async (swipeSender, swipeEventArg) =>
                    {
                        if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                        {
                            left.Children.Remove(frame);
                            right.Children.Remove(frame);
                        }
                    };
                    frame.GestureRecognizers.Add(swipe);
                    right.Children.Add(frame);
                }
                else
                {
                    swipe.Direction = SwipeDirection.Left;
                    swipe.Swiped += async (swipeSender, swipeEventArg) =>
                    {
                        if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                        {
                            left.Children.Remove(frame);
                            right.Children.Remove(frame);
                        }
                    };
                    frame.GestureRecognizers.Add(swipe);
                    left.Children.Add(frame);
                }


            };
            Navigation.PushAsync(editor);
        }
    }
}
