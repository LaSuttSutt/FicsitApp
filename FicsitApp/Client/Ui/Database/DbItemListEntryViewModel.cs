using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.View;

namespace Client.Ui.Database;

public class DbItemListEntryViewModel : ViewModelBase
{
    public Bitmap Image { get; set; } = ImageHelper.Images["avares://Client.Assets/ImageDb/A01_default_64.png"];
    public string Name { get; set; } = "Test-Item";
}