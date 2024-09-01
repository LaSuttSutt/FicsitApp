using System.Collections.Generic;
using Client.Shared.DomainModel;
using Client.Shared.View;
using ReactiveUI;
using Shared.DomainModel;

namespace Client.Ui.Database;

public class ItemDetailUserControlViewModel : ViewModelBase
{
    private ItemViewModel _item = new();

    public ItemViewModel Item
    {
        get => _item;
        set => this.RaiseAndSetIfChanged(ref _item, value);
    }
    
    // ReSharper disable once CollectionNeverQueried.Global
    public static readonly List<Recipe> TestRecipes = [new Recipe(), new Recipe()];
}