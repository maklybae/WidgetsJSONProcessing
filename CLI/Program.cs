using System.Text.Json;
using WidgetsJSON;

namespace CLI
{
    internal class Program
    {
        static void Main()
        {
            string fileName = "D:\\VisualStudio\\WidgetsJSONProcessing\\test.json";
            string jsonString = File.ReadAllText(fileName);
            List<Widget>? widgets = JsonSerializer.Deserialize<List<Widget>>(jsonString);

            foreach (var item in widgets)
            {
                Console.WriteLine(item.Name);
            }
            var options = new JsonSerializerOptions { WriteIndented = true };
            jsonString = JsonSerializer.Serialize(widgets, options);

            Console.WriteLine(jsonString);
        }
    }
}