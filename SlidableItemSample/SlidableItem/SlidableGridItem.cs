using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System.Diagnostics;

namespace SlidableItemSample.SlidableItem
{
    /// <summary>
    /// ContentControl providing functionality for sliding Top or Bottom to expose functions
    /// </summary>
    [TemplatePart(Name = PartContentGrid, Type = typeof(Grid))]
    [TemplatePart(Name = PartCommandContainer, Type = typeof(Grid))]
    [TemplatePart(Name = PartTopCommandPanel, Type = typeof(StackPanel))]
    [TemplatePart(Name = PartBottomCommandPanel, Type = typeof(StackPanel))]
    public class SlidableGridItem : ContentControl
    {
        /// <summary>
        /// Identifies the <see cref="ExtraSwipeThreshold"/> property
        /// </summary>
        public static readonly DependencyProperty ExtraSwipeThresholdProperty =
            DependencyProperty.Register(nameof(ExtraSwipeThreshold), typeof(int), typeof(SlidableGridItem), new PropertyMetadata(default(int)));

        /// <summary>
        /// Identifies the <see cref="IsOffsetLimited"/> property
        /// </summary>
        public static readonly DependencyProperty IsOffsetLimitedProperty =
            DependencyProperty.Register(nameof(IsOffsetLimited), typeof(bool), typeof(SlidableGridItem), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="IsBottomCommandEnabled"/> property
        /// </summary>
        public static readonly DependencyProperty IsBottomCommandEnabledProperty =
            DependencyProperty.Register(nameof(IsBottomCommandEnabled), typeof(bool), typeof(SlidableGridItem), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="IsTopCommandEnabled"/> property
        /// </summary>
        public static readonly DependencyProperty IsTopCommandEnabledProperty =
            DependencyProperty.Register(nameof(IsTopCommandEnabled), typeof(bool), typeof(SlidableGridItem), new PropertyMetadata(true));

        /// <summary>
        /// Identifies the <see cref="ActivationWidth"/> property
        /// </summary>
        public static readonly DependencyProperty ActivationWidthProperty =
            DependencyProperty.Register(nameof(ActivationWidth), typeof(double), typeof(SlidableGridItem), new PropertyMetadata(80));

        /// <summary>
        /// Indeifies the <see cref="TopIcon"/> property
        /// </summary>
        public static readonly DependencyProperty TopIconProperty =
            DependencyProperty.Register(nameof(TopIcon), typeof(Symbol), typeof(SlidableGridItem), new PropertyMetadata(Symbol.Favorite));

        /// <summary>
        /// Identifies the <see cref="BottomIcon"/> property
        /// </summary>
        public static readonly DependencyProperty BottomIconProperty =
            DependencyProperty.Register(nameof(BottomIcon), typeof(Symbol), typeof(SlidableGridItem), new PropertyMetadata(Symbol.Delete));

        /// <summary>
        /// Identifies the <see cref="TopLabel"/> property
        /// </summary>
        public static readonly DependencyProperty TopLabelProperty =
            DependencyProperty.Register(nameof(TopLabel), typeof(string), typeof(SlidableGridItem), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Identifies the <see cref="BottomLabel"/> property
        /// </summary>
        public static readonly DependencyProperty BottomLabelProperty =
            DependencyProperty.Register(nameof(BottomLabel), typeof(string), typeof(SlidableGridItem), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Identifies the <see cref="TopForeground"/> property
        /// </summary>
        public static readonly DependencyProperty TopForegroundProperty =
            DependencyProperty.Register(nameof(TopForeground), typeof(Brush), typeof(SlidableGridItem), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        /// <summary>
        /// Identifies the <see cref="BottomForeground"/> property
        /// </summary>
        public static readonly DependencyProperty BottomForegroundProperty =
            DependencyProperty.Register(nameof(BottomForeground), typeof(Brush), typeof(SlidableGridItem), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        /// <summary>
        /// Identifies the <see cref="TopBackground"/> property
        /// </summary>
        public static readonly DependencyProperty TopBackgroundProperty =
            DependencyProperty.Register(nameof(TopBackground), typeof(Brush), typeof(SlidableGridItem), new PropertyMetadata(new SolidColorBrush(Colors.LightGray)));

        /// <summary>
        /// Identifies the <see cref="BottomBackground"/> property
        /// </summary>
        public static readonly DependencyProperty BottomBackgroundProperty =
            DependencyProperty.Register(nameof(BottomBackground), typeof(Brush), typeof(SlidableGridItem), new PropertyMetadata(new SolidColorBrush(Colors.DarkGray)));

        /// <summary>
        /// Identifies the <see cref="MouseSlidingEnabled"/> property
        /// </summary>
        public static readonly DependencyProperty MouseSlidingEnabledProperty =
            DependencyProperty.Register(nameof(MouseSlidingEnabled), typeof(bool), typeof(SlidableGridItem), new PropertyMetadata(false));

        /// <summary>
        /// Identifies the <see cref="TopCommand"/> property
        /// </summary>
        public static readonly DependencyProperty TopCommandProperty =
            DependencyProperty.Register(nameof(TopCommand), typeof(ICommand), typeof(SlidableGridItem), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="BottomCommand"/> property
        /// </summary>
        public static readonly DependencyProperty BottomCommandProperty =
            DependencyProperty.Register(nameof(BottomCommand), typeof(ICommand), typeof(SlidableGridItem), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="TopCommandParameter"/> property
        /// </summary>
        public static readonly DependencyProperty TopCommandParameterProperty =
            DependencyProperty.Register(nameof(TopCommandParameter), typeof(object), typeof(SlidableGridItem), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="BottomCommandParameter"/> property
        /// </summary>
        public static readonly DependencyProperty BottomCommandParameterProperty =
            DependencyProperty.Register(nameof(BottomCommandParameter), typeof(object), typeof(SlidableGridItem), new PropertyMetadata(null));

        /// <summary>
        /// Identifies the <see cref="SwipeStatus"/> property
        /// </summary>
        public static readonly DependencyProperty SwipeStatusProperty =
            DependencyProperty.Register(nameof(SwipeStatus), typeof(object), typeof(SlidableGridItem), new PropertyMetadata(SwipeStatus.Idle));

        /// <summary>
        /// Identifues the <see cref="IsPointerReleasedOnSwipingHandled"/> property
        /// </summary>
        public static readonly DependencyProperty IsPointerReleasedOnSwipingHandledProperty =
            DependencyProperty.Register("IsPointerReleasedOnSwipingHandled", typeof(bool), typeof(SlidableGridItem), new PropertyMetadata(false));

        /// <summary>
        /// Occurs when SwipeStatus has changed
        /// </summary>
        public event TypedEventHandler<SlidableGridItem, SwipeStatusChangedEventArgs> SwipeStatusChanged;

        private const string PartContentGrid = "ContentGrid";
        private const string PartCommandContainer = "CommandContainer";
        private const string PartTopCommandPanel = "TopCommandPanel";
        private const string PartBottomCommandPanel = "BottomCommandPanel";
        private const int FinishAnimationDuration = 150;
        private const int SnappedCommandMargin = 20;
        private const int AnimationSetDuration = 200;
        private Grid _contentGrid;
        private CompositeTransform _transform;
        private Grid _commandContainer;
        private CompositeTransform _commandContainerTransform;
        private DoubleAnimation _commandContainerClipTranslateAnimation;
        private StackPanel _TopCommandPanel;
        private CompositeTransform _TopCommandTransform;
        private StackPanel _BottomCommandPanel;
        private CompositeTransform _BottomCommandTransform;
        private DoubleAnimation _contentAnimation;
        private Storyboard _contentStoryboard;
        private AnimationSet _TopCommandAnimationSet;
        private AnimationSet _BottomCommandAnimationSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlidableGridItem"/> class.
        /// Creates a new instance of <see cref="SlidableGridItem"/>
        /// </summary>
        public SlidableGridItem()
        {
            DefaultStyleKey = typeof(SlidableGridItem);
        }

        /// <summary>
        /// Occurs when the user swipes to the Top to activate the Bottom action
        /// </summary>
        public event EventHandler BottomCommandRequested;

        /// <summary>
        /// Occurs when the user swipes to the Bottom to activate the Top action
        /// </summary>
        public event EventHandler TopCommandRequested;

        /// <summary>
        /// Invoked whenever application code or internal processes (such as a rebuilding
        /// layout pass) call <see cref="OnApplyTemplate"/>. In simplest terms, this means the method
        /// is called just before a UI element displays in an application. Override this
        /// method to influence the default post-template logic of a class.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            if (_contentGrid != null)
            {
                _contentGrid.PointerReleased -= ContentGrid_PointerReleased;
                _contentGrid.ManipulationStarted -= ContentGrid_ManipulationStarted;
                _contentGrid.ManipulationDelta -= ContentGrid_ManipulationDelta;
                _contentGrid.ManipulationCompleted -= ContentGrid_ManipulationCompleted;
            }

            _contentGrid = GetTemplateChild(PartContentGrid) as Grid;

            if (_contentGrid != null)
            {
                _contentGrid.PointerReleased += ContentGrid_PointerReleased;

                _transform = _contentGrid.RenderTransform as CompositeTransform;
                _contentGrid.ManipulationStarted += ContentGrid_ManipulationStarted;
                _contentGrid.ManipulationDelta += ContentGrid_ManipulationDelta;
                _contentGrid.ManipulationCompleted += ContentGrid_ManipulationCompleted;
            }

            Loaded += SlidableGridItem_Loaded;
            Unloaded += SlidableGridItem_Unloaded;

            base.OnApplyTemplate();
        }

        private void SlidableGridItem_Loaded(object sender, RoutedEventArgs e)
        {
            if (_contentStoryboard != null)
            {
                _contentStoryboard.Completed += ContentStoryboard_Completed;
            }
        }

        private void SlidableGridItem_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_contentStoryboard != null)
            {
                _contentStoryboard.Completed -= ContentStoryboard_Completed;
            }
        }

        private void ContentGrid_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            if (SwipeStatus != SwipeStatus.Idle && IsPointerReleasedOnSwipingHandled)
            {
                e.Handled = true;
            }
        }

        private void ContentGrid_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            if ((!MouseSlidingEnabled && e.PointerDeviceType == PointerDeviceType.Mouse) || (!IsTopCommandEnabled && !IsBottomCommandEnabled))
            {
                return;
            }

            if (_contentStoryboard == null)
            {
                _contentAnimation = new DoubleAnimation();
                Storyboard.SetTarget(_contentAnimation, _transform);
                Storyboard.SetTargetProperty(_contentAnimation, "TranslateY");
                _contentAnimation.To = 0;
                _contentAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(FinishAnimationDuration));

                _contentStoryboard = new Storyboard();
                _contentStoryboard.Children.Add(_contentAnimation);

                _contentStoryboard.Completed += ContentStoryboard_Completed;
            }

            if (_commandContainer == null)
            {
                _commandContainer = GetTemplateChild(PartCommandContainer) as Grid;
                if (_commandContainer != null)
                {
                    _commandContainer.Background = TopBackground as SolidColorBrush;
                    _commandContainer.Clip = new RectangleGeometry();
                    _commandContainerTransform = new CompositeTransform();
                    _commandContainer.Clip.Transform = _commandContainerTransform;

                    _commandContainerClipTranslateAnimation = new DoubleAnimation();
                    Storyboard.SetTarget(_commandContainerClipTranslateAnimation, _commandContainerTransform);
                    Storyboard.SetTargetProperty(_commandContainerClipTranslateAnimation, "TranslateY");
                    _commandContainerClipTranslateAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(FinishAnimationDuration));
                    _contentStoryboard.Children.Add(_commandContainerClipTranslateAnimation);
                }
            }

            if (_TopCommandPanel == null)
            {
                _TopCommandPanel = GetTemplateChild(PartTopCommandPanel) as StackPanel;
                if (_TopCommandPanel != null)
                {
                    _TopCommandTransform = _TopCommandPanel.RenderTransform as CompositeTransform;
                }
            }

            if (_BottomCommandPanel == null)
            {
                _BottomCommandPanel = GetTemplateChild(PartBottomCommandPanel) as StackPanel;
                if (_BottomCommandPanel != null)
                {
                    _BottomCommandTransform = _BottomCommandPanel.RenderTransform as CompositeTransform;
                }
            }

            _contentStoryboard.Stop();
            _commandContainer.Opacity = 0;
            _commandContainerTransform.TranslateY = 0;
            _transform.TranslateY = 0;
            SwipeStatus = SwipeStatus.Starting;
        }

        /// <summary>
        /// Handler for when slide manipulation is complete
        /// </summary>
        private void ContentGrid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (SwipeStatus == SwipeStatus.Idle)
            {
                return;
            }

            var y = _transform.TranslateY;
            _contentAnimation.From = y;
            _commandContainerClipTranslateAnimation.From = 0;
            _commandContainerClipTranslateAnimation.To = -y;
            _contentStoryboard.Begin();

            if (SwipeStatus == SwipeStatus.SwipingPassedTopThreshold)
            {
                BottomCommandRequested?.Invoke(this, EventArgs.Empty);
                BottomCommand?.Execute(BottomCommandParameter);
            }
            else if (SwipeStatus == SwipeStatus.SwipingPassedBottomThreshold)
            {
                TopCommandRequested?.Invoke(this, EventArgs.Empty);
                TopCommand?.Execute(TopCommandParameter);
            }

            Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () => { SwipeStatus = SwipeStatus.Idle; }).AsTask();
        }

