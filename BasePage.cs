using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PraveenMatoria;

public class BasePage: ComponentBase
{
    [Inject]
    public required IJSRuntime JS { get; set; }

    protected override void OnInitialized()
    {
        var DotNetHelper = DotNetObjectReference.Create(this);
        JS.InvokeVoidAsync("SetDotnetReference", DotNetHelper);
    }
}
