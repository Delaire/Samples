namespace SlidableItemSample.SlidableItem
{
    /// <summary>
    /// Types of swipe status.
    /// </summary>
    public enum SwipeStatus
    {
        /// <summary>
        /// Swiping is not occuring.
        /// </summary>
        Idle,

        /// <summary>
        /// Swiping is going to start.
        /// </summary>
        Starting,

        /// <summary>
        /// Swiping to the Top, but the command is disabled.
        /// </summary>
        DisabledSwipingToTop,

        /// <summary>
        /// Swiping to the Top below the threshold.
        /// </summary>
        SwipingToTopThreshold,

        /// <summary>
        /// Swiping to the Top above the threshold.
        /// </summary>
        SwipingPassedTopThreshold,

        /// <summary>
        /// Swiping to the Bottom, but the command is disabled.
        /// </summary>
        DisabledSwipingToBottom,

        /// <summary>
        /// Swiping to the Bottom below the threshold.
        /// </summary>
        SwipingToBottomThreshold,

        /// <summary>
        /// Swiping to the Bottom above the threshold.
        /// </summary>
        SwipingPassedBottomThreshold
    }
}