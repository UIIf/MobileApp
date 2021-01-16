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

        private void DrawMainPage()
        {
            left.Children.Clear();
            for(int i = 0; i < left_arr.Count; i++)
            {
                left.Children.Add(left_arr[i]);
            }

            right.Children.Clear();
            for (int i = 0; i < right_arr.Count; i++)
            {
                right.Children.Add(right_arr[i]);
            }
        }

        List<Frame> left_arr = new List<Frame>();
        List<Frame> right_arr = new List<Frame>();
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
                            double lH = left.Height;
                            double rH = right.Height;

                            if (left_arr.Contains(frame))
                            {
                                lH -= frame.Height;
                                left_arr.Remove(frame);
                                while (lH + 5 < rH && right_arr[right_arr.Count - 1].Height < rH - lH)
                                {
                                    lH += right_arr[right_arr.Count - 1].Height;
                                    rH -= right_arr[right_arr.Count - 1].Height;
                                    left_arr.Add(right_arr[right_arr.Count - 1]);
                                    right_arr.RemoveAt(right_arr.Count - 1);
                                }
                            }
                            else
                            {

                                rH -= frame.Height;
                                right_arr.Remove(frame);
                                while (rH + 5 < lH && left_arr[left_arr.Count - 1].Height < lH - rH)
                                {
                                    rH += left_arr[left_arr.Count - 1].Height;
                                    lH -= left_arr[left_arr.Count - 1].Height;
                                    right_arr.Add(left_arr[left_arr.Count - 1]);
                                    left_arr.RemoveAt(left_arr.Count - 1);
                                }
                            }
                        }
                        else
                        {
                            label.Text = editor2.text;
                        }

                        //if (left_arr.Contains(frame))
                        //{
                        //    if (editor2.text == "")
                        //    {
                        //        lH -= frame.Height;
                        //        left_arr.Remove(frame);
                        //    }
                        //    while (lH + 2 < rH)
                        //    {
                        //        lH += right_arr[right_arr.Count - 1].Height;
                        //        rH -= right_arr[right_arr.Count - 1].Height;
                        //        left_arr.Add(right_arr[right_arr.Count - 1]);
                        //        right_arr.RemoveAt(right_arr.Count - 1);
                        //    }
                        //}
                        //else
                        //{
                        //    if (editor2.text == "")
                        //    {
                        //        rH -= frame.Height;
                        //        right_arr.Remove(frame);
                        //    }
                        //    while (rH + 2< lH)
                        //    {
                        //        rH += left_arr[left_arr.Count - 1].Height;
                        //        lH -= left_arr[left_arr.Count - 1].Height;
                        //        right_arr.Add(left_arr[left_arr.Count - 1]);
                        //        left_arr.RemoveAt(left_arr.Count - 1);
                        //    }
                        //}
                       
                        
                        DrawMainPage();
                    };
                    Navigation.PushAsync(editor2);
                };
                frame.GestureRecognizers.Add(tapGestureRecognizer);
                SwipeGestureRecognizer swipe = new SwipeGestureRecognizer();

                swipe.Direction = SwipeDirection.Right;
                swipe.Swiped += async (swipeSender, swipeEventArg) =>
                {
                    if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                    {
                        double lH = left.Height;
                        double rH = right.Height;

                        if (left_arr.Contains(frame))
                        {
                            lH -= frame.Height;
                            left_arr.Remove(frame);
                            while (lH + 5 < rH && right_arr[right_arr.Count - 1].Height < rH - lH)
                            {
                                lH += right_arr[right_arr.Count - 1].Height;
                                rH -= right_arr[right_arr.Count - 1].Height;
                                left_arr.Add(right_arr[right_arr.Count - 1]);
                                right_arr.RemoveAt(right_arr.Count - 1);
                            }
                        }
                        else
                        {

                            rH -= frame.Height;
                            right_arr.Remove(frame);
                            while (rH + 5 < lH && left_arr[left_arr.Count - 1].Height < lH - rH)
                            {
                                rH += left_arr[left_arr.Count - 1].Height;
                                lH -= left_arr[left_arr.Count - 1].Height;
                                right_arr.Add(left_arr[left_arr.Count - 1]);
                                left_arr.RemoveAt(left_arr.Count - 1);
                            }
                        }
                    }
                };
                frame.GestureRecognizers.Add(swipe);

                swipe.Direction = SwipeDirection.Left;
                swipe.Swiped += async (swipeSender, swipeEventArg) =>
                {
                    if (await DisplayAlert("Confirm the deleting", "Are you sure?", "Yes!", "No"))
                    {
                        double lH = left.Height;
                        double rH = right.Height;

                        if (left_arr.Contains(frame))
                        {
                            lH -= frame.Height;
                            left_arr.Remove(frame);
                            while (lH + 5 < rH && right_arr[right_arr.Count - 1].Height < rH - lH)
                            {
                                lH += right_arr[right_arr.Count - 1].Height;
                                rH -= right_arr[right_arr.Count - 1].Height;
                                left_arr.Add(right_arr[right_arr.Count - 1]);
                                right_arr.RemoveAt(right_arr.Count - 1);
                            }
                        }
                        else
                        {

                            rH -= frame.Height;
                            right_arr.Remove(frame);
                            while (rH + 5 < lH && left_arr[left_arr.Count - 1].Height < lH - rH)
                            {
                                rH += left_arr[left_arr.Count - 1].Height;
                                lH -= left_arr[left_arr.Count - 1].Height;
                                right_arr.Add(left_arr[left_arr.Count - 1]);
                                left_arr.RemoveAt(left_arr.Count - 1);
                            }
                        }
                    }
                };
                frame.GestureRecognizers.Add(swipe);

                if (left.Height > right.Height)
                {                  
                    right_arr.Add(frame);
                }
                else
                { 
                    left_arr.Add(frame);
                }

                DrawMainPage();
            };
            Navigation.PushAsync(editor);
        }
    }
}
