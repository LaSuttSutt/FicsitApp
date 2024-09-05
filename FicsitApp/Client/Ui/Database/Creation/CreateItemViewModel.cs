using System.Collections;
using System.Collections.Generic;
using System.Reactive;
using System.Windows.Input;
using Client.Shared.View;
using ReactiveUI;
using Shared.DomainModel;

namespace Client.Ui.Database.Creation;

public class CreateItemViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, List<Item>> SaveCommand { get; }

    public CreateItemViewModel()
    {
        SaveCommand = ReactiveCommand.Create(() => new List<Item>([new(), new()]));
    }
}