using App.Scripts.Cards.Components;
using App.Scripts.DurakGame.PlayingCards.Views.Components.Suits.Group;
using App.Scripts.DurakGame.PlayingCards.Views.Configs;
using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;
using UnityEngine;

namespace App.Scripts.DurakGame.PlayingCards.Views.Components.Suits
{
    public class SuitFrontView : MonoBehaviour, IPlayingCardViewComponent
    {
        [SerializeField] private SuitVerticalGroupView _leftGroup;
        [SerializeField] private SuitVerticalGroupView _centerGroup;
        [SerializeField] private SuitVerticalGroupView _rightGroup;

        public bool IsActive(IPlayingCardViewModel viewModel)
        {
            return viewModel.CardType is PlayingCardViewModelType.Numeric || IsAce(viewModel);
        }

        public void Enable()
        {
            _leftGroup.Construct();
            _centerGroup.Construct();
            _rightGroup.Construct();
            gameObject.SetActive(true);
        }

        public void UpdateView(IPlayingCardViewModel viewModel, PlayingCardViewConfig viewConfig)
        {
            var config = GetSuitFrontData(viewModel, viewConfig);
            _leftGroup.ShowSuitsCount(config.LeftGroupSuitsCount, viewModel, viewConfig);
            _centerGroup.ShowSuitsCount(config.CenterGroupSuitsCount, viewModel, viewConfig);
            _rightGroup.ShowSuitsCount(config.RightGroupSuitsCount, viewModel, viewConfig);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public void Release()
        {
            _leftGroup.Release();
            _centerGroup.Release();
            _rightGroup.Release();
        }

        private static SuitFrontViewConfigData GetSuitFrontData(IPlayingCardViewModel viewModel, PlayingCardViewConfig viewConfig)
        {
            return IsAce(viewModel)
                ? new SuitFrontViewConfigData(0, 1, 0)
                : viewConfig.GetSuitDataForRank(viewModel.Rank.Value);
        }

        private static bool IsAce(IPlayingCardViewModel viewModel)
        {
            return viewModel.Rank.Equals(RankComponent.Ace);
        }
    }
}