        /// <summary>
        /// Handler for when slide manipulation is underway
        /// </summary>
        private void ContentGrid_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (SwipeStatus == SwipeStatus.Idle)
            {
                return;
            }

            //    var newTranslationX = _transform.TranslateX + e.Delta.Translation.X;
            var newTranslationY = _transform.TranslateY + e.Delta.Translation.Y;


            bool swipingInDisabledArea = false;
            SwipeStatus newSwipeStatus = SwipeStatus.Idle;

            if (newTranslationY > 0)
            {
                // Swiping from Top to Bottom
                if (!IsTopCommandEnabled)
                {
                    // If swipe is not enabled, only allow swipe a very short distance
                    if (newTranslationY > 0)
                    {
                        swipingInDisabledArea = true;
                        newSwipeStatus = SwipeStatus.DisabledSwipingToBottom;
                    }

                    if (newTranslationY > 16)
                    {
                        newTranslationY = 16;
                    }
                }
                else if (IsOffsetLimited)
                {
                    // If offset is limited, there will be a limit how much swipe is possible.
                    // This will be the value of the command panel plus some threshold.
                    // This value can't be less than the ActivationWidth.
                    var swipeThreshold = _TopCommandPanel.ActualHeight + ExtraSwipeThreshold;
                    if (swipeThreshold < ActivationWidth)
                    {
                        swipeThreshold = ActivationWidth;
                    }

                    if (Math.Abs(newTranslationY) > swipeThreshold)
                    {
                        newTranslationY = swipeThreshold;
                    }
                }

                // Don't allow swiping more than almost the whole content grid width
                // (doing this will cause the control to change size).
                if (newTranslationY > (_contentGrid.ActualHeight - 4))
                {
                    newTranslationY = _contentGrid.ActualHeight - 4;
                }
            }
            else
            {
                // Swiping from Bottom to Top
                if (!IsBottomCommandEnabled)
                {
                    // If swipe is not enabled, only allow swipe a very short distance
                    if (newTranslationY < 0)
                    {
                        swipingInDisabledArea = true;
                        newSwipeStatus = SwipeStatus.DisabledSwipingToTop;
                    }

                    if (newTranslationY < -16)
                    {
                        newTranslationY = -16;
                    }
                }
                else if (IsOffsetLimited)
                {
                    // If offset is limited, there will be a limit how much swipe is possible.
                    // This will be the value of the command panel plus some threshold.
                    // This value can't be less than the ActivationWidth.
                    var swipeThreshold = _BottomCommandPanel.ActualHeight + ExtraSwipeThreshold;
                    if (swipeThreshold < ActivationWidth)
                    {
                        swipeThreshold = ActivationWidth;
                    }

                    if (Math.Abs(newTranslationY) > swipeThreshold)
                    {
                        newTranslationY = -swipeThreshold;
                    }
                }

                // Don't allow swiping more than almost the whole content grid width
                // (doing this will cause the control to change size).
                if (newTranslationY < -(_contentGrid.ActualHeight - 4))
                {
                    newTranslationY = -(_contentGrid.ActualHeight - 4);
                }
            }

