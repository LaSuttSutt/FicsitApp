using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using Client.Helper;
using Client.Shared.DomainModel;
using Client.Shared.View;
using Client.Ui.Database;
using Client.Ui.Projects;
using Client.Ui.Projects.Creation;
using Client.Ui.Shared;
using Client.Ui.Shared.Dialogs;
using DynamicData;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;
using Shared.TestData;

namespace Client.Ui.MainView;

public class MainWindowViewModel : ViewModelBase
{
    #region View Properties ---------------------

    public ICommand OpenDatabaseWindowCommand { get; }
    public ICommand AddProjectCommand { get; }
    public ReactiveCommand<Project, Unit> OpenProjectWindowCommand { get; }
    public ReactiveCommand<Project, Unit> EditProjectCommand { get; }
    public ReactiveCommand<Project, Unit> DeleteProjectCommand { get; }
    public Interaction<DatabaseWindowViewModel, bool> ShowDatabaseDialog { get; }
    public Interaction<ProjectWindowViewModel, bool> ShowProjectDialog { get; }
    public ObservableCollection<Project> Projects { get; } = [];

    #endregion

    #region C'tor -------------------------------

    public MainWindowViewModel()
    {
        ImageHelper.Initialize();
        ItemDatabase.Initialize();
        DataAccess.Initialize();
        
        LoadProjects();
        
        ShowProjectDialog = new Interaction<ProjectWindowViewModel, bool>();
        OpenProjectWindowCommand = ReactiveCommand.Create<Project>(OpenProjectDialog);
        
        ShowDatabaseDialog = new Interaction<DatabaseWindowViewModel, bool>();
        OpenDatabaseWindowCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var viewModel = new DatabaseWindowViewModel();
            return await ShowDatabaseDialog.Handle(viewModel);
        });
        
        AddProjectCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var project = new Project();
            var viewModel = new CreateProjectViewModel(project);
            var result = await SaveCancelDialog.Show<MainWindow>(viewModel);

            if (result == DialogResult.Ok)
            {
                DataAccess.AddEntity(project);
                Projects.Add(project);
            }
        });

        EditProjectCommand = ReactiveCommand.Create<Project>(EditProject);
        DeleteProjectCommand = ReactiveCommand.Create<Project>(DeleteProject);
    }

    #endregion
    
    #region Methods

    private void LoadProjects()
    {
        Projects.AddRange(DataAccess.GetEntities<Project>());
    }
    
    private async void EditProject(Project project)
    {
        var projectClone = project.Clone();
        var viewModel = new CreateProjectViewModel(projectClone);
        var result = await SaveCancelDialog.Show<MainWindow>(viewModel);

        if (result != DialogResult.Ok) return;

        project.Update(projectClone);
        DataAccess.UpdateEntity(project);
        Projects.Replace(project, project);
    }
    
    private void DeleteProject(Project project)
    {
        Projects.Remove(project);
        DataAccess.DeleteEntity(project);
    }

    private async void OpenProjectDialog(Project project)
    {
        var viewModel = new ProjectWindowViewModel(project);
        await ShowProjectDialog.Handle(viewModel);
    }
    
    #endregion
}