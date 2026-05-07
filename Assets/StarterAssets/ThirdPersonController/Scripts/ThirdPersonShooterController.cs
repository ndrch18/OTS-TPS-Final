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
        UnityEngine.Vector3 mouseWorldPosition = UnityEngine.Vector3.zero;

        UnityEngine.Vector2 screenCenterPoint = new UnityEngine.Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask)) {
            debugTransform.position = raycastHit.point;
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }
        if (starterAssetsInputs.aim)
        {
            aimCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            UnityEngine.Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            UnityEngine.Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = UnityEngine.Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
        }

        if (starterAssetsInputs.shoot) {
            //if (hitTransform != null)
           // {
                // hit something
                //if (hitTransform.GetComponent<BulletTarget>() != null)
                //{
                    //Instantiate(vfxHitGreen, mouseWorldPosition, Quaternion.identity);
                    //Enemy enemy = hitTransform.GetComponent<Enemy>();
                    //if (enemy != null)
                    //{
                        //enemy.TakeDamage();
                    //}
                //}
                //else
               // {
                     // hit something else
                   // Instantiate(vfxHitRed, mouseWorldPosition, Quaternion.identity);
                //}
            //}
            UnityEngine.Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
            Instantiate(pfBulletProjectile, spawnBulletPosition.position, UnityEngine.Quaternion.LookRotation(aimDir, UnityEngine.Vector3.up));
            starterAssetsInputs.shoot = false;
        }
    }
}