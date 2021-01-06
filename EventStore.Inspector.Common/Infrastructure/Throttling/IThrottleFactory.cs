namespace EventStore.Inspector.Common.Infrastructure.Throttling
{
    /// <summary>
    /// Factory to create a throttling mechanism <see cref="IThrottle"/>.
    /// </summary>
    public interface IThrottleFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="IThrottle"/> based on the specified
        /// options.
        /// </summary>
        /// <param name="options">Throttling options used to decide which <see cref="IThrottle"/>
        /// implementation to create.</param>
        /// <returns>A new instance of <see cref="IThrottle"/> based on the specified options.</returns>
        IThrottle Create(ThrottleOptions options);
    }
}
