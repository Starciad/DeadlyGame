namespace DeadlyGame.Tests.Components.Common
{
    public sealed class DGInventoryComponent_UnitTests
    {
        private class FakeItem_01 : DGItem { }
        private class FakeItem_02 : DGItem { }

        #region ADD
        [Fact]
        public void TryAddItem_SuccessfullyAddsItemToInventory()
        {
            // Arrange
            DGInventoryComponent inventory = new();
            inventory.ModifyNumberOfSlots(1);
            FakeItem_01 item = new();

            // Act
            bool added = inventory.TryAddItem(item, 1);

            // Assert
            Assert.True(added);
            _ = Assert.Single(inventory.Slots);
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
            _ = inventory.TryAddItem(item1, 1);
            bool added = inventory.TryAddItem(item2, 1);

            // Assert
            Assert.False(added);
            _ = Assert.Single(inventory.Slots);
            Assert.Equal(item1.GetType(), inventory.Slots[0].ItemType);
            Assert.Equal(1, inventory.Slots[0].Amount);
        }

        [Fact]
        public void TryAddItem_AddsAndMergesTheQuantityOfAnItemThatAlreadyExistsInTheInventory()
        {
            // Arrange
            DGInventoryComponent inventory = new();
            inventory.ModifyNumberOfSlots(1);

            FakeItem_01 item_01 = new();
            FakeItem_01 item_02 = new();

            bool item1Added = inventory.TryAddItem(item_01, 1);
            bool item2Added = inventory.TryAddItem(item_02, 1);

            // Assert
            Assert.True(item1Added);
            Assert.True(item2Added);
            _ = Assert.Single(inventory.Slots);
            Assert.Equal(item_01.GetType(), inventory.Slots[0].ItemType);
            Assert.Equal(2, inventory.Slots[0].Amount);
        }

        [Fact]
        public void TryAddItem_AddANewSlotIfTheItemStackHasExceededTheLimit()
        {
            // Arrange
            DGInventoryComponent inventory = new();
            inventory.ModifyNumberOfSlots(2);

            FakeItem_01 item = new();

            bool item1Added = inventory.TryAddItem(item, DGInventoryConstants.MAXIMUM_ITEM_CAPACITY);
            bool item2Added = inventory.TryAddItem(item, 1);

            // Assert
            Assert.True(item1Added);
            Assert.True(item2Added);
            Assert.Equal(item.GetType(), inventory.Slots[0].ItemType);
            Assert.Equal(item.GetType(), inventory.Slots[1].ItemType);
            Assert.Equal(DGInventoryConstants.MAXIMUM_ITEM_CAPACITY, inventory.Slots[0].Amount);
            Assert.Equal(1, inventory.Slots[1].Amount);
        }
        #endregion

        #region REMOVE
        [Fact]
        public void TryRemoveItem_SuccessfullyRemovesItemFromInventory()
        {
            // Arrange
            DGInventoryComponent inventory = new();
            inventory.ModifyNumberOfSlots(1);
            FakeItem_01 item = new();
            _ = inventory.TryAddItem(item, 2);

            // Act
            bool removed = inventory.TryRemoveItem(item, 1);

            // Assert
            Assert.True(removed);
            _ = Assert.Single(inventory.Slots);
            Assert.Equal(item.GetType(), inventory.Slots[0].ItemType);
            Assert.Equal(1, inventory.Slots[0].Amount);
        }

        [Fact]
        public void TryRemoveItem_RemoveItemSlotFromInventoryIfItIsEmpty()
        {
            // Arrange
            DGInventoryComponent inventory = new();
            inventory.ModifyNumberOfSlots(1);

            FakeItem_01 item = new();
            _ = inventory.TryAddItem(item, 1);

            // Act
            bool removed = inventory.TryRemoveItem(item, 1);

            // Assert
            Assert.True(removed);
            Assert.Empty(inventory.Slots);
        }
        #endregion

        #region GET
        [Fact]
        public void TryGetItem_GetAnItemThatHasBeenAddedToTheInventory()
        {
            // Arrange
            DGInventoryComponent inventory = new();
            inventory.ModifyNumberOfSlots(1);
            FakeItem_01 item = new();

            // Act
            bool added = inventory.TryAddItem(item, 1);
            bool itemObtained = inventory.GetItem<FakeItem_01>() != null;

            // Assert
            Assert.True(added);
            Assert.True(itemObtained);
            _ = Assert.Single(inventory.Slots);
            Assert.Equal(item.GetType(), inventory.Slots[0].ItemType);
            Assert.Equal(1, inventory.Slots[0].Amount);
        }
        #endregion

        #region HAS ITEM
        [Fact]
        public void HasItem_AddItemToInventoryAndCheckIfItExists()
        {
            // Arrange
            DGInventoryComponent inventory = new();
            inventory.ModifyNumberOfSlots(1);
            FakeItem_01 item = new();

            // Act
            bool added = inventory.TryAddItem(item, 1);
            bool haveTheItem = inventory.HasItem(item);

            // Assert
            Assert.True(added);
            Assert.True(haveTheItem);
            _ = Assert.Single(inventory.Slots);
            Assert.Equal(item.GetType(), inventory.Slots[0].ItemType);
            Assert.Equal(1, inventory.Slots[0].Amount);
        }

        [Fact]
        public void HasItem_CheckTheExistenceOfAnItemThatIsNotInTheInventory()
        {
            // Arrange
            DGInventoryComponent inventory = new();

            // Act
            bool notHaveTheItem = !inventory.HasItem<FakeItem_01>();

            // Assert
            Assert.True(notHaveTheItem);
            Assert.Empty(inventory.Slots);
        }
        #endregion

        #region UTILITIES
        [Fact]
        public void ClearInventory_ClearAllItemsPresentInTheInventory()
        {
            // Arrange
            DGInventoryComponent inventory = new();
            inventory.ModifyNumberOfSlots(2);
            FakeItem_01 item_01 = new();
            FakeItem_02 item_02 = new();

            // Act
            bool addedItem01 = inventory.TryAddItem(item_01, 1);
            bool addedItem02 = inventory.TryAddItem(item_02, 1);

            inventory.ClearInventory();

            // Assert
            Assert.True(addedItem01);
            Assert.True(addedItem02);
            Assert.Empty(inventory.Slots);
        }
        #endregion
    }
}