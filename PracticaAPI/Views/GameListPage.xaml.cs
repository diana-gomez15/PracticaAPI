using RawPostres.ViewModel;

namespace RawPostres.Views;

public partial class PostreListPage : ContentPage
{
    public PostreListPage(PostreListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}