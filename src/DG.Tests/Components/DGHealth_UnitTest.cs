using DG.Core.Components.Common;

namespace DG.Tests.Components
{
    public sealed class DGHealth_UnitTest
    {
        [Fact]
        public void IsDead_WhenCurrentHealthIsZero_ReturnsTrue()
        {
            // Arrange
            DGHealth health = new();
            health.SetCurrentHealth(0);

            // Act
            bool isDead = health.IsDead;

            // Assert
            Assert.True(isDead);
        }

        [Fact]
        public void IsDead_WhenCurrentHealthIsPositive_ReturnsFalse()
        {
            // Arrange
            DGHealth health = new();
            health.SetCurrentHealth(10);

            // Act
            bool isDead = health.IsDead;

            // Assert
            Assert.False(isDead);
        }

        [Theory]
        [InlineData(50, 30, 20)]
        [InlineData(30, 50, 0)]
        [InlineData(0, 20, 0)]
        public void Hurt_ReducesCurrentHealth(uint initialHealth, uint damage, int expectedHealth)
        {
            // Arrange
            DGHealth health = new();
            health.SetCurrentHealth(initialHealth);

            // Act
            health.Hurt(damage);

            // Assert
            Assert.Equal(expectedHealth, health.CurrentHealth);
        }

        [Theory]
        [InlineData(50, 30, 50)]
        [InlineData(80, 30, 80)]
        [InlineData(70, 30, 100)]
        public void Heal_IncreasesCurrentHealth(uint initialHealth, uint healAmount, int expectedHealth)
        {
            // Arrange
            DGHealth health = new();
            health.SetMaximumHealth(100);
            health.SetCurrentHealth(initialHealth);

            // Act
            health.Heal(healAmount);

            // Assert
            Assert.Equal(expectedHealth, health.CurrentHealth);
        }
    }
}
