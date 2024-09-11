using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace Client.Ui.Shared.Controls;

public partial class FicsitHeaderControl : ContentControl
{
    public static readonly StyledProperty<ICommand> HomeCommandProperty = 
        AvaloniaProperty.Register<FicsitHeaderControl, ICommand>(nameof(HomeCommand)); 

    public ICommand HomeCommand
    {
        get => GetValue(HomeCommandProperty);
        set => SetValue(HomeCommandProperty, value);
    }
}