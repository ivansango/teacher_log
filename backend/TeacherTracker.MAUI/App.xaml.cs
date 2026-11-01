
using TeacherTracker.MAUI.Services;

namespace TeacherTracker.MAUI;

public partial class App : Application
{
    private readonly SchoolApiService _schoolApiService;

    public App(SchoolApiService schoolApiService)
    {
        InitializeComponent();
        _schoolApiService = schoolApiService;
    }

    protected override Window CreateWindow(
        IActivationState? activationState)
    {
        return new Window(
            new MainPage(_schoolApiService));
    }
}
