using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.View;

namespace Client.Ui.Database;

public class DatabaseWindowViewModel : ViewModelBase
{
    public static Dictionary<string, Bitmap>.ValueCollection Images => ImageHelper.Images.Values;

    public DatabaseWindowViewModel()
    {
    }
}