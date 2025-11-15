using UnityEngine;

namespace CDB.LevelsSystem
{
    /// <summary>
    /// Покадровая 2D анимация на спрайте для 3D портала
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class PortalSpriteAnimation : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private Sprite[] animationFrames;
        [SerializeField] private float frameRate = 12f;
        [SerializeField] private bool loop = true;
        
        [Header("Billboard Settings")]
        [SerializeField] private bool faceCamera = true;
        [SerializeField] private bool lockYAxis = true;
        
        private SpriteRenderer _spriteRenderer;
        private float _timer;
        private int _currentFrame;
        private Transform _cameraTransform;
        private float _frameInterval;
        private int _framesLength;
        private bool _hasFrames;
        
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            CacheAnimationData();
        }
        
        private void Start()
        {
            if (faceCamera)
            {
                FindCamera();
            }
        }
        
        private void CacheAnimationData()
        {
            _hasFrames = animationFrames != null && animationFrames.Length > 0;
            
            if (_hasFrames)
            {
                _framesLength = animationFrames.Length;
                _frameInterval = 1f / frameRate;
                _spriteRenderer.sprite = animationFrames[0];
            }
        }
        
        private void FindCamera()
        {
            _cameraTransform = Camera.main?.transform;
            
            if (_cameraTransform == null)
            {
                Camera cam = FindFirstObjectByType<Camera>();
                if (cam != null)
                {
                    _cameraTransform = cam.transform;
                }
            }
        }
        
        private void Update()
        {
            if (_hasFrames)
            {
                UpdateAnimation();
            }
        }
        
        private void LateUpdate()
        {
            if (faceCamera)
            {
                if (_cameraTransform == null)
                {
                    FindCamera();
                }
                else
                {
                    UpdateBillboard();
                }
            }
        }
        
        private void UpdateAnimation()
        {
            _timer += Time.deltaTime;
            
            if (_timer >= _frameInterval)
            {
                _timer -= _frameInterval;
                _currentFrame++;
                
                if (_currentFrame >= _framesLength)
                {
                    _currentFrame = loop ? 0 : _framesLength - 1;
                    if (!loop) return;
                }
                
                _spriteRenderer.sprite = animationFrames[_currentFrame];
            }
        }
        
        private void UpdateBillboard()
        {
            Vector3 directionToCamera = _cameraTransform.position - transform.position;
            
            if (lockYAxis)
            {
                directionToCamera.y = 0;
            }
            
            if (directionToCamera.sqrMagnitude > 0.001f)
            {
                transform.rotation = Quaternion.LookRotation(directionToCamera);
            }
        }
        
        /// <summary>
        /// Установить новые кадры анимации
        /// </summary>
        public void SetFrames(Sprite[] frames)
        {
            animationFrames = frames;
            _currentFrame = 0;
            _timer = 0f;
            CacheAnimationData();
        }
        
        /// <summary>
        /// Сбросить анимацию на первый кадр
        /// </summary>
        public void ResetAnimation()
        {
            _currentFrame = 0;
            _timer = 0f;
            
            if (_hasFrames)
            {
                _spriteRenderer.sprite = animationFrames[0];
            }
        }
    }
}
