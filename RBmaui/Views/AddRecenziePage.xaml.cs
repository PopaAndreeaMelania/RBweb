using RBmaui.Data;

namespace RBmaui.Views;

public partial class AddRecenziePage : ContentPage
{
    private readonly int _comandaId;
    private readonly RestService _restService;

    public AddRecenziePage(int comandaId)
    {
        InitializeComponent();
        _comandaId = comandaId;
        _restService = new RestService();
    }

    private async void OnTrimiteClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ComentariuEditor.Text))
        {
            await DisplayAlert("Eroare", "Comentariul este obligatoriu.", "OK");
            return;
        }

        var success = await _restService.AdaugaRecenzieAsync(
            _comandaId,
            (int)RatingStepper.Value,
            ComentariuEditor.Text
        );

        if (success)
        {
            await DisplayAlert("Succes", "Recenzie trimisă!", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Eroare", "Nu s-a putut trimite recenzia.", "OK");
        }
    }
}