using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Client.Ui.Database.Creation;
using ReactiveUI;
using Shared.DomainModel;

namespace Client.Ui.Database.ItemList;

public partial class DbItemListView : ReactiveUserControl<DbItemListViewModel>
{
    private Window _dbWindow = null!;

    public static readonly DirectProperty<DbItemListView, Window> DbWindowProperty = AvaloniaProperty.RegisterDirect<DbItemListView, Window>(
        nameof(DbWindow), o => o.DbWindow, (o, v) => o.DbWindow = v);

    public Window DbWindow
    {
        get => _dbWindow;
        set => SetAndRaise(DbWindowProperty, ref _dbWindow, value);
    }
    
    public DbItemListView()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;
        this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }

    private async Task DoShowDialogAsync(InteractionContext<CreateItemViewModel, List<Item>> interaction)
    {
        var dialog = new CreateItemView
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<List<Item>>(DbWindow);
        interaction.SetOutput(result);
    }
}