namespace CLI;

internal static class DataConverter
{
    internal static string[][] ConvertToJaggedArray(
        List<(string widgetId, string name, int quantity, double price, bool isAvailable, DateTime manufactureDate,
        List<(string specName, double specPrice, bool isCustom)> specifications)> items)
    {
        string[][] res = new string[items.Count][];
        for (int i = 0; i < items.Count; i++)
        {
            res[i] = new string[]
            {
                items[i].widgetId,
                items[i].name,
                items[i].quantity.ToString(),
                items[i].price.ToString(),
                items[i].isAvailable.ToString(),
                items[i].manufactureDate.ToString(),
                string.Join(";", Array.ConvertAll(items[i].specifications.ToArray(), elem => $"{elem.specName}({elem.specPrice}-{elem.isCustom})"))
            };
        }
        return res;
    }
}
