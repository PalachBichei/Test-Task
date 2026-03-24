using UnityEngine;

public abstract class AutoWeaponBase : MonoBehaviour
{
    [SerializeField] protected Transform firePoint;

    protected TargetSelector TargetSelector;
    protected float CooldownTimer;

    public void SetTargetSelector(TargetSelector targetSelector)
    {
        TargetSelector = targetSelector;
    }

    protected virtual void Update()
    {
        if (!CanRun())
            return;

        CooldownTimer -= Time.deltaTime;
        if (CooldownTimer > 0f)
            return;

        CooldownTimer = GetInterval();
        FireInternal();
    }

    protected Vector3 GetOrigin()
    {
        return firePoint != null ? firePoint.position : transform.position;
    }

    protected abstract bool CanRun();
    protected abstract float GetInterval();
    protected abstract void FireInternal();
}