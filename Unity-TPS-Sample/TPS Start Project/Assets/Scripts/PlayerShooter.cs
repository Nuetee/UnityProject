using UnityEngine;


public class PlayerShooter : MonoBehaviour
{
    public enum AimState
    {
        Idle,
        HipFire
    }

    public AimState aimState { get; private set; }

    public Gun gun;
    public LayerMask excludeTarget;
    
    private PlayerInput playerInput;
    private Animator playerAnimator;
    private Camera playerCamera;
    
    private float waitingTimeForReleasingAim = 2.5f; // 견착상태에서 2.5초지나면 Idle상태로 되돌아오도록
    private float lastFireInputTime;

    private Vector3 aimPoint;
    private bool linedUp => !(Mathf.Abs( playerCamera.transform.eulerAngles.y - transform.eulerAngles.y) > 1f);
    private bool hasEnoughDistance => !Physics.Linecast(transform.position + Vector3.up * gun.fireTransform.position.y,gun.fireTransform.position, ~excludeTarget);
    
    void Awake()
    {
        if (excludeTarget != (excludeTarget | (1 << gameObject.layer)))
        {
            excludeTarget |= 1 << gameObject.layer;
        }
    }

    private void Start()
    {
        playerCamera = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        aimState = AimState.Idle;
        gun.gameObject.SetActive(true);
        gun.Setup(this);
    }

    private void OnDisable()
    {
        aimState = AimState.Idle;
        gun.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (playerInput.fire)
        {
            lastFireInputTime = Time.time; // 마지막 발사시간 갱신 
            Shoot();
        }
        else if (playerInput.reload)
        {
            Reload();
        }
    }

    private void Update()
    {
        UpdateAimTarget();

        //상체의 숙임 정도 애니메이션 구현, parameter는 Angle. Angle = 0 ->아래숙임, Angle = 1 -> 위 바라봄
        var angle = playerCamera.transform.eulerAngles.x;
        if(angle > 270f) angle -= 360f;

        angle = angle / -180f + 0.5f;
        playerAnimator.SetFloat("Angle", angle);

        //발사버튼을 누르지 않고 있고 마지막 발사 시점부터 2.5초 이상 흘렀을 때 aimState를 Idle로 변경
        if(!playerInput.fire && Time.time >= lastFireInputTime + waitingTimeForReleasingAim)
        {
            aimState = AimState.Idle;
        }
        UpdateUI();
    }

    public void Shoot()
    {
        if(aimState == AimState.Idle){
            if(linedUp) aimState = AimState.HipFire;
        }
        if(aimState == AimState.HipFire){
            if(hasEnoughDistance){
                if(gun.Fire(aimPoint)){ // 발사시도
                    playerAnimator.SetTrigger("Shoot");
                }
            }
            else{
                aimState = AimState.Idle;
            }
        }
    }

    public void Reload()
    {
        if(gun.Reload())
        {
            playerAnimator.SetTrigger("Reload");
        }
    }

    private void UpdateAimTarget()
    {
        RaycastHit hit;
        // ViewportPointToRay는 Viewport상에 한 점을 찍어주면 그 점을 향해 나아가는 Ray를 생성해줌
        var ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if(Physics.Raycast(ray, out hit, gun.fireDistance, ~excludeTarget)) {
            aimPoint = hit.point;

            if(Physics.Linecast(gun.fireTransform.position, hit.point, out hit, ~excludeTarget)) {
                aimPoint = hit.point;
            }
        }
        else{
            aimPoint = playerCamera.transform.position + playerCamera.transform.forward * gun.fireDistance;
        }
    }

    private void UpdateUI()
    {
        if (gun == null || UIManager.Instance == null) return;
        
        UIManager.Instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        
        UIManager.Instance.SetActiveCrosshair(hasEnoughDistance);
        UIManager.Instance.UpdateCrossHairPosition(aimPoint);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(gun == null || gun.state == Gun.State.Reloading) return;

        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, gun.leftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand, gun.leftHandMount.rotation);
    }
}