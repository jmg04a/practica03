using myAmiibo.ViewModels;

namespace myAmiibo;

public partial class MainPage : ContentPage
{
    // Modificamos el constructor para recibir el ViewModel
    public MainPage(AmiibosViewModel viewModel)
    {
        InitializeComponent();

        // Conectamos los datos a la pantalla
        BindingContext = viewModel;
    }
}