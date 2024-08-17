using App.Scripts.DurakGame.PlayingCards.Views.Configs;
using App.Scripts.DurakGame.PlayingCards.Views.ViewModel;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.DurakGame.PlayingCards.Views
{
    public class PlayingCardView : SerializedMonoBehaviour, IPlayingCardView
    {
        [SerializeField] private PlayingCardViewConfig _viewConfig;
        [SerializeField] private IPlayingCardViewComponent[] _viewComponents;
        
        public void UpdateView(IPlayingCardViewModel viewModel)
        {
            foreach (var viewComponent in _viewComponents)
            {
                if (viewComponent.IsActive(viewModel))
                {
                    viewComponent.Enable();
                    viewComponent.UpdateView(viewModel, _viewConfig);
                }
                else
                {
                    viewComponent.Disable();
                }
            }
        }

        public void Release()
        {
            foreach (var viewComponent in _viewComponents)
            {
                viewComponent.Release();
            }
        }
    }
}