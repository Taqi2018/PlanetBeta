using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

public class TargetRange : MonoBehaviour
{
    public static TargetRange Instance { get; private set; }
    public event EventHandler<OnEnemyInTargetEventArgs> OnEnemyInTarget;

    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask enemyLayer;
    List<GameObject> enemies;
    List<GameObject> Shields;
    public event EventHandler OnNoEnemyInTarget;
    public class OnEnemyInTargetEventArgs : EventArgs
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
        /*        if (other.name == "Scorpian" | other.name == "Scorpian(Clone)")
                {
                    other.TryGetComponent(out Enemy e);

                    if (!e.isScorpianDead & (other.transform.position - transform.position).magnitude < checkRadius)
                    {

                        enemies.Add(other.gameObject);

                        OnEnemyInTarget?.Invoke(this, new OnEnemyInTargetEventArgs { enemyPosition = other.transform.position });
                    }
                    else
                    {
                     //   if (!e.isScorpianDead)
                       // {
                            bool noEnemiesInRadius = !Physics.CheckSphere(transform.position, transform.GetComponent<SphereCollider>().radius, enemyLayer);
                            if (noEnemiesInRadius)
                            {
                                ///    StartCoroutine(DelayForStopShooting());
                                OnNoEnemyInTarget?.Invoke(this, EventArgs.Empty);
                            }
                      //  }
             *//*           else
                        {
                            OnNoEnemyInTarget?.Invoke(this, EventArgs.Empty);
                        }*//*
                    }
                }*/


        if (other.name == "Alien" | other.name == "Alien(Clone)")
        {
            other.TryGetComponent(out Enemy e);

            if (!e.isAlienDead & (other.transform.position - transform.position).magnitude < checkRadius)
            {

                enemies.Add(other.gameObject);

                OnEnemyInTarget?.Invoke(this, new OnEnemyInTargetEventArgs { enemyPosition = other.transform.position });


            }
            else
            {

                bool noEnemiesInRadius = Physics.CheckSphere(transform.position, transform.GetComponent<SphereCollider>().radius, enemyLayer);
                if (noEnemiesInRadius)
                {
                  
                    OnNoEnemyInTarget?.Invoke(this, EventArgs.Empty);
                }
            }

        }



        if (other.name == "Scorpian" | other.name == "Scorpian(Clone)")
        {
            other.TryGetComponent(out Enemy e);

            if (!e.isScorpianDead & (other.transform.position - transform.position).magnitude < checkRadius)
            {

                enemies.Add(other.gameObject);

                OnEnemyInTarget?.Invoke(this, new OnEnemyInTargetEventArgs { enemyPosition = other.transform.position });


            }
            else
            {

                bool noEnemiesInRadius = Physics.CheckSphere(transform.position, transform.GetComponent<SphereCollider>().radius, enemyLayer);
                if (noEnemiesInRadius)
                {

                    OnNoEnemyInTarget?.Invoke(this, EventArgs.Empty);
                }
            }

        }




        if (other.name == "Drone" | other.name == "Drone(Clone)")
        {
            other.TryGetComponent(out Enemy e);

            if (!e.isDroneDead & (other.transform.position - transform.position).magnitude < checkRadius)
            {

                enemies.Add(other.gameObject);

                OnEnemyInTarget?.Invoke(this, new OnEnemyInTargetEventArgs { enemyPosition = other.transform.position });


            }
            else
            {

                bool noEnemiesInRadius = Physics.CheckSphere(transform.position, transform.GetComponent<SphereCollider>().radius, enemyLayer);
                if (noEnemiesInRadius)
                {
                //    StartCoroutine(DelayForStopShooting());
                    OnNoEnemyInTarget?.Invoke(this, EventArgs.Empty);
                }
            }

        }



    }

    








}


    /*        if (other.name == "Drone" | other.name == "Drone(Clone)")
            {
                other.TryGetComponent(out Enemy e);

                if (!e.isDroneDead && (other.transform.position - transform.position).magnitude < checkRadius)
                {

                    enemies.Add(other.gameObject);

                    OnEnemyInTarget?.Invoke(this, new OnEnemyInTargetEventArgs { enemyPosition = other.transform.position });
                }
                else
                {
               //     if (!e.isDroneDead)
                 //   {
                        bool noEnemiesInRadius = !Physics.CheckSphere(transform.position, transform.GetComponent<SphereCollider>().radius, enemyLayer);
                        if (noEnemiesInRadius)
                        {
                            ///    StartCoroutine(DelayForStopShooting());
                            OnNoEnemyInTarget?.Invoke(this, EventArgs.Empty);
                        }
                   // }
            *//*        else
                    {
                        OnNoEnemyInTarget?.Invoke(this, EventArgs.Empty);
                    }*//*
                }
            }*/
