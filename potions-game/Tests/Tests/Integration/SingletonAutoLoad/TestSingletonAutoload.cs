using System;
using GodotXUnitApi;
using PotionsGame.Core.Managers;
using PotionsGame.Core.Utils;
using Xunit;

namespace Tests.Integration.SingletonAutoLoad
{
    public class TestSingletonFixture: IDisposable
    {
        private static MockSingleton mockObject;
        public static MockSingleton MockObject => mockObject;
        public class MockSingleton : SingletonAutoload<MockSingleton>
        {
            public string Thing { get; set; }
            public int Add(int a, int b) => a + b;
        }
            
        public TestSingletonFixture()
        {
            mockObject = new MockSingleton();
            var root = GDU.CurrentScene.GetTree().Root;
            root.AddChild(mockObject);
        }
            
        public void Dispose()
        {
            mockObject.Dispose();
        }
    }
    
    public class TestSingletonAutoload: IClassFixture<TestSingletonFixture>
    {
        [GodotFact]
        public void SingletonAutoLoad_Instance_Should_Not_Be_Null()
        {
            Assert.NotNull(TestSingletonFixture.MockSingleton.Instance);
        }
        
        [GodotFact]
        public void SingletonAutoLoad_Properties_Should_Be_Accessible_From_Instance()
        {
            TestSingletonFixture.MockSingleton.Instance.Thing = "Hello World";
            Assert.Equal("Hello World", TestSingletonFixture.MockSingleton.Instance.Thing);
        }

        [GodotFact]
        public void SingletonAutoLoad_Methods_Should_Be_Accessible_From_Instance()
        {
            var result = TestSingletonFixture.MockObject.Add(1, 2);
            Assert.Equal(3, result);
        }
    }
}