using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Client.Helper;
using Client.Shared.View;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DomainModel;

namespace Client.Ui.Database.Items.Creation;

public class CreateItemViewModel : ViewModelBase
{
    public Item Item { get; }
    public List<FicsitImage> ItemImages => ImageHelper.ItemImages;
    public FicsitImage? SelectedImage { get; set; }
    
    public ReactiveCommand<Unit, ShowDialogResult> SaveItemCommand { get; }
    public ReactiveCommand<Unit, ShowDialogResult> CancelCommand { get; } = 
        ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Cancel));
    
    public CreateItemViewModel(Item item)
    {
        Item = item;
        SelectedImage = ItemImages.FirstOrDefault
            (i => i.ImagePath == item.ImageName);

        SaveItemCommand = ReactiveCommand.Create(() =>
        {
            if (SelectedImage != null)
                item.ImageName = SelectedImage.ImagePath;

            return new ShowDialogResult(DialogResult.Ok);
        });
    }
}