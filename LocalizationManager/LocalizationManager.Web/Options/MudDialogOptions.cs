using MudBlazor;

namespace LocalizationManager.Web.Options;

public static class MudDialogOptions
{
    public static readonly DialogOptions AuthDialogOptions = new()
    {
        CloseOnEscapeKey = true,
        MaxWidth = MaxWidth.ExtraSmall,
        FullWidth = true,
        CloseButton = true,
    };

    public static readonly DialogOptions LocalizationDialogOptions = new()
    {
        CloseOnEscapeKey = true,
        MaxWidth = MaxWidth.Medium,
        FullWidth = true,
        CloseButton = true,
    };
}
