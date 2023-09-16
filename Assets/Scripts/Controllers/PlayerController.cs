using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Controls controls;
    [SerializeField] private GameObject turret;
    [SerializeField] private Transform ShootingPos;
    private float rotateby = 0;

    private void Awake()
    {
        controls = new Controls();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    void Start()
    {
        controls.Turret.Clockwise.performed += ctx => RotateTurret(-1);
        controls.Turret.Clockwise.canceled += ctx => RotateTurret(0);
        controls.Turret.CounterClockwise.performed += ctx => RotateTurret(1);
        controls.Turret.CounterClockwise.canceled += ctx => RotateTurret(0);
        controls.Turret.Shoot.performed += ctx => Shoot();
        controls.MainMenu.Pause.performed += ctx => PauseGame();
    }

    private void PauseGame()
    {
        GameManager.instance.UpdateGameState(GameState.Paused);
    }

    private void Shoot()
    {
        var direction = ShootingPos.position - turret.transform.position;
        BulletPoolController.instance.SpawnBullet(ShootingPos.position,direction);
    }

    private void RotateTurret(int i)
    {
        Debug.Log($"RotateTurret : {i} : {turret.transform.rotation}");
        rotateby = i;
    }

    private void Update()
    {
        turret.transform.Rotate(0, 0, rotateby/20);
        var currentRotation = turret.transform.rotation;
        currentRotation.z = Mathf.Clamp(currentRotation.z, -0.75f, 0.75f);
        turret.transform.rotation = currentRotation;
    }

}