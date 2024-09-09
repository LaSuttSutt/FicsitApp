using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Collections;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Ui.Shared;
using ReactiveUI;

namespace Client.Ui.Database.Creation.Pages;

public class ImagesViewModel : WizardViewModel
{
    public ObservableCollection<FicsitImage> Images { get; } = [];

    public ImagesViewModel()
    {
        foreach (var item in ImageHelper.Images)
        {
            Images.Add(new FicsitImage(item.Key, item.Value));
        }
    }
}