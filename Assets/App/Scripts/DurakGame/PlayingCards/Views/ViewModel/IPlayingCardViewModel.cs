using App.Scripts.Cards.Components;

namespace App.Scripts.DurakGame.PlayingCards.Views.ViewModel
{
    public interface IPlayingCardViewModel
    {
        string RankView { get; }
        string SuitView { get; }
        RankComponent Rank { get; }
        int Color { get; }
        PlayingCardViewModelType CardType { get; }
    }
}