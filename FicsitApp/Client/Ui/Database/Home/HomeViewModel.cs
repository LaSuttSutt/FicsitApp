using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Client.Shared.Notification;
using Client.Shared.View;
using Client.Ui.Database.Items;
using Client.Ui.Database.Machines;
using ReactiveUI;
using Shared.DataAccess;

namespace Client.Ui.Database.Home;

public class HomeViewModel : NavigationViewModel
{
    public ReactiveCommand<NavigationViewModel, Unit> OnNavButtonClicked { get; set; }
    public ICommand OnDbExportClicked { get; }
    public ICommand OnDbImportClicked { get; }
    
    public ItemsViewModel ItemsViewModel { get; } = new();
    public MachinesViewModel MachinesViewModel { get; } = new();

    public HomeViewModel()
    {
        Title = "Ficsit Database";
        OnNavButtonClicked = ReactiveCommand.Create<NavigationViewModel>(ShowSubView);

        OnDbExportClicked = ReactiveCommand.Create<UserControl>(ExportDb);
        OnDbImportClicked = ReactiveCommand.Create<UserControl>(ImportDb);
    }
    
    private void ShowSubView(NavigationViewModel viewModel)
    {
        NotificationService.RaiseOnShowNavigationView(this, viewModel);
    }

    private async void ExportDb(UserControl control)
    {
        var path = await GetFolderString(control);
        if (path != string.Empty)
            DataAccess.ExportItemDatabase(path);
    }

    private async void ImportDb(UserControl control)
    {
        var path = await GetFolderString(control);
        if (path != string.Empty)
            DataAccess.ImportItemDatabase(path);
    }

    private async Task<string> GetFolderString(UserControl control)
    {
        var topLevel = TopLevel.GetTopLevel(control);
        if (topLevel == null)
            return string.Empty;
        
        var folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = "Open Export Folder",
            AllowMultiple = false
        });

        var folder = folders[0];
        return folder.Path.AbsolutePath;
    }
}