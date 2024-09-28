using DG.Core.Components.Common;

namespace DG.Tests.Components.Common
{
    public sealed class DGHealthComponent_UnitTests
    {
        [Fact]
        public void IsDead_WhenCurrentHealthIsZero_ReturnsTrue()
        {
            // Arrange
            DGHealthComponent health = new();
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
            DGHealthComponent health = new();
            health.SetCurrentHealth(10);

            // Act
            bool isDead = health.IsDead;

            // Assert
            Assert.False(isDead);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        public void SetCurrentHealth_DefiningAValueForCurrentHealth(int healthDefined)
        {
            DGHealthComponent health = new();
            health.SetCurrentHealth(healthDefined);

            Assert.Equal(healthDefined, health.CurrentHealth);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        public void SetMaximumHealth_DefiningAValueForMaximumHealth(int healthDefined)
        {
            DGHealthComponent health = new();
            health.SetMaximumHealth(healthDefined);

            Assert.Equal(healthDefined, health.MaximumHealth);
        }

        [Theory]
        [InlineData(50, 30, 20)]
        [InlineData(30, 50, 0)]
        [InlineData(0, 20, 0)]
        public void Hurt_ReducesCurrentHealth(int initialHealth, int damage, int expectedHealth)
        {
            // Arrange
            DGHealthComponent health = new();
            health.SetCurrentHealth(initialHealth);

            // Act
            health.Hurt(damage);

            // Assert
            Assert.Equal(expectedHealth, health.CurrentHealth);
        }

        [Theory]
        [InlineData(50, 50, 30, 50)]
        [InlineData(80, 80, 30, 80)]
        [InlineData(70, 100, 30, 100)]
        public void Heal_IncreasesCurrentHealth(int initialHealth, int initialMaximumHealth, int healAmount, int expectedHealth)
        {
            // Arrange
            DGHealthComponent health = new();
            health.SetMaximumHealth(initialMaximumHealth);
            health.SetCurrentHealth(initialHealth);

            // Act
            health.Heal(healAmount);

            // Assert
            Assert.Equal(expectedHealth, health.CurrentHealth);
        }
    }
}
