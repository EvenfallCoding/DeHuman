using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Analytics.IAnalytic;

// ispirato da: https://www.youtube.com/watch?v=tnhlAdj9Xhs&t=5s

// inserire lo script in Weapon, figlio del GameObject PlayerCamera del personaggio.

// questo codice fornisce tutte le funzionalita di cui necessita una arma

public class PlayerWeapon : MonoBehaviour
{
    public enum WeaponType
    {
        Normal,
        Burst // 
    }

    [Header("Action Map")]
    [SerializeField] private InputActionAsset weaponControls; // inserire /Inputs/WeaponsInput.inputactions

    [Header("Muzzle Flash Settings")]
    [SerializeField] private GameObject muzzleFlashPrefab; // inserire prefab del MuzzleFlash
    [SerializeField] private Transform muzzleFlashTransform; // inserire GameObject MuzzleFlashPosition, figlio della arma 

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource; // inserire modulo AudioSource
    [SerializeField] private AudioClip bulletSound; // inserire il suono

    [Header("Weapon Settings")]
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private int magazineSize = 30; // massimo numero di proiettili nel caricatore
    [SerializeField] private float delayBetweenBullets = 0.075f;

    [Header("Burst Settings")]
    [SerializeField] private int burstSize = 3;
    [SerializeField] private float burstCoolDown = 0.3f;
    [SerializeField] private bool requireRetriggerForBurst = true;

    [Header("Reload Settings")]
    [SerializeField] private bool autoReload = true;
    [SerializeField] private float reloadTime = 2f;

    [Header("Shooting Raycast Settings")]
    [SerializeField] private float fireDistance = 100f;
    [SerializeField] private float fireSpread = 1.0f;
    [SerializeField] private LayerMask hitLayers;

    private InputAction shootAction;
    private InputAction reloadAction;

    private int currentBulletsInMagazine;
    private bool isTryingToShoot;
    private bool canShoot = true;
    private bool isReloading;
    private bool isBursting;

    private void Awake()
    {
        InitialiseInputActions();
        currentBulletsInMagazine = magazineSize;
    }
    
    private void Update()
    {
        if (isTryingToShoot && canShoot && !isReloading)
        {
            StartCoroutine(ShootSequence());
        }
    }
    
    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        isTryingToShoot = true;
    }

    private void OnShootCanceled(InputAction.CallbackContext context)
    {
        isTryingToShoot = false;
    }

    private void OnReloadPerformed(InputAction.CallbackContext context)
    {
        AttemptReload();
    }

    private void InitialiseInputActions()
    {
        InputActionMap weaponActionMap = weaponControls.FindActionMap("Weapon");
        shootAction = weaponActionMap.FindAction("Shoot");
        reloadAction = weaponActionMap.FindAction("Reload");

        shootAction.performed += OnShootPerformed;
        shootAction.canceled += OnShootCanceled;
        reloadAction.performed += OnReloadPerformed;

        shootAction.Enable();
        reloadAction.Enable();
    }

    private void UnsubscribeFromInputActions()
    {
        shootAction.performed -= OnShootPerformed;
        shootAction.canceled -= OnShootCanceled;
        reloadAction.performed -= OnReloadPerformed;
    }

    private void OnDestroy() // quando arma disabilitata
    {
        UnsubscribeFromInputActions();
    }

    private bool CheckIfGunCanShoot()
    {
        if (currentBulletsInMagazine <= 0) // se non ci sono proiettili nel caricatore
        {
            return false;
        }

        if (isReloading) // se sta ricaricando munizioni
        {
            return false;
        }

        return true; // puo sparare
    }

    private Vector3 GetRandomShootDirection()
    {
        Vector3 forward = muzzleFlashTransform.forward;

        float spreadX = Random.Range(-fireSpread, fireSpread);
        float spreadY = Random.Range(-fireSpread, fireSpread);

        Quaternion spreadRotation = Quaternion.Euler(spreadX, spreadY, 0f);

        return spreadRotation * forward;
    }

    private void PerformRaycatShoot()
    {
        Vector3 direction = GetRandomShootDirection();

        RaycastHit hit;
        bool hitSomething = Physics.Raycast(muzzleFlashTransform.position, direction, out hit, fireDistance, hitLayers);

        Color lineColor = hitSomething ? Color.red : Color.green;
        Debug.DrawRay(muzzleFlashTransform.position, direction * fireDistance, lineColor, 1f);

        if (hitSomething)
        {
            // hit logic
        }
    }

    private void PlayMuzzleFlash()
    {
        GameObject flashInstance = Instantiate(muzzleFlashPrefab, muzzleFlashTransform);
        Destroy(flashInstance, Mathf.Min(delayBetweenBullets, 0.03f));
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(bulletSound);
        PlayMuzzleFlash();
        currentBulletsInMagazine--;

        TryAutoReload();

        PerformRaycatShoot();
    }

    private IEnumerator NormalShootRoutine()
    {
        if (CheckIfGunCanShoot())
        {
            Shoot();
            yield return new WaitForSeconds(delayBetweenBullets);
        }
    }

    private IEnumerator BurstShootRoutine()
    {
        if (isBursting)
        {
            yield break;
        }

        isBursting = true;

        for (int i = 0; i < burstSize; i++)
        {
            if (!CheckIfGunCanShoot())
            {
                break;
            }

            Shoot();
            yield return new WaitForSeconds(delayBetweenBullets);
        }
        
        if (requireRetriggerForBurst)
        {
            yield return new WaitUntil(() => !isTryingToShoot);
        }

        yield return new WaitForSeconds(burstCoolDown);
        isBursting = false;
    }

    private IEnumerator ShootSequence()
    {
        canShoot = false;

        switch (weaponType)
        {
            case WeaponType.Normal:
                yield return NormalShootRoutine();
                break;
            case WeaponType.Burst:
                yield return BurstShootRoutine();
                break;
        }

        canShoot = true;
    }

    private IEnumerator ReloadSequence()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentBulletsInMagazine = magazineSize; // ricarica completamente il caricatore
        isReloading = false;
    }

    private void AttemptReload()
    {
        if (isReloading || currentBulletsInMagazine >= magazineSize) // se sta gia ricaricando o caricatore gia pieno
        {
            return;
        }
        StartCoroutine(ReloadSequence()); // puo ricaricare
    }

    private void TryAutoReload()
    {
        if (currentBulletsInMagazine <= 0 && autoReload && !isReloading)
        {
            StartCoroutine(ReloadSequence());
        }
    }
}

