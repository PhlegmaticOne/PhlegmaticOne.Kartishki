using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Scripts.DurakGame.PlayingCards.Views
{
    public class PlayingCardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("Begin drag");
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.pointerCurrentRaycast.worldPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("End drag");
        }
    }
}