using LocalizationSDK.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Generic;
using System.IO;
using Xunit;

public class LocalizationManagerTests
{
    [Fact]
    public void TestCreateAndUpdateResxFile()
    {
        var logger = NullLogger<LocalizationManager>.Instance;
        var localizationManager = new LocalizationManager(logger);
        var testResxFilePath = "test_resources_unit_test.resx";

        // Clean up any existing file
        if (File.Exists(testResxFilePath))
        {
            File.Delete(testResxFilePath);
        }

        // Create new RESX file
        localizationManager.CreateResxFile(testResxFilePath);
        Assert.True(File.Exists(testResxFilePath));

        // Update the RESX file
        var newValues = new Dictionary<string, string>
        {
            { "Hello", "Hello Unit Test" },
            { "Goodbye", "Goodbye Unit Test" }
        };
        localizationManager.UpdateResxFile(testResxFilePath, newValues);

        // Read the values back
        var resxValues = localizationManager.ReadResxFile(testResxFilePath);
        Assert.Equal("Hello Unit Test", resxValues["Hello"]);
        Assert.Equal("Goodbye Unit Test", resxValues["Goodbye"]);

        // Clean up
        File.Delete(testResxFilePath);
    }
}
