using UnityEngine;

namespace CDB.LevelsSystem
{
    /// <summary>
    /// Billboard эффект для портала - всегда поворачивается лицом к камере
    /// </summary>
    public class PortalSpriteAnimation : MonoBehaviour
    {
        [Header("Billboard Settings")]
        [SerializeField] private bool faceCamera = true;
        [SerializeField] private bool lockYAxis = true;
        
        private Transform _cameraTransform;
        
        private void Start()
        {
            if (faceCamera)
            {
                FindCamera();
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
    }
}