            bool hasPassedThreshold = !swipingInDisabledArea && Math.Abs(newTranslationY) >= ActivationWidth;

            if (swipingInDisabledArea)
            {
                // Don't show any command if swiping in disabled area.
                _commandContainer.Opacity = 0;
                _TopCommandPanel.Opacity = 0;
                _BottomCommandPanel.Opacity = 0;
            }
            else if (newTranslationY > 0)
            {
                // If swiping from Top to Bottom, show Top command panel.
                _BottomCommandPanel.Opacity = 0;

                _commandContainer.Background = TopBackground as SolidColorBrush;
                _commandContainer.Opacity = 1;
                _TopCommandPanel.Opacity = 1;

                _commandContainer.Clip.Rect = new Rect(0, 0, _commandContainer.ActualWidth, Math.Max(newTranslationY - 1, 0));

                if (newTranslationY < ActivationWidth)
                {
                    _TopCommandAnimationSet?.Stop();
                    _TopCommandPanel.RenderTransform = _TopCommandTransform;
                    _TopCommandTransform.TranslateY = newTranslationY / 2;

                    newSwipeStatus = SwipeStatus.SwipingToBottomThreshold;
                }
                else
                {
                    if (SwipeStatus != SwipeStatus.SwipingPassedBottomThreshold)
                    {
                        // This will cover extrem cases when previous state wasn't
                        // below threshold.
                        _TopCommandAnimationSet?.Stop();
                        _TopCommandPanel.RenderTransform = _TopCommandTransform;
                        _TopCommandTransform.TranslateY = SnappedCommandMargin;
                    }

                    newSwipeStatus = SwipeStatus.SwipingPassedBottomThreshold;
                }
            }
            else
            {
                // If swiping from Bottom to Top, show Bottom command panel.
                _TopCommandPanel.Opacity = 0;

                _commandContainer.Background = BottomBackground as SolidColorBrush;
                _commandContainer.Opacity = 1;
                _BottomCommandPanel.Opacity = 1;

         
                _commandContainer.Clip.Rect = new Rect(0,
                    0,
                    _commandContainer.ActualWidth,
                    _commandContainer.ActualHeight);
             
                Debug.WriteLine("Bottom: " + _commandContainer.Clip.Rect.Bottom);
                Debug.WriteLine("Top: " + _commandContainer.Clip.Rect.Top);


                if (-newTranslationY < ActivationWidth)
                {
                    _BottomCommandAnimationSet?.Stop();
                    _BottomCommandPanel.RenderTransform = _BottomCommandTransform;
                    _BottomCommandTransform.TranslateY = newTranslationY / 2;

                    newSwipeStatus = SwipeStatus.SwipingToTopThreshold;
                }
                else
                {
                     if (SwipeStatus != SwipeStatus.SwipingPassedTopThreshold)
                    {
                        // This will cover extrem cases when previous state wasn't
                        // below threshold.
                        _BottomCommandAnimationSet?.Stop();
                        _BottomCommandPanel.RenderTransform = _BottomCommandTransform;
                        _BottomCommandTransform.TranslateY = -SnappedCommandMargin;
                    }

                    newSwipeStatus = SwipeStatus.SwipingPassedTopThreshold;
                }
            }

