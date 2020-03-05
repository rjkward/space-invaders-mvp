using System;
using Zenject;

namespace SpaceInvadersMVP.UI
{
    public abstract class ViewModel : IInitializable, IDisposable
    {
        [Inject]
        protected SignalBus SignalBus;

        public virtual void Initialize() { }

        public virtual void Dispose() { }
    }
}
