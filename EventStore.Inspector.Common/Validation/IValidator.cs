namespace EventStore.Inspector.Common.Validation
{
    public interface IValidator<T>
    {
        T Validate(T value);
    }
}
