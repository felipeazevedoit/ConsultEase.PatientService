

namespace MyComponentTemplate.Tests.TestConfig
{
    public static class MockComponentData
    {
        // Simula uma lista de dados que pode ser usada em testes
        public static List<string> GetMockedData()
        {
            return new List<string>
            {
                "MockedData1",
                "MockedData2",
                "MockedData3"
            };
        }

        // Simula um objeto mais complexo para testes
        public static MyComponent GetMockedComponent()
        {
            return new MyComponent
            {
                Id = 1,
                Name = "Mocked Component",
                Description = "This is a mocked component for testing purposes"
            };
        }
    }

    public class MyComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
