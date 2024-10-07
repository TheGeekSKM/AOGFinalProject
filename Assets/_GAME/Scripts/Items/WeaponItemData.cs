using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[CreateAssetMenu(fileName = "WeaponItemData", menuName = "Items/WeaponItemData")]
public class WeaponItemData : ItemData
{
    [Header("Weapon Data Shooting Config")]
    public LayerMask HitLayer;
    public Vector3 Spread = new Vector3(0.1f, 0.1f, 0.1f);
    public float FireRate = 0.25f;
    public float Damage = 10f;

    [Header("Weapon Data Trail Config")]
    public Gradient ColorOverLifeTime;
    public Material Material;
    public AnimationCurve WidthCurve;
    public float Duration = 0.5f;
    public float MinVertexDistance = 0.1f;
    public float MissDistance = 100f;
    public float SimulationSpeed = 100f;

    [Header("Weapon Data Effects")]
    public ParticleSystem ShootSystem;
    public GameObject HitEffect;
    private ObjectPool<TrailRenderer> _trailPool;
    float _lastShootTime;
    MonoBehaviour _owner;
    Transform _parent;

    public override void UseItem()
    {
        var pawnAttackController = PawnController.Instance.AttackController;
        pawnAttackController.EquippedWeapon = this;

        _owner = pawnAttackController;
        _parent = PawnController.Instance.FirePoint;

        

        _lastShootTime = 0f;
        _trailPool = new ObjectPool<TrailRenderer>(CreateTrail);
    }

    TrailRenderer CreateTrail()
    {
        var trail = new GameObject("Trail").AddComponent<TrailRenderer>();
        trail.colorGradient = ColorOverLifeTime;
        trail.material = Material;
        trail.widthCurve = WidthCurve;
        trail.time = Duration;
        trail.minVertexDistance = MinVertexDistance;

        trail.emitting = false;
        trail.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        return trail;
    }

    public void Shoot()
    {
        if (Time.time > FireRate + _lastShootTime)
        {
            _lastShootTime = Time.time;
            ShootSystem.Play();
            Vector3 shootDirection = _parent.transform.forward + new Vector3(
                Random.Range(-Spread.x, Spread.x), 
                Random.Range(-Spread.y, Spread.y), 
                Random.Range(-Spread.z, Spread.z)
            );
            shootDirection.Normalize();

            if (Physics.Raycast(ShootSystem.transform.position, shootDirection, out RaycastHit hit, MissDistance, HitLayer))
            {
                _owner.StartCoroutine(ShootTrail(_parent.transform.position, hit.point, hit));
            }
            else
            {
                _owner.StartCoroutine(ShootTrail(_parent.transform.position, _parent.transform.position + shootDirection * MissDistance, new RaycastHit()));
            }
        }
    }

    IEnumerator ShootTrail(Vector3 start, Vector3 end, RaycastHit hit)
    {
        var trail = _trailPool.Get();
        trail.gameObject.SetActive(true);
        trail.transform.position = start;
        
        yield return null;

        trail.emitting = true;
        trail.AddPosition(start);
        float distance = Vector3.Distance(start, end);
        float time = distance / SimulationSpeed;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            trail.AddPosition(Vector3.Lerp(start, end, elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        trail.AddPosition(end);

        //TODO: Add hit effect
        if (hit.collider != null)
        {
            Debug.Log($"Hit {hit.collider.name}");
            var health = hit.collider.GetComponent<Health>();
            if (health) health.ChangeHealth(-Damage);

            if (HitEffect != null)
            {
                Instantiate(HitEffect, hit.point, Quaternion.identity);
            }
        }

        yield return new WaitForSeconds(Duration);
        yield return null;

        trail.emitting = false;
        trail.gameObject.SetActive(false);
        _trailPool.Release(trail);
    }
}
