using System;
using System.Collections.Generic;
using SpaceInvadersMVP.Model;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace SpaceInvadersMVP.Manager
{
    public class UIManager<TState> : IInitializable, IDisposable where TState : Enum
    {
        [Inject]
        private IUIStateModel<TState> _model;
        
        [Inject]
        private Canvas _canvas;

        [Inject]
        private DiContainer _container;
        
        [Inject]
        private Dictionary<TState, List<GameObject>> ViewPrefabsByState;

        private Dictionary<GameObject, GameObject> CachedViewsByPrefab =
            new Dictionary<GameObject, GameObject>();

        private List<GameObject> _displayedViews = new List<GameObject>();

        private List<GameObject> _newStateBuffer = new List<GameObject>();

        public void Initialize()
        {
            _model.UIState.Subscribe(DisplayViewsForState);
        }

        public virtual void Dispose()
        {
            _model.UIState.Unsubscribe(DisplayViewsForState);
            
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

        private void DisplayViewsForState(TState state)
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
    }
}
