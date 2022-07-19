using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public enum FireRate { Fast = 1, Normal = 2, Slow = 3 }

public class TurretBehavior : MonoBehaviour
{
    //private PhotonView _photonView;
    [SerializeField]private List<Transform> _enemiesInRange;
    private Transform _target;
    private const string _enemyTag = "Enemy";
    private bool _canShoot;

    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _turretGun, _projectilePosLeft, _projectilePosRight;
    [SerializeField] private FireRate _fireRate = FireRate.Normal;
    [SerializeField] private float _projectileForwardForce = 10000f;
    [SerializeField] private float _fireRateModifier = 1.75f;


    private delegate void State();
    private State _state;

    private void Awake()
    {
        _state = Idle;
    }

    private void Start()
    {
        //_photonView = GetComponent<PhotonView>();
        _canShoot = true;

        if (_state != Idle)
        {
            _state = Idle;
        }
    }

    private void Update()
    {
        _state.Invoke();
        if (_enemiesInRange.Count>0)
        {

            if (_target = _enemiesInRange[0])
            {
                print($"New Target Set {_target.name} )");
            }
        }
    }
    //

    private void OnTriggerEnter(Collider other)
    {
        // locking on target
        if (other.tag == _enemyTag)
        {
            Debug.Log($"ALERT: new enemy at {gameObject.name} attacking range");
            _enemiesInRange.Add(other.gameObject.transform);

            //_photonView.RPC("SwitchToTargetLockedRPC", RpcTarget.All);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // returning to idle
        if (other.tag == _enemyTag)
        {
            Debug.Log($"ALERT: an enemy was removed from {gameObject.name} attacking range");
            _enemiesInRange.Remove(other.gameObject.transform);

            //_photonView.RPC("SwitchToIdleRPC", RpcTarget.All);
        }
    }

    private void Idle()
    {
        Debug.Log("Current State: Idle");

        if (_target != null)
        {
            _state = TargetLocked;
        }
        //else if (_enemiesInRange[0] != null)
        //{
        //    _target = _enemiesInRange[0];
        //    _state = TargetLocked;
        //}

        if (_turretGun.rotation != Quaternion.identity)
        {
            _turretGun.rotation = Quaternion.identity;
        }
    }

    private void TargetLocked()
    {
        Debug.Log("Current State: TargetLocked");
        _enemiesInRange.Sort();
        _target = _enemiesInRange[0];

        // check if _target != null
        if (!_target)
        {
            _state = Idle;
        }

        _turretGun.LookAt(_target);

        if (_canShoot)
            StartCoroutine(ShootProjectiles(_fireRate));
    }

    private IEnumerator ShootProjectiles(FireRate fireRate)
    {
        _canShoot = false;
        GameObject leftProjectile = Instantiate(_projectile, _projectilePosLeft.position, _turretGun.rotation);
        GameObject rightProjectile = Instantiate(_projectile, _projectilePosRight.position, _turretGun.rotation);

        Rigidbody leftProjectileRB = leftProjectile.GetComponent<Rigidbody>();
        Rigidbody rightProjectileRB = rightProjectile.GetComponent<Rigidbody>();

        leftProjectileRB.AddForce(_projectileForwardForce * Time.deltaTime * _turretGun.forward);
        rightProjectileRB.AddForce(_projectileForwardForce * Time.deltaTime * _turretGun.forward);

        yield return new WaitForSeconds((int)fireRate / _fireRateModifier);

        _canShoot = true;
    }
    //
    //[PunRPC]
    //private void SwitchToIdleRPC()
    //{
    //    _state = Idle;
    //}
    //
    //[PunRPC]
    //private void SwitchToTargetLockedRPC()
    //{
    //    _state = TargetLocked;
    //}
}
