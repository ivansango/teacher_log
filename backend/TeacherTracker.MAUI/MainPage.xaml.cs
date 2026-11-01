
using System.Net;
using TeacherTracker.MAUI.Models;
using TeacherTracker.MAUI.Services;

namespace TeacherTracker.MAUI;

public partial class MainPage : ContentPage
{
    private readonly SchoolApiService _schoolApiService;

    public MainPage(SchoolApiService schoolApiService)
    {
        InitializeComponent();

        _schoolApiService = schoolApiService;

        Loaded += OnPageLoaded;
    }

    private async void OnPageLoaded(
        object? sender,
        EventArgs e)
    {
        Loaded -= OnPageLoaded;
        await LoadSchoolsAsync();
    }

    private async void OnRefreshClicked(
        object sender,
        EventArgs e)
    {
        await LoadSchoolsAsync();
    }

    private async Task LoadSchoolsAsync()
    {
        try
        {
            var schools = await _schoolApiService.GetSchoolsAsync();

            SchoolsContainer.Children.Clear();

            foreach (var school in schools)
            {
                var label = new Label
                {
                    Text = $"{school.Name}\n" +
                           $"{school.City}\n" +
                           $"OIB: {school.OIB}\n" +
                           $"Aktivna: {school.IsActive}",
                    Padding = new Thickness(12),
                    FontSize = 15
                };

                SchoolsContainer.Children.Add(label);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Greška",
                $"Dohvat škola nije uspio: {ex.Message}",
                "OK");
        }
    }

    private async void OnSaveClicked(
        object sender,
        EventArgs e)
    {
        var name = NameEntry.Text?.Trim() ?? "";
        var oib = OibEntry.Text?.Trim() ?? "";
        var address = AddressEntry.Text?.Trim() ?? "";
        var city = CityEntry.Text?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(address) ||
            string.IsNullOrWhiteSpace(city) ||
            oib.Length != 11 ||
            !oib.All(char.IsDigit))
        {
            await DisplayAlert(
                "Neispravni podaci",
                "Popuni sva polja i upiši OIB od 11 znamenki.",
                "OK");

            return;
        }

        var request = new CreateSchoolRequest
        {
            Name = name,
            OIB = oib,
            Address = address,
            City = city
        };

        SaveButton.IsEnabled = false;

        try
        {
            await _schoolApiService.CreateSchoolAsync(request);

            NameEntry.Text = "";
            OibEntry.Text = "";
            AddressEntry.Text = "";
            CityEntry.Text = "";

            await DisplayAlert(
                "Uspješno",
                "Nova škola je spremljena.",
                "OK");

            await LoadSchoolsAsync();
        }
        catch (HttpRequestException ex)
            when (ex.StatusCode == HttpStatusCode.Conflict)
        {
            await DisplayAlert(
                "Duplikat",
                "Škola s tim OIB-om već postoji.",
                "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Greška",
                $"Spremanje nije uspjelo: {ex.Message}",
                "OK");
        }
        finally
        {
            SaveButton.IsEnabled = true;
        }
    }
}
