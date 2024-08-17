using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;

namespace App.Scripts.DurakGame.PlayingCards.Views
{
    public interface IPlayingCardView
    {
        void UpdateView(IPlayingCardViewModel viewModel);
        void Release();
    }
}