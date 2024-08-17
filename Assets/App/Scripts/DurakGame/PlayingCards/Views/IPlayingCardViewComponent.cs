using App.Scripts.DurakGame.PlayingCards.Views.Configs;
using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;

namespace App.Scripts.DurakGame.PlayingCards.Views
{
    public interface IPlayingCardViewComponent
    {
        bool IsActive(IPlayingCardViewModel viewModel);
        void Enable();
        void UpdateView(IPlayingCardViewModel viewModel, PlayingCardViewConfig viewConfig);
        void Disable();
        void Release();
    }
}