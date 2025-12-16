using RawPostres.Model;
using RawPostres.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RawPostres.ViewModel
{
    public class PostreListViewModel : BaseViewModel
    {
        private readonly PostresApiService _apiService;
        private bool _isLoadingMore = false;
        private string _searchText = string.Empty;
        private int _currentPage = 1;

        public ObservableCollection<Postre> Postres { get; } = new();

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (SetProperty(ref _searchText, value))
                {
                    // Búsqueda automática (igual que RawGames)
                }
            }
        }

        public bool IsLoadingMore
        {
            get => _isLoadingMore;
            set => SetProperty(ref _isLoadingMore, value);
        }

        public ICommand LoadPostresCommand { get; }
        public ICommand LoadMorePostresCommand { get; }
        public ICommand RefreshCommand { get; }

        public ICommand PostreSelectedCommand { get; }

        public PostreListViewModel(PostresApiService apiService)
        {
            _apiService = apiService;
            Title = "Postres";

            LoadPostresCommand = new Command(async () => await LoadPostresAsync());
            LoadMorePostresCommand = new Command(async () => await LoadMorePostresAsync());
            RefreshCommand = new Command(async () => await RefreshPostresAsync());
            PostreSelectedCommand = new Command<Postre>(OnPostreSelected);

            // Cargar postres al iniciar (igual que RawGames)
            Task.Run(async () => await LoadPostresAsync());
        }

        private void OnPostreSelected(Postre postre)
        {
            if (postre == null)
                return;

            Application.Current?.MainPage?.DisplayAlert(
                "Postre Seleccionado",
                postre.Nombre,
                "OK");
        }

        private async Task LoadPostresAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                _currentPage = 1;
                Postres.Clear();

                PostresResponse? response;

                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    response = await _apiService.GetPostresAsync();
                }
                else
                {
                    response = await _apiService.SearchPostresAsync(SearchText);
                }

                if (response?.Results != null)
                {
                    foreach (var postre in response.Results)
                    {
                        Postres.Add(postre);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar los postres: {ex.Message}");
                await Application.Current!.MainPage!.DisplayAlert(
                    "Error",
                    "No se pudieron cargar los postres.",
                    "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadMorePostresAsync()
        {
            if (IsLoadingMore || IsBusy)
                return;

            try
            {
                IsLoadingMore = true;
                _currentPage++;

                PostresResponse? response;

                if (string.IsNullOrWhiteSpace(SearchText))
                {
                    response = await _apiService.GetPostresAsync();
                }
                else
                {
                    response = await _apiService.SearchPostresAsync(SearchText);
                }

                if (response?.Results != null)
                {
                    foreach (var postre in response.Results)
                    {
                        Postres.Add(postre);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar más postres: {ex.Message}");
            }
            finally
            {
                IsLoadingMore = false;
            }
        }

        private async Task RefreshPostresAsync()
        {
            await LoadPostresAsync();
        }
    }
}