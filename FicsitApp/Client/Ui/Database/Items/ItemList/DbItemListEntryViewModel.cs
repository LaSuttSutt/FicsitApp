using System;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.DomainModel;
using Client.Shared.View;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Database.Items.ItemList;

public class DbItemListEntryViewModel : ViewModelBase
{
    public Guid ItemId { get; }
    public string Name { get; set; } = "<not found>";
    public Bitmap Image { get; set; } = ImageHelper.DefaultImage;

    public DbItemListEntryViewModel(Guid itemId)
    {
        ItemId = itemId;

        var item = DataAccess.GetEntity<Item>(itemId);
        if (item == null) return;
        
        Name = item.HasShortName ? item.ShortName : item.Name;
        Image = item.Image();
    }
    
    public void Reload()
    {
        var item = DataAccess.GetEntity<Item>(ItemId);
        if(item == null) return;

        Name = item.HasShortName ? item.ShortName : item.Name;
        Image = item.Image();

        this.RaisePropertyChanged(nameof(Name));
        this.RaisePropertyChanged(nameof(Image));
    }
}