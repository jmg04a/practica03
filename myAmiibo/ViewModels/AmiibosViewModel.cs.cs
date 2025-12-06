using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using myAmiibo.Models;
using myAmiibo.Services;

namespace myAmiibo.ViewModels;

public partial class AmiibosViewModel : ObservableObject
{
    private readonly AmiiboService _amiiboService;

    // 1. LISTA MAESTRA (Respaldo oculto con todos los datos)
    private List<Amiibo> _allAmiibos = new();

    // 2. LISTA VISIBLE (Lo que se ve en pantalla)
    public ObservableCollection<Amiibo> Amiibos { get; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    private bool _isBusy;

    public bool IsNotBusy => !IsBusy;

    // 3. TEXTO DEL BUSCADOR
    [ObservableProperty]
    private string _searchText;

    // ESTE MÉTODO ES MÁGICO: Se ejecuta solo cada vez que 'SearchText' cambia
    partial void OnSearchTextChanged(string value)
    {
        FilterAmiibos(value);
    }

    public AmiibosViewModel(AmiiboService amiiboService)
    {
        _amiiboService = amiiboService;
    }

    [RelayCommand]
    async Task GetAmiibosAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var amiibos = await _amiiboService.GetAmiibosAsync();

            if (amiibos.Count > 0)
            {
                // A. Guardamos TODO en la lista maestra
                _allAmiibos.Clear();
                _allAmiibos.AddRange(amiibos);

                // B. Llenamos la lista visible también
                FilterAmiibos(SearchText);
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    // 4. LÓGICA DE FILTRADO
    private void FilterAmiibos(string filtro)
    {
        // Limpiamos la pantalla
        Amiibos.Clear();

        // Si el buscador está vacío, mostramos todo
        if (string.IsNullOrWhiteSpace(filtro))
        {
            foreach (var amiibo in _allAmiibos)
                Amiibos.Add(amiibo);
        }
        else
        {
            // Si hay texto, buscamos coincidencias (ignorando mayúsculas)
            var encontrados = _allAmiibos
                .Where(a => a.Name.Contains(filtro, StringComparison.OrdinalIgnoreCase) ||
                            a.GameSeries.Contains(filtro, StringComparison.OrdinalIgnoreCase))
                .ToList();

            foreach (var amiibo in encontrados)
                Amiibos.Add(amiibo);
        }
    }
}