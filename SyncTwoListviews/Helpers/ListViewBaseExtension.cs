using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;


namespace DoubleListViewItemTemplates.Helpers
{
    public static class ListViewBaseExtension
    {
        public static ScrollViewer GetScrollViewer(this DependencyObject element)
        {
            if (element is ScrollViewer)
            {
                return (ScrollViewer)element;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                var child = VisualTreeHelper.GetChild(element, i);

                var result = GetScrollViewer(child);
                if (result == null)
                {
                    continue;
                }
                else
                {
                    return result;
                }
            }

            return null;
        }

        public static T GetChildOfType<T>(this DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }

        public async static Task ScrollToIndex(this ListViewBase listViewBase, int index)
        {
            bool isVirtualizing = default(bool);
            double previousHorizontalOffset = default(double), previousVerticalOffset = default(double);

            // get the ScrollViewer withtin the ListView/GridView
            var scrollViewer = listViewBase.GetScrollViewer();
            // get the SelectorItem to scroll to
            var selectorItem = listViewBase.ContainerFromIndex(index) as SelectorItem;

            // when it's null, means virtualization is on and the item hasn't been realized yet
            if (selectorItem == null)
            {
                isVirtualizing = true;

                previousHorizontalOffset = scrollViewer.HorizontalOffset;
                previousVerticalOffset = scrollViewer.VerticalOffset;

                // call task-based ScrollIntoViewAsync to realize the item
                await listViewBase.ScrollIntoViewAsync(listViewBase.Items[index]);

                // this time the item shouldn't be null again
                selectorItem = (SelectorItem)listViewBase.ContainerFromIndex(index);
            }

            // calculate the position object in order to know how much to scroll to
            var transform = selectorItem.TransformToVisual((UIElement)scrollViewer.Content);
            //var position = transform.TransformPoint(new Point(0, -60));
            var position = transform.TransformPoint(new Point(0, 0));

            // when virtualized, scroll back to previous position without animation
            if (isVirtualizing)
            {
                await scrollViewer.ChangeViewAsync(previousHorizontalOffset, previousVerticalOffset, true);
            }

            // scroll to desired position with animation!
            scrollViewer.ChangeView(position.X, position.Y, null, false);
            // await Task.Delay(2);



            //Debug.WriteLine("position.X : " + position.X);
            //Debug.WriteLine("position.Y : " + position.Y);
            //Debug.WriteLine("scrollViewer.HorizontalOffset : " + scrollViewer.HorizontalOffset);
            //Debug.WriteLine("scrollViewer.VerticalOffset : " + scrollViewer.VerticalOffset);
            //Debug.WriteLine("--------------------------");
        }
        public static Dictionary<int, double> GetAllItemsPositions(this ListViewBase listViewBase)
        {
            Dictionary<int, double> SegmentPositon = new Dictionary<int, double>();
            // get the ScrollViewer withtin the ListView/GridView
            var scrollViewer = listViewBase.GetScrollViewer();

            try
            {
                for (int index = 0; index < listViewBase.Items.Count; index++)
                {
                    // get the SelectorItem to scroll to
                    var selectorItem = listViewBase.ContainerFromIndex(index) as SelectorItem;

                    // when it's null, means virtualization is on and the item hasn't been realized yet
                    if (selectorItem == null)
                    {                         
                        // this time the item shouldn't be null again
                        selectorItem = (SelectorItem)listViewBase.ContainerFromIndex(index);
                    }

                    if (selectorItem != null)
                    {
                        // calculate the position object in order to know how much to scroll to
                        var transform = selectorItem.TransformToVisual((UIElement)scrollViewer.Content);
                        var position = transform.TransformPoint(new Point(0, -60));

                        //Debug.WriteLine("position.Y : " + position.Y);
                        //Debug.WriteLine("--------------------------");

                        SegmentPositon.Add(index, position.Y);
                    }
                }
            }
            catch (Exception)
            {


            }

            return SegmentPositon;
        }

        public static async Task ScrollIntoViewAsync(this ListViewBase listViewBase, object item)
        {
            var tcs = new TaskCompletionSource<object>();
            var scrollViewer = listViewBase.GetScrollViewer();

            if (scrollViewer != null)
            {
                EventHandler<ScrollViewerViewChangedEventArgs> viewChanged = (s, e) => tcs.TrySetResult(null);
                try
                {
                    scrollViewer.ViewChanged += viewChanged;
                    listViewBase.ScrollIntoView(item, ScrollIntoViewAlignment.Leading);

                    await tcs.Task;
                }
                finally
                {
                    scrollViewer.ViewChanged -= viewChanged;
                }
            }
        }

        public static async Task ScrollIntoViewAsync(this ListViewBase listViewBase, int index)
        {
            var tcs = new TaskCompletionSource<object>();
            var scrollViewer = listViewBase.GetScrollViewer();
            if (scrollViewer != null)
            {
                EventHandler<ScrollViewerViewChangedEventArgs> viewChanged = (s, e) => tcs.TrySetResult(null);
                try
                {
                    scrollViewer.ViewChanged += viewChanged;
                    await listViewBase.ScrollToIndex(index);

                    await tcs.Task;
                }
                finally
                {
                    scrollViewer.ViewChanged -= viewChanged;
                }
            }
        }

        public static async Task ChangeViewAsync(this ScrollViewer scrollViewer, double? horizontalOffset, double? verticalOffset, bool disableAnimation)
        {
            var tcs = new TaskCompletionSource<object>();

            EventHandler<ScrollViewerViewChangedEventArgs> viewChanged = (s, e) => tcs.TrySetResult(null);
            try
            {
                scrollViewer.ViewChanged += viewChanged;
                scrollViewer.ChangeView(horizontalOffset, verticalOffset, null, disableAnimation);
                await tcs.Task;
            }
            finally
            {
                scrollViewer.ViewChanged -= viewChanged;
            }
        }
    }
}
