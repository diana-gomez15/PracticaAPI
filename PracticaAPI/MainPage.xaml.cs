namespace PracticaAPI
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

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
