using DG.Core.Components;
using DG.Core.Exceptions.Components;

namespace DG.Tests.Components
{
    public sealed class DGComponentContainer_UnitTest
    {
        private abstract class DGFakeComponent : DGComponent
        {
            public bool Initialized { get; private set; }
            public bool Updated { get; private set; }

            public override void Initialize()
            {
                this.Initialized = true;
            }

            public override void Update()
            {
                this.Updated = true;
            }
        }

        private sealed class DGFakeComponent_01 : DGFakeComponent { }
        private sealed class DGFakeComponent_02 : DGFakeComponent { }

        [Fact]
        public void AddComponent_ThrowsInvalidTypeException_WhenTypeIsNotSubclassOfDGComponent()
        {
            DGComponentContainer container = new();

            _ = Assert.Throws<DGInvalidComponentTypeException>(() => container.AddComponent(typeof(object)));
        }

        [Fact]
        public void AddComponent_InvalidOperationException_WhenAddingADuplicateComponent()
        {
            DGComponentContainer container = new();

            _ = Assert.Throws<DGDuplicateComponentsException>(() =>
            {
                _ = container.AddComponent<DGFakeComponent_01>();
                _ = container.AddComponent<DGFakeComponent_01>();
            });
        }

        [Fact]
        public void AddComponent_AddsComponent_WhenTypeIsValid()
        {
            DGComponentContainer container = new();

            _ = container.AddComponent<DGFakeComponent_01>();

            Assert.True(container.HasComponent<DGFakeComponent_01>());
        }

        [Fact]
        public void GetComponent_ReturnsComponent_WhenComponentExists()
        {
            DGComponentContainer container = new();
            DGFakeComponent_01 component = container.AddComponent<DGFakeComponent_01>();
            DGFakeComponent_01 retrievedComponent = container.GetComponent<DGFakeComponent_01>();

            Assert.Equal(component, retrievedComponent);
        }

        [Fact]
        public void TryGetComponent_ReturnsTrue_WhenComponentExists()
        {
            DGComponentContainer container = new();
            DGFakeComponent_01 component = container.AddComponent<DGFakeComponent_01>();

            bool result = container.TryGetComponent(out DGFakeComponent_01 retrievedComponent);

            Assert.True(result);
            Assert.Equal(component, retrievedComponent);
        }

        [Fact]
        public void TryRemoveComponent_RemovesComponent_WhenComponentExists()
        {
            DGComponentContainer container = new();
            _ = container.AddComponent<DGFakeComponent_01>();

            bool result = container.TryRemoveComponent<DGFakeComponent_01>();

            Assert.True(result);
            Assert.False(container.HasComponent<DGFakeComponent_01>());
        }

        [Fact]
        public void HasComponent_ReturnsTrue_WhenComponentExists()
        {
            DGComponentContainer container = new();

            _ = Assert.Throws<DGInvalidComponentTypeException>(() => container.HasComponent(typeof(object)));
        }

        [Fact]
        public void HasComponent_ThrowsInvalidTypeException_WhenTypeIsNotSubclassOfDGComponent()
        {
            DGComponentContainer container = new();
            _ = container.AddComponent<DGFakeComponent_01>();

            Assert.True(container.HasComponent<DGFakeComponent_01>());
        }

        [Fact]
        public void RemoveAllComponents_RemovesAllComponents()
        {
            DGComponentContainer container = new();
            _ = container.AddComponent<DGFakeComponent_01>();
            _ = container.AddComponent<DGFakeComponent_02>();

            container.RemoveAllComponents();

            Assert.Equal(0, container.Count);
        }

        [Fact]
        public void Initialize_CallsInitializeMethodOfAllComponents()
        {
            DGComponentContainer container = new();
            DGFakeComponent_01 component1 = container.AddComponent<DGFakeComponent_01>();
            DGFakeComponent_02 component2 = container.AddComponent<DGFakeComponent_02>();

            container.Initialize();

            Assert.True(component1.Initialized);
            Assert.True(component2.Initialized);
        }

        [Fact]
        public void Update_CallsUpdateMethodOfAllComponents()
        {
            DGComponentContainer container = new();
            DGFakeComponent_01 component1 = container.AddComponent<DGFakeComponent_01>();
            DGFakeComponent_02 component2 = container.AddComponent<DGFakeComponent_02>();

            container.Update();

            Assert.True(component1.Updated);
            Assert.True(component2.Updated);
        }
    }
}