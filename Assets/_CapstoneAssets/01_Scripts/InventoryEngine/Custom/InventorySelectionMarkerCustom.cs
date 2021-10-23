using UnityEngine;
using System.Collections;
using MoreMountains.Tools;
using UnityEngine.EventSystems;

namespace MoreMountains.InventoryEngine
{
    [RequireComponent(typeof(RectTransform))]
    /// <summary>
    /// This class handles the selection marker, that will mark the currently selected slot.
    /// MIKE NOTE: I just copied and pasted the code from the original Selection Market rather than extending the script because I didn't know how to insert arbitrary lines of code into the update loop from an extended class.
    /// My bad if that at any point causes confusion later. x_x
    /// </summary>
    public class InventorySelectionMarkerCustom : MonoBehaviour
    {
        [MMInformation("The selection marker will highlight the current selection. Here you can define its transition speed and minimal distance threshold (it's usually ok to leave it to default).", MMInformationAttribute.InformationType.Info, false)]
        /// the speed at which the selection marker will move from one slot to the other
        public float TransitionSpeed = 5f;
        /// the threshold distance at which the marker will stop moving
        public float MinimalTransitionDistance = 0.01f;

        protected RectTransform _rectTransform;
        protected GameObject _currentSelection;
        protected Vector3 _originPosition;
        protected Vector3 _originLocalScale;
        protected Vector3 _originSizeDelta;
        protected float _originTime;
        protected bool _originIsNull = true;
        protected float _deltaTime;

        [SerializeField] Transform itemSlotParent;

        /// <summary>
        /// On Start, we get the associated rect transform
        /// </summary>
        void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        /// <summary>
        /// On Update, we get the current selected object, and we move the marker to it if necessary
        /// </summary>
        void Update()
        {
            _currentSelection = EventSystem.current.currentSelectedGameObject;
            if (_currentSelection == null)
            {
                return;
            }

            if (_currentSelection.gameObject.MMGetComponentNoAlloc<InventorySlot>() == null)
            {
                return;
            }

            // Added line below to check if the selected object is a related to the current inventory. This will allow the marker to keep its place on this inventory screen.
            if(_currentSelection.transform.IsChildOf(itemSlotParent) != true)
            {
                return;
            }

            if (Vector3.Distance(transform.position, _currentSelection.transform.position) > MinimalTransitionDistance)
            {
                if (_originIsNull)
                {
                    _originIsNull = false;
                    _originPosition = transform.position;
                    _originLocalScale = _rectTransform.localScale;
                    _originSizeDelta = _rectTransform.sizeDelta;
                    _originTime = Time.unscaledTime;
                }
                _deltaTime = (Time.unscaledTime - _originTime) * TransitionSpeed;
                transform.position = Vector3.Lerp(_originPosition, _currentSelection.transform.position, _deltaTime);
                _rectTransform.localScale = Vector3.Lerp(_originLocalScale, _currentSelection.GetComponent<RectTransform>().localScale, _deltaTime);
                _rectTransform.sizeDelta = Vector3.Lerp(_originSizeDelta, _currentSelection.GetComponent<RectTransform>().sizeDelta, _deltaTime);
            }
            else
            {
                _originIsNull = true;
            }
        }
    }
}