﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Client.Shared.DomainModel;
using Client.Shared.View;
using Client.Ui.Shared.Binding;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Projects.Planning;

public class ItemEntryViewModel : ViewModelBase
{
    #region Declarations

    private ItemListModel? _selectedItem;
    private RecipeListModel? _selectedRecipe;
    private decimal _machineCount;
    private decimal _workload;

    public static NullBlockerConverter NumericUpDownConverter { get; } = new();
    public event EventHandler? SelectedItemChanged;
    public event EventHandler<ItemEntryViewModel>? SelectedRecipeChanged;
    public event EventHandler<ItemEntryViewModel>? RecalculationNeeded;
    public event EventHandler<ItemEntryViewModel>? DeleteMainItemClicked;
    public ReactiveCommand<ItemEntryViewModel, Unit> OnDeleteMainItem { get; }

    public List<ItemListModel> Items { get; }
    public List<RecipeListModel> Recipes { get; set; }
    public decimal Amount { get; set; }
    public decimal Requirement { get; set; }
    public decimal Difference { get; set; }
    public bool IsPrimaryItem { get; }
    public bool IsNeededItem { get; }
    public bool IsMiner { get; }
    public bool Overproduction => Difference > 0;
    public bool Underproduction => Difference < 0;

    public ItemListModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            if (value == null) return;
            this.RaiseAndSetIfChanged(ref _selectedItem, value);
            OnSelectedItemChanged();
        }
    }

    public RecipeListModel? SelectedRecipe
    {
        get => _selectedRecipe;
        set
        {
            if (value == null) return;
            this.RaiseAndSetIfChanged(ref _selectedRecipe, value);
            OnSelectedRecipeChanged();
        }
    }

    public decimal MachineCount
    {
        get => _machineCount;
        set
        {
            this.RaiseAndSetIfChanged(ref _machineCount, value);
            OnCalculateAmount();
            RecalculationNeeded?.Invoke(this, this);
        }
    }

    public decimal Workload
    {
        get => _workload;
        set
        {
            this.RaiseAndSetIfChanged(ref _workload, value);
            OnCalculateAmount();
            RecalculationNeeded?.Invoke(this, this);
        }
    }

    #endregion

    #region C'tor

    public ItemEntryViewModel(bool isMiner = false)
    {
        if (isMiner)
        {
            Items = [];
            IsMiner = true;
            OnDeleteMainItem = ReactiveCommand.Create<ItemEntryViewModel>(RaiseDeleteMainItemClicked);
            Recipes = [];
            return;
        }
        
        Items = DataAccess.GetEntities<Item>(i => !i.IsResource).OrderBy(i => i.ShortNameOrName).ToList().ToItemList();
        SelectedItem = Items.First();
        IsPrimaryItem = true;
        OnDeleteMainItem = ReactiveCommand.Create<ItemEntryViewModel>(RaiseDeleteMainItemClicked);

        Recipes = DataAccess.GetEntities<Recipe>(r => r.ItemId == SelectedItem.Item.Id).OrderByDescending(r => r.Name)
            .ToList().ToRecipeList();
        SelectedRecipe = Recipes[0];
    }

    public ItemEntryViewModel(Item item)
    {
        Items = [];
        SelectedItem = item.ToItemListModel();
        IsNeededItem = true;
        
        OnDeleteMainItem = ReactiveCommand.Create<ItemEntryViewModel>(RaiseDeleteMainItemClicked);

        Recipes = DataAccess.GetEntities<Recipe>(r => r.ItemId == item.Id).OrderByDescending(r => r.Name).ToList()
            .ToRecipeList();
        SelectedRecipe = Recipes[0];
    }

    #endregion

    #region Public Methods

    public void SetRequirement(decimal requirement)
    {
        Requirement = requirement;
        Difference = Math.Round(Amount - Requirement, 2, MidpointRounding.AwayFromZero);
        this.RaisePropertyChanged(nameof(Overproduction));
        this.RaisePropertyChanged(nameof(Underproduction));
        this.RaisePropertyChanged(nameof(Difference));
        this.RaisePropertyChanged(nameof(Requirement));
    }

    #endregion

    #region Private Methods

    private void OnSelectedItemChanged()
    {
        if (SelectedItem == null) return;

        Recipes = DataAccess.GetEntities<Recipe>(r => r.ItemId == SelectedItem.Item.Id).ToRecipeList();
        this.RaisePropertyChanged(nameof(Recipes));
        SelectedRecipe = Recipes[0];

        SelectedItemChanged?.Invoke(this, EventArgs.Empty);
    }

    private void OnSelectedRecipeChanged()
    {
        if (SelectedRecipe == null) return;

        _machineCount = 1;
        _workload = 100;
        Amount = SelectedRecipe.Recipe.Amount;
        Difference = Math.Round(Amount - Requirement, 2, MidpointRounding.AwayFromZero);

        this.RaisePropertyChanged(nameof(MachineCount));
        this.RaisePropertyChanged(nameof(Workload));
        this.RaisePropertyChanged(nameof(Amount));
        this.RaisePropertyChanged(nameof(Difference));

        SelectedRecipeChanged?.Invoke(this, this);
    }

    private void OnCalculateAmount()
    {
        if (SelectedRecipe == null) return;

        Amount = Math.Round(MachineCount * SelectedRecipe.Recipe.Amount * Workload / 100.0m, 2,
            MidpointRounding.AwayFromZero);
        this.RaisePropertyChanged(nameof(Amount));
    }

    private void RaiseDeleteMainItemClicked(ItemEntryViewModel item)
    {
        DeleteMainItemClicked?.Invoke(this, item);
    }

    #endregion
}