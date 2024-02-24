namespace CLI;

/// <summary>
/// Represents a selector for widget and specification numbers.
/// </summary>
internal struct WidgetSpecificationSelector
{
    private int _widgetNum;
    private int _specificationNum;

    /// <summary>
    /// Gets or sets the widget number.
    /// </summary>
    public int WidgetNum { readonly get { return _widgetNum; } set { _widgetNum = value; } }

    /// <summary>
    /// Gets or sets the specification number.
    /// </summary>
    public int SpecificationNum { readonly get { return _specificationNum; } set { _specificationNum = value; } }
}
