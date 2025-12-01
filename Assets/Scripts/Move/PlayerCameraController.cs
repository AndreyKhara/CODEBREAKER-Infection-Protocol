using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace CDB.Input
{
    public class PlayerCameraController : MonoBehaviour
    {   

        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private GlitchCameraEffect _glitchEffect; // Ссылка на компонент глитч-эффектов

        [Header("Настройки вращения")]
        [SerializeField] private float sensitivity = 2.0f; // Чувствительность вращения
        [SerializeField] private float smoothing = 2.0f;   // Сглаживание вращения
        [SerializeField] private float minPitch = -90f;
        [SerializeField] private float maxPitch = 90f;


        private InputAction _cameraControl;
        private Transform _selfTransform;

        private Vector2 _currentMouseDelta;  // Текущее смещение мыши
        private Vector2 _smoothedMouseDelta; // Сглаженное смещение мыши
        private Vector2 _cameraRotation;     // Накопленные углы вращения камеры (x для Yaw, y для Pitch)

        
        [Inject]
        private PlayerInput _playerInput;
        
        private void Awake()
        {
            _cameraControl = _playerInput.actions["CameraControl"];

            _selfTransform = GetComponent<Transform>();

            _cameraControl.performed += OnCameraControlPerformed;
            _cameraControl.performed += OnCameraControlCanceled;

            // Автоматически найти GlitchCameraEffect если не назначен
            if (_glitchEffect == null)
            {
                _glitchEffect = GetComponent<GlitchCameraEffect>();
                if (_glitchEffect == null)
                {
                    _glitchEffect = GlitchCameraEffect.Instance;
                }
            }
            
            // Подписываемся на событие окончания глитч-эффекта
            if (_glitchEffect != null)
            {
                _glitchEffect.OnEffectEnded += OnGlitchEffectEnded;
            }
        }

        /// <summary>
        /// Вызывается когда глитч-эффект заканчивается.
        /// Применяет накопленный offset к реальному положению камеры (snap).
        /// </summary>
        private void OnGlitchEffectEnded(Vector2 offset)
        {
            // "Вливаем" offset в реальное положение камеры
            // Теперь камера остаётся там, где игрок визуально смотрел
            _cameraRotation.x += offset.x;
            _cameraRotation.y -= offset.y; // Минус потому что pitch инвертирован
            
            // Ограничиваем pitch
            _cameraRotation.y = Mathf.Clamp(_cameraRotation.y, minPitch, maxPitch);
        }

        private void OnEnable()
        {
            _cameraControl.Enable();
        }


        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void LateUpdate()
        {
            // Применяем вращение с учётом glitch offset каждый кадр
            ApplyCameraRotation();
        }

        private void OnCameraControlPerformed(InputAction.CallbackContext context)
        {

            _currentMouseDelta = context.ReadValue<Vector2>();

            // Применяем чувствительность
            _currentMouseDelta *= sensitivity;

            // Применяем сглаживание к дельте мыши
            _smoothedMouseDelta.x = Mathf.Lerp(_smoothedMouseDelta.x, _currentMouseDelta.x, 1f / smoothing);
            _smoothedMouseDelta.y = Mathf.Lerp(_smoothedMouseDelta.y, _currentMouseDelta.y, 1f / smoothing);

            // Накапливаем углы вращения
            _cameraRotation.x += _smoothedMouseDelta.x; // Yaw (вращение вокруг Y оси, горизонтальное)
            _cameraRotation.y += _smoothedMouseDelta.y; // Pitch (вращение вокруг X оси, вертикальное)

            // Ограничиваем вертикальное вращение (Pitch), чтобы камера не переворачивалась
            _cameraRotation.y = Mathf.Clamp(_cameraRotation.y, minPitch, maxPitch);
        }

        /// <summary>
        /// Применяет вращение камеры с учётом glitch offset
        /// </summary>
        private void ApplyCameraRotation()
        {
            // Получаем glitch offset
            Vector2 glitchOffset = Vector2.zero;
            if (_glitchEffect != null)
            {
                glitchOffset = _glitchEffect.GetCurrentOffset();
            }

            // Применяем горизонтальное вращение (Yaw) к родительскому объекту (игроку) + glitch offset
            _selfTransform.localRotation = Quaternion.Euler(0f, _cameraRotation.x + glitchOffset.x, 0f);

            // Применяем вертикальное вращение (Pitch) к Transform самой камеры + glitch offset
            float finalPitch = Mathf.Clamp(-_cameraRotation.y + glitchOffset.y, minPitch, maxPitch);
            _cameraTransform.localRotation = Quaternion.Euler(finalPitch, 0f, 0f);
        }

        private void OnCameraControlCanceled(InputAction.CallbackContext context)
        {
            _currentMouseDelta = Vector2.zero;
            _smoothedMouseDelta = Vector2.zero;
        }


        private void OnDisable()
        {
            _cameraControl.performed -= OnCameraControlPerformed;

            _cameraControl.Disable();
            
            // Отписываемся от события глитч-эффекта
            if (_glitchEffect != null)
            {
                _glitchEffect.OnEffectEnded -= OnGlitchEffectEnded;
            }
        }

    }
}
