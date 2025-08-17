using UnityEngine;

namespace Astrostrafer.View
{
    public sealed class AutoScaleBackground : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private int _sortingOrder = -1000;
        [SerializeField] private Vector2 _extraPadding = new Vector2(0.5f, 0.5f);

        private void Awake()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
            }
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponent<SpriteRenderer>();
            }
            if (_spriteRenderer != null)
            {
                _spriteRenderer.sortingOrder = _sortingOrder;
            }
            UpdateBackground(true);
        }

        private void LateUpdate()
        {
            UpdateBackground(false);
        }

        private void UpdateBackground(bool force)
        {
            if (_camera == null || _spriteRenderer == null || _spriteRenderer.sprite == null)
            {
                return;
            }

            float worldHeight = _camera.orthographicSize * 2f;
            float worldWidth = worldHeight * _camera.aspect;

            var spriteSize = _spriteRenderer.sprite.bounds.size; // world units at scale 1
            if (spriteSize.x <= 0f || spriteSize.y <= 0f)
            {
                return;
            }

            float scaleX = (worldWidth + _extraPadding.x) / spriteSize.x;
            float scaleY = (worldHeight + _extraPadding.y) / spriteSize.y;
            transform.localScale = new Vector3(scaleX, scaleY, 1f);

            var camPos = _camera.transform.position;
            transform.position = new Vector3(camPos.x, camPos.y, 0f);
        }
    }
}


