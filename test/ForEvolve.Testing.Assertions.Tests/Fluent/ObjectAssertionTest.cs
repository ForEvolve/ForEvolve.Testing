using ForEvolve.Testing.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace ForEvolve.Testing.AssertExtensions.Fluent
{
    public class ObjectAssertionTest
    {
        public class OwnProperty : ObjectAssertionTest
        {
            [Fact]
            public void Should_not_throw_when_the_property_equals_the_expected_value()
            {
                // Arrange
                var input = new { SomeProperty = "Some value" };

                // Act & Assert
                input.Should().OwnProperty("SomeProperty")
                    .That().Is().EqualTo("Some value");
            }

            [Fact]
            public void Should_throw_PropertyNotFoundException_when_the_property_does_not_exist()
            {
                // Arrange
                var input = new { SomeProperty = "Some value" };

                // Act & Assert
                Assert.Throws<PropertyNotFoundException>(() 
                    => input.Should().OwnProperty("DoesNotExist"));
            }
        }

        public class EqualTo : ObjectAssertionTest
        {
            [Fact]
            public void Should_not_throw_when_the_object_equals_the_expected_value()
            {
                // Arrange
                var input = (object)"Some value";

                // Act & Assert
                input.Should().Be().EqualTo("Some value");
            }

            [Fact]
            public void Should_throw_an_EqualException_when_the_object_is_not_equal_to_the_excepted_value()
            {
                // Arrange
                var input = (object)"Some value";

                // Act & Assert
                Assert.Throws<EqualException>(()
                    => input.Should().Be().EqualTo("Not the same"));
            }

            [Fact]
            public void Should_compare_complex_object_trees()
            {
                //
                // TODO: create test cases that compare object and arrays in .Should().Equal()
                //
                // Arrange


                // Act


                // Assert
                throw new NotImplementedException();
            }

        }

        public class Empty : ObjectAssertionTest
        {
            [Fact]
            public void Should_not_throw_when_the_collection_is_empty()
            {
                // Arrange
                var input = Enumerable.Empty<string>();

                // Act & Assert
                input.Should().Be().Empty();
            }

            [Fact]
            public void Should_throw_an_IsAssignableFromException_when_the_object_is_not_an_IEnumerable()
            {
                // Arrange
                var input = new { };

                // Act & Assert
                Assert.Throws<IsAssignableFromException>(()
                    => input.Should().Be().Empty());
            }


            [Fact]
            public void Should_throw_an_EmptyException_when_the_collection_is_not_empty()
            {
                // Arrange
                var input = new[] { 1, 2, 3 };

                // Act & Assert
                Assert.Throws<EmptyException>(()
                    => input.Should().Be().Empty());
            }
        }
    }
}
