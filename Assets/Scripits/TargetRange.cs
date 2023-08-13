using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class TargetRange : MonoBehaviour
{
    public static TargetRange Instance { get; private set; }
    public event EventHandler <OnEnemyInTargetEventArgs>OnEnemyInTarget;
    
    [SerializeField]private float checkRadius;
    [SerializeField] private LayerMask enemyLayer;
    List<GameObject> enemies;
    List<GameObject> Shields;
    public event EventHandler OnNoEnemyInTarget;
    public class OnEnemyInTargetEventArgs:EventArgs
    {
        public Vector3 enemyPosition;
    }

    private void Start()
    {
        enemies = new List<GameObject>();
        Instance = this;

      

    }


    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
            OnEnemyInTarget?.Invoke(this, new OnEnemyInTargetEventArgs { enemyPosition = other.transform.position });
        }
        else
        {
            bool noEnemiesInRadius = !Physics.CheckSphere(transform.position, checkRadius, enemyLayer);
            if (noEnemiesInRadius)
            {
                OnNoEnemyInTarget?.Invoke(this, EventArgs.Empty);
            }
        }


    }


    private void OnTriggerExit(Collider other)
    {
     /*   if (other.CompareTag("Enemy"))
        {
            enemies.Remove(other.gameObject);
            if (enemies.Count == 0)
            {
                OnNoEnemyInTarget?.Invoke(this, EventArgs.Empty);
            }
        }*/
    }


}


