using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace SpaceInvadersMVP.Manager
{
    public abstract class UIManager<TState> : IDisposable where TState : Enum
    {
        [Inject]
        protected Dictionary<TState, List<GameObject>> ViewPrefabsByState;

        protected Dictionary<GameObject, GameObject> CachedViewsByPrefab =
            new Dictionary<GameObject, GameObject>();

        [Inject]
        private Canvas _canvas;

        [Inject]
        private DiContainer _container;

        private List<GameObject> _displayedViews = new List<GameObject>();

        private List<GameObject> _newStateBuffer = new List<GameObject>();

        protected void DisplayViewsForState(TState state)
        {
            _newStateBuffer.Clear();
            if (ViewPrefabsByState.TryGetValue(state, out List<GameObject> viewPrefabs))
            {
                foreach (GameObject viewPrefab in viewPrefabs)
                {
                    if (!CachedViewsByPrefab.TryGetValue(viewPrefab, out GameObject viewInstance))
                    {
                        viewInstance = _container.InstantiatePrefab(viewPrefab, _canvas.transform);
                        CachedViewsByPrefab[viewPrefab] = viewInstance;
                    }

                    if (!viewInstance.activeSelf)
                    {
                        viewInstance.SetActive(true);
                    }

                    _displayedViews.Remove(viewInstance);
                    _newStateBuffer.Add(viewInstance);
                }
            }

            foreach (GameObject previousStateView in _displayedViews)
            {
                previousStateView.SetActive(false);
            }

            _displayedViews.Clear();
            _displayedViews.AddRange(_newStateBuffer);
        }


        public virtual void Dispose()
        {
            ViewPrefabsByState = null;
            foreach (GameObject cachedView in CachedViewsByPrefab.Values)
            {
                if (cachedView != null)
                {
                    Object.Destroy(cachedView);
                }
            }

            CachedViewsByPrefab.Clear();
            _displayedViews.Clear();
            _displayedViews = null;
            _newStateBuffer.Clear();
            _newStateBuffer = null;
        }
    }
}
