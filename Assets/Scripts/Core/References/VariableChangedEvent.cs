namespace Core.References
{
    public delegate void VariableChangedEvent<T>(object sender, VariableChangedEventArgs<T> args);

    public class VariableChangedEventArgs<T>
    {
        public VariableChangedEventArgs(T oldValue, T newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public T oldValue { get; }

        public T newValue { get; }
    }
}