            _transform.TranslateY = newTranslationY;



            SwipeStatus = newSwipeStatus;
        }

        private void ContentStoryboard_Completed(object sender, object e)
        {
            _commandContainer.Opacity = 0.0;
        }

        /// <summary>
        /// Gets or sets the amount of extra pixels for swipe threshold when <see cref="IsOffsetLimited"/> is enabled.
        /// </summary>
        public int ExtraSwipeThreshold
        {
            get { return (int)GetValue(ExtraSwipeThresholdProperty); }
            set { SetValue(ExtraSwipeThresholdProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether maximum swipe offset is limited or not.
        /// </summary>
        public bool IsOffsetLimited
        {
            get { return (bool)GetValue(IsOffsetLimitedProperty); }
            set { SetValue(IsOffsetLimitedProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Bottom command is enabled or not.
        /// </summary>
        public bool IsBottomCommandEnabled
        {
            get { return (bool)GetValue(IsBottomCommandEnabledProperty); }
            set { SetValue(IsBottomCommandEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Top command is enabled or not.
        /// </summary>
        public bool IsTopCommandEnabled
        {
            get { return (bool)GetValue(IsTopCommandEnabledProperty); }
            set { SetValue(IsTopCommandEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the amount of pixels the content needs to be swiped for an
        /// action to be requested
        /// </summary>
        public double ActivationWidth
        {
            get { return (double)GetValue(ActivationWidthProperty); }
            set { SetValue(ActivationWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Top icon symbol
        /// </summary>
        public Symbol TopIcon
        {
            get { return (Symbol)GetValue(TopIconProperty); }
            set { SetValue(TopIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Bottom icon symbol
        /// </summary>
        public Symbol BottomIcon
        {
            get { return (Symbol)GetValue(BottomIconProperty); }
            set { SetValue(BottomIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Top label
        /// </summary>
        public string TopLabel
        {
            get { return (string)GetValue(TopLabelProperty); }
            set { SetValue(TopLabelProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Bottom label
        /// </summary>
        public string BottomLabel
        {
            get { return (string)GetValue(BottomLabelProperty); }
            set { SetValue(BottomLabelProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Top foreground color
        /// </summary>
        public Brush TopForeground
        {
            get { return (Brush)GetValue(TopForegroundProperty); }
            set { SetValue(TopForegroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Bottom foreground color
        /// </summary>
        public Brush BottomForeground
        {
            get { return (Brush)GetValue(BottomForegroundProperty); }
            set { SetValue(BottomForegroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Top background color
        /// </summary>
        public Brush TopBackground
        {
            get { return (Brush)GetValue(TopBackgroundProperty); }
            set { SetValue(TopBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets the Bottom background color
        /// </summary>
        public Brush BottomBackground
        {
            get { return (Brush)GetValue(BottomBackgroundProperty); }
            set { SetValue(BottomBackgroundProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether it has the ability to slide the control with the mouse. False by default
        /// </summary>
        public bool MouseSlidingEnabled
        {
            get { return (bool)GetValue(MouseSlidingEnabledProperty); }
            set { SetValue(MouseSlidingEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets the ICommand for Top command request
        /// </summary>
        public ICommand TopCommand
        {
            get
            {
                return (ICommand)GetValue(TopCommandProperty);
            }

            set
            {
                SetValue(TopCommandProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the ICommand for Bottom command request
        /// </summary>
        public ICommand BottomCommand
        {
            get
            {
                return (ICommand)GetValue(BottomCommandProperty);
            }

            set
            {
                SetValue(BottomCommandProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the CommandParameter for Top command request
        /// </summary>
        public object TopCommandParameter
        {
            get
            {
                return GetValue(TopCommandParameterProperty);
            }

            set
            {
                SetValue(TopCommandParameterProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the CommandParameter for Bottom command request
        /// </summary>
        public object BottomCommandParameter
        {
            get
            {
                return GetValue(BottomCommandParameterProperty);
            }

            set
            {
                SetValue(BottomCommandParameterProperty, value);
            }
        }

        /// <summary>
        /// Gets the SwipeStatus for current swipe status
        /// </summary>
        public SwipeStatus SwipeStatus
        {
            get
            {
                return (SwipeStatus)GetValue(SwipeStatusProperty);
            }

            private set
            {
                var oldValue = SwipeStatus;

                if (value != oldValue)
                {
                    SetValue(SwipeStatusProperty, value);

                    var eventArguments = new SwipeStatusChangedEventArgs()
                    {
                        OldValue = oldValue,
                        NewValue = value
                    };

                    SwipeStatusChanged?.Invoke(this, eventArguments);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the PointerReleased event is handled when swiping.
        /// Set this to <value>true</value> to prevent ItemClicked or selection to occur when swiping in a <see cref="ListView"/>
        /// </summary>
        public bool IsPointerReleasedOnSwipingHandled
        {
            get { return (bool)GetValue(IsPointerReleasedOnSwipingHandledProperty); }
            set { SetValue(IsPointerReleasedOnSwipingHandledProperty, value); }
        }
    }
}

