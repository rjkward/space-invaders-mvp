using UnityEngine;
using Zenject;

namespace SpaceInvadersMVP.UI
{
    public abstract class View<T> : MonoBehaviour where T : ViewModel
    {
        [Inject]
        protected T ViewModel;

        private void OnDestroy()
        {
            ViewModel.Dispose();
        }
    }
}
