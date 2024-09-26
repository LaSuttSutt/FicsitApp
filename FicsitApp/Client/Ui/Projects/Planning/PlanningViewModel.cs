using System.Collections.Generic;
using System.Linq;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.DomainModel;
using Client.Shared.View;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Projects.Planning;

public class PlanningViewModel : ViewModelBase
{
    public List<ItemListModel> Items { get; }
    public List<Recipe> Recipes { get; set; } = [];
    public decimal MachineCount { get; set; } = 1;
    public decimal Workload { get; set; } = 100;
    public decimal ItemAmount { get; set; }
    public Bitmap MachineImage { get; set; } = ImageHelper.DefaultMachine;
    
    private ItemListModel? _selectedItem;
    public ItemListModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            SelectedItemChanged();
        }
    }

    private Recipe? _selectedRecipe;
    public Recipe? SelectedRecipe
    {
        get => _selectedRecipe;
        set
        {
            _selectedRecipe = value;
            SelectedRecipeChanged();
        }
    }
   
    public PlanningViewModel()
    {
        Items = DataAccess.GetEntities<Item>(i => !i.IsResource).ToItemList();
        SelectedItem = Items.First();
    }

    private void SelectedItemChanged()
    {
        if (SelectedItem == null) return;
        
        Recipes = DataAccess.GetEntities<Recipe>(r => r.ItemId == SelectedItem.Item.Id);
        this.RaisePropertyChanged(nameof(Recipes));
        
        SelectedRecipe = Recipes.First();
        this.RaisePropertyChanged(nameof(SelectedRecipe));
    }

    private void SelectedRecipeChanged()
    {
        if (SelectedRecipe == null) return;
        
        var machine = DataAccess.GetEntity<Machine>(SelectedRecipe.MachineId);
        MachineImage = machine?.Image() ?? ImageHelper.DefaultItem;
        MachineCount = 1;
        Workload = 100;
        ItemAmount = SelectedRecipe.Amount;
        
        this.RaisePropertyChanged(nameof(MachineImage));
        this.RaisePropertyChanged(nameof(MachineCount));
        this.RaisePropertyChanged(nameof(Workload));
        this.RaisePropertyChanged(nameof(ItemAmount));
    }
}