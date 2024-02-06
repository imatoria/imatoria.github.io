using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace PraveenMatoria;

public class BasePage: ComponentBase
{
    [Inject]
    public required IJSRuntime JS { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var DotNetHelper = DotNetObjectReference.Create(this);
        await JS.InvokeVoidAsync("SetDotnetReference", DotNetHelper);
    }
}
