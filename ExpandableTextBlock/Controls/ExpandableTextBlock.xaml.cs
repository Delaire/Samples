using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ExpandableTextBlock.Controls
{
    public sealed partial class ExpandableTextBlock : UserControl
    {

        public ExpandableTextBlock()
        {
            this.InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(ExpandableTextBlock), new PropertyMetadata(default(string), OnTextChanged));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public double MaxHeight { get; set; }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (ExpandableTextBlock)d;
            ctl.CommentTextBlock.SetValue(TextBlock.TextProperty, (string)e.NewValue);
            ctl.CommentTextBlock.SetValue(TextBlock.HeightProperty, Double.NaN);

            ctl.CommentTextBlock.Measure(new Size(ctl.CommentTextBlock.Width, double.MaxValue));

            double desiredheight = ctl.CommentTextBlock.DesiredSize.Height;
            ctl.CommentTextBlock.SetValue(TextBlock.HeightProperty, (double)63);

            if (desiredheight > (double)ctl.CommentTextBlock.GetValue(TextBlock.HeightProperty))
            {
                ctl.ExpandHint.SetValue(StackPanel.VisibilityProperty, Visibility.Visible);
                ctl.MaxHeight = desiredheight;
            }
            else
            {
                ctl.ExpandHint.SetValue(StackPanel.VisibilityProperty, Visibility.Collapsed);
            }

            //Setting length of comments
            var boundsWidth = Window.Current.Bounds.Width;
            ctl.CommentTextBlock.SetValue(TextBlock.WidthProperty, boundsWidth);
        }

        public static readonly DependencyProperty CollapsedHeightProperty = DependencyProperty.Register(
            "CollapsedHeight", typeof(double), typeof(ExpandableTextBlock), new PropertyMetadata(default(double), OnCollapsedHeightChanged));


        public double CollapsedHeight
        {
            get { return (double)GetValue(CollapsedHeightProperty); }
            set { SetValue(CollapsedHeightProperty, value); }
        }

        private static void OnCollapsedHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctl = (ExpandableTextBlock)d;
            ctl.CollapsedHeight = (double)e.NewValue;
        }


        private void LayoutRoot_OnTap(object sender, TappedRoutedEventArgs tappedRoutedEventArgs)
        {
           if ((Visibility)this.ExpandHint.GetValue(StackPanel.VisibilityProperty) == Visibility.Visible)
            {
                //transition
                this.CommentTextBlock.SetValue(TextBlock.HeightProperty, Double.NaN);

                this.ExpandHint.SetValue(StackPanel.VisibilityProperty, Visibility.Collapsed);
            }
        }
    }

}
