using DG.Core.Components.Common;
using DG.Core.Items;

namespace DG.Tests.Components.Common
{
    public sealed class DGInventoryComponent_UnitTests
    {
        private class FakeItem_01 : DGItem
        {
            public override void Build() { }
        }

        private class FakeItem_02 : DGItem
        {
            public override void Build() { }
        }

        [Fact]
        public void TryAddItem_SuccessfullyAddsItemToInventory()
        {
            // Arrange
            DGInventoryComponent inventory = new();
            FakeItem_01 item = new();

            // Act
            bool added = inventory.TryAddItem(item, 1);

            // Assert
            Assert.True(added);
            Assert.Single(inventory.Slots);
            Assert.Equal(item.GetType(), inventory.Slots[0].ItemType);
            Assert.Equal(1, inventory.Slots[0].Amount);
        }

        [Fact]
        public void TryAddItem_FailsToAddItemWhenInventoryIsFull()
        {
            // Arrange
            DGInventoryComponent inventory = new();
            inventory.ModifyNumberOfSlots(1);
            FakeItem_01 item1 = new();
            FakeItem_02 item2 = new();

            // Act
            inventory.TryAddItem(item1, 1);
            bool added = inventory.TryAddItem(item2, 1);

            // Assert
            Assert.False(added);
            Assert.Single(inventory.Slots);
            Assert.Equal(item1.GetType(), inventory.Slots[0].ItemType);
            Assert.Equal(1, inventory.Slots[0].Amount);
        }

        [Fact]
        public void TryRemoveItem_SuccessfullyRemovesItemFromInventory()
        {
            // Arrange
            DGInventoryComponent inventory = new();
            FakeItem_01 item = new();
            inventory.TryAddItem(item, 2);

            // Act
            bool removed = inventory.TryRemoveItem(item, 1);

            // Assert
            Assert.True(removed);
            Assert.Single(inventory.Slots);
            Assert.Equal(item.GetType(), inventory.Slots[0].ItemType);
            Assert.Equal(1, inventory.Slots[0].Amount);
        }
    }
}
