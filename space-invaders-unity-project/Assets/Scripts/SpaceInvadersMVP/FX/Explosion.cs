using System;
using DG.Tweening;
using SpaceInvadersMVP.Util;
using TMPro;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace SpaceInvadersMVP.FX
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Explosion : MonoBehaviour, IPoolable<Vector2, bool, IMemoryPool>, IDisposable
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        [SerializeField]
        private TextMesh _textMesh;

        private IMemoryPool _pool;

        private Sequence _animationSequence;

        private static readonly string[] EncouragingTextOptions =
        {
            "wow", "cool", "extremely\ngood", "nice", "awesome", "yay", "excellent",
            "wooooooo!", "phwoar", "whoah", "chef\nkiss", "positive\nreinforcement",
            "fire\nemoji", "incredible", "amazing"
        };

        private static readonly string[] DiscouragingTextOptions =
        {
            "oh no", "oops", "oh\ndear", "nooooooo!", "oh god", "errr...", "whatever", "wait\nwhat"
        };

        private void Awake()
        {
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }

        public void OnSpawned(Vector2 position, bool benefitsPlayer, IMemoryPool pool)
        {
            SetStartingValues(position, benefitsPlayer);
            _pool = pool;
            Animate();
        }

        private void SetStartingValues(Vector2 position, bool benefitsPlayer)
        {
            Transform t = transform;
            t.position = position;
            t.localScale = Vector3.one * 0.5f;

            string[] textOptions = benefitsPlayer ? EncouragingTextOptions : DiscouragingTextOptions;
            _textMesh.text = textOptions[Random.Range(0, textOptions.Length)];

            Color color = benefitsPlayer ? Color.green : Color.red;
            color.a = 0.5f;
            _spriteRenderer.color = color;
        }

        private void Animate()
        {
            _animationSequence = DOTween.Sequence();
            _animationSequence.Append(transform.DOScale(Vector3.one * 3f, Config.ExplosionDuration));
            _animationSequence.Insert(0f, _spriteRenderer.DOFade(0f, Config.ExplosionDuration));
            _animationSequence.onComplete = () =>
            {
                _animationSequence = null;
                Dispose();
            };
        }

        public void OnDespawned()
        {
            _pool = null;
        }

        public void Dispose()
        {
            if (_animationSequence != null)
            {
                _animationSequence.Kill();
                _animationSequence = null;
            }

            _pool?.Despawn(this);
        }

        public class Factory : PlaceholderFactory<Vector2, bool, Explosion> { }
    }
}
