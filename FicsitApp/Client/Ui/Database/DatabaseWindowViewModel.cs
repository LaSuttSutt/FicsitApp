using System;
using System.Windows.Input;
using Client.Shared.View;
using Client.Ui.Database.Items;
using ReactiveUI;

namespace Client.Ui.Database;

public class DatabaseWindowViewModel : ViewModelBase
{
    public ItemsViewModel ItemsViewModel { get; } = new();
    
    public ICommand HomeCommand { get; set; }

    public DatabaseWindowViewModel()
    {
        HomeCommand = ReactiveCommand.Create(() =>
        {
            Console.WriteLine("Home clicked ;)");
        });
    }
}