using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HexKeyGames
{
    public class CreditsAutoScroll : MonoBehaviour, IScrollHandler
    {
        [SerializeField, Range(0f, 5f)]
        private float startPositionNormalized = 2.40f;
        [SerializeField, Range(0f, -5f)]
        private float endPositionNormalized = -1.40f;

        private ScrollRect scrollRect;

        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
        }

        private void OnEnable()
        {
            //scrollRect.verticalNormalizedPosition = 2f;
            scrollRect.verticalNormalizedPosition = startPositionNormalized;
            StartCoroutine(StartAutoScrollAfter(0f));
        }

        public void OnScroll(PointerEventData _)
        {
            StopAllCoroutines();
            StartCoroutine(StartAutoScrollAfter(2f));
        }

        private IEnumerator AutoScroll()
        {
            while (true)
            {
                scrollRect.verticalNormalizedPosition -= 0.1f * Time.deltaTime;
                if (scrollRect.verticalNormalizedPosition < endPositionNormalized)
                    scrollRect.verticalNormalizedPosition = startPositionNormalized;
                yield return null;
            }
        }

        private IEnumerator StartAutoScrollAfter(float seconds)
        {
            yield return new WaitForSecondsRealtime(seconds);
            StartCoroutine(AutoScroll());
        }
    }
}
