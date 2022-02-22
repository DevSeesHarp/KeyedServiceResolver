using Xunit;
using System;
using Devseesharp.KeyedServices;

namespace KeyedServices.Core.Test
{
    public class KeyedServiceResolverTest
    {
        public enum FakeEnumKeys
        {
            Key1,
            Key2,
            Key3
        }

        [Fact]
        public void PairedValueForKeyMustReturnCorrectValue()
        {
            // Arrange
            var key = FakeEnumKeys.Key1;
            KeyedServiceResolver<FakeEnumKeys, string>.ResolverDelegate resolver = key =>
            {
                return key switch
                {
                    FakeEnumKeys.Key1 => "1",
                    FakeEnumKeys.Key2 => "2",
                    _ => throw new ArgumentOutOfRangeException(nameof(key), key, null)
                };
            };
            // Act
            var actual = resolver(key);

            // Assert
            Assert.Equal("1", actual);
        }
    }
}