using UnityEngine;
using Unity.Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera aimCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform pfBulletProjectile;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;
    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
    
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        
    
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)) {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
        } else {
        
            mouseWorldPosition = ray.GetPoint(999f);
        }

    
    
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; 
        cameraForward.Normalize();

        if (starterAssetsInputs.aim)
        {
            aimCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            
        
            thirdPersonController.SetRotateOnMove(false); 
            
        
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 20f));

        
            transform.forward = cameraForward;
        }
        else
        {
            aimCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);

        
        
            thirdPersonController.SetRotateOnMove(false); 
            transform.forward = cameraForward;

            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 20f));
        }

    
        if (starterAssetsInputs.shoot) {
        
            Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            
        
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, 
            Quaternion.LookRotation(aimDir, Vector3.up));
            
            starterAssetsInputs.shoot = false;
        }
    }
}