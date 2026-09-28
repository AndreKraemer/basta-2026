using CommunityToolkit.Mvvm.Input;
using MauiAppDemoLive.Models;

namespace MauiAppDemoLive.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}