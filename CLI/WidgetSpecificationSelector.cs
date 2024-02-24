namespace CLI;

internal struct WidgetSpecificationSelector
{
    private int _widgetNum;
    private int _specificationNum;

    public int WidgetNum { readonly get { return _widgetNum; } set { _widgetNum = value; } }
    public int SpecificationNum { readonly get { return _specificationNum; } set { _specificationNum = value; } }
}
