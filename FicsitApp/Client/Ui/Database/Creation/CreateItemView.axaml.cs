using System;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Client.Ui.Database.Creation;

public partial class CreateItemView : ReactiveWindow<CreateItemViewModel>
{
    public CreateItemView()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;

        this.WhenActivated(action => action(ViewModel!.SaveCommand.Subscribe(Close)));
    }
}