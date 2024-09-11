using System.Collections.ObjectModel;
using Client.Helper;
using Client.Ui.Shared;
using ReactiveUI;

namespace Client.Ui.Database.Items.Creation.Pages;

public class ImagesViewModel : WizardViewModel
{
    public ObservableCollection<FicsitImage> Images { get; } = [];
    private FicsitImage? _selectedImage;

    public FicsitImage? SelectedImage
    {
        get => _selectedImage;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedImage, value);
            CanGoForward = value != null;
            RaiseOnStateChanged();
        }
    }

    public ImagesViewModel()
    {
        foreach (var item in ImageHelper.Images)
        {
            Images.Add(new FicsitImage(item.Key, item.Value));
        }
    }
}