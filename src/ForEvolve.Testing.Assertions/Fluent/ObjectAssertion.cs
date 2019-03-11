using System;
using System.Collections;
using Xunit;

namespace ForEvolve.Testing.Fluent
{
    public class ObjectAssertion : BaseAssertion
    {
        private readonly object _object;
        public ObjectAssertion(object @object)
        {
            _object = @object ?? throw new ArgumentNullException(nameof(@object));
        }

        public ObjectAssertion OwnProperty(string propertyName)
        {
            var property = _object.GetType().GetProperty(propertyName);
            if (property == null)
            {
                throw new PropertyNotFoundException(propertyName);
            }
            var propertyValue = property.GetValue(_object, null);
            return new ObjectAssertion(propertyValue);
        }

        public ObjectAssertion EqualTo(object expectedValue)
        {
            Assert.Equal(expectedValue, _object);
            return this;
        }

        public ObjectAssertion Equal(object expectedValue)
        {
            return EqualTo(expectedValue);
        }

        public new ObjectAssertion Equals(object expectedValue)
        {
            return EqualTo(expectedValue);
        }

        public ObjectAssertion Empty()
        {
            Assert.IsAssignableFrom<IEnumerable>(_object);
            Assert.Empty(_object as IEnumerable);
            return this;
        }
    }
}