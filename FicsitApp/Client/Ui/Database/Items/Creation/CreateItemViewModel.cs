using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Client.Helper;
using Client.Shared.View;
using Client.Ui.Shared.Dialogs;
using Shared.DomainModel;

namespace Client.Ui.Database.Items.Creation;

public class CreateItemViewModel : ViewModelBase, ISaveCancelViewModel
{
    public string Title { get; } = "Ficsit App - Item";
    public Size Size { get; } = new Size(417, 337);
    
    public Item Item { get; }
    public List<FicsitImage> ItemImages => ImageHelper.ItemImages;
    public FicsitImage? SelectedImage { get; set; }
    
    
    public CreateItemViewModel(Item item)
    {
        Item = item;
        SelectedImage = ItemImages.FirstOrDefault
            (i => i.ImagePath == item.ImageName);
    }

    public bool DoSaving()
    {
        if (SelectedImage != null)
            Item.ImageName = SelectedImage.ImagePath;
        return true;
    }
}