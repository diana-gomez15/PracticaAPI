namespace PracticaAPI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGoToPostresClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//PostreListPage");
        }
    }
}
