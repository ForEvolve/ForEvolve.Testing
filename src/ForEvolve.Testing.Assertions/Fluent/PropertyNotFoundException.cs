namespace ForEvolve.Testing.Fluent
{
    public class PropertyNotFoundException : FluentException
    {
        public PropertyNotFoundException(string propertyName)
            : base($"The property {propertyName} was not found.")
        {
        }
    }
}
