using PokemonGo_UWP.Utils;
using System;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace PokemonGo_UWP.Views
{
    public sealed partial class PokedexPage : Page
    {
        public PokedexPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }
        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.CompareTo(nameof(ViewModel.SelectedPokedexEntry)) == 0)
                scrollPokedexEntry.ChangeView(0, 0, scrollPokedexEntry.ZoomFactor);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }
        private WidthConverter widthCalc = new WidthConverter();
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var newWidth = (int)widthCalc.Convert(1, null, "0,28", null);
            pokeindex.Width = newWidth;
        }
        private int x1, x2;
        private void Grid_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            x1 = (int)e.Position.X;
        }

        private void Grid_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            x2 = (int)e.Position.X;
            int diff = x2 - x1;
            int AbsDiff = Math.Abs(x2 - x1);
            if(diff>0 && AbsDiff > 75)
                ViewModel.PrevPokemon.Execute();
            else if(diff<0 && AbsDiff > 75)
                ViewModel.NextPokemon.Execute();
        }
    }
}
