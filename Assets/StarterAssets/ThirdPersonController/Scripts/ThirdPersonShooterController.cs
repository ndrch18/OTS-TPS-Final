using UnityEngine;
using Unity.Cinemachine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera aimCamera;
    private StarterAssetsInputs starterAssetsInputs;

    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (starterAssetsInputs.aim)
        {
            aimCamera.gameObject.SetActive(true);
        }
        else
        {
            aimCamera.gameObject.SetActive(false);
        }
    }
}