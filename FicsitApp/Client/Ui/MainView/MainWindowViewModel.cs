using System.Collections.ObjectModel;
using System.Reactive;
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

namespace Client.Ui.MainView;

public class MainWindowViewModel : ViewModelBase
{
    #region View Properties ---------------------

    public ICommand OpenDatabaseWindowCommand { get; }
    public ICommand AddProjectCommand { get; }
    public ReactiveCommand<Project, Unit> OpenProjectWindowCommand { get; }
    public ReactiveCommand<Project, Unit> EditProjectCommand { get; }
    public ReactiveCommand<Project, Unit> DeleteProjectCommand { get; }
    public ObservableCollection<Project> Projects { get; } = [];

    #endregion

    #region C'tor -------------------------------

    public MainWindowViewModel()
    {
        ImageHelper.Initialize();
        DataAccess.Initialize();
        //DataAccess.ExportItemDatabase("../../../../Shared/TestData/");
        
        OpenProjectWindowCommand = ReactiveCommand.Create<Project>(OpenProjectDialog);
        OpenDatabaseWindowCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var viewModel = new DatabaseViewModel();
            return await ModalWindow.Show<MainWindow>(viewModel);
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
        var viewModel = new ProjectViewModel(project);
        await ModalWindow.Show<MainWindow>(viewModel);
    }
    
    #endregion
}