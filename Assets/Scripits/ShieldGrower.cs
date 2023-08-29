using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ShieldGrower : MonoBehaviour
{
    public static ShieldGrower Instance { get;private  set; }
    public event EventHandler <OnShieldPartActioavtionEventArgs>OnShieldPartActivation;

    public class OnShieldPartActioavtionEventArgs : EventArgs
    {
       public GameObject shieldPart;
    }
    public Transform shieldPartToActivate;
   [SerializeField] GameObject shield;
    [SerializeField] public float shieldPartActivationDelay;
    private int shieldPartNo;
    int totalShieldPartActive;
    public List<GameObject>activeShieldParts;
    private bool isShieldBooster;
    public ParticleSystem shieldBoosterParticles;

    public int ShieldPartNo()
    {
        return shieldPartNo;
    }
    // Start is called before the first frame update
    void Start()
    {
        shieldPartNo = 0;
        activeShieldParts = new List<GameObject>();
        Instance = this;
        LoadShieldParts();

        StartCoroutine(ShieldPartActivationDelay());
    }


    IEnumerator ShieldPartActivationDelay()
    {
        ActivateShieldPart();
        yield return new WaitForSeconds(shieldPartActivationDelay);



        StartCoroutine(ShieldPartActivationDelay());
    }

    private void ActivateShieldPart()
    {

     






        foreach (GameObject part in activeShieldParts)
        {

            if (!part.activeInHierarchy)
            {
                totalShieldPartActive++;
                if (part.name == "Part32"  && SceneManager.GetActiveScene().name=="Level1" )
                {
                    Level1EnemySpawner.Instance.CheckLevelCompeletion();
                }
                if (part.name == "Part32" && SceneManager.GetActiveScene().name == "Level2")
                {
                    Level2EnemySpawner.Instance.CheckLevelCompeletion();
                }
                if (part.name == "Part32" && SceneManager.GetActiveScene().name == "Level3")
                {
                    Level2EnemySpawner.Instance.CheckLevelCompeletion();
                }
                if (part.name == "Part32" && SceneManager.GetActiveScene().name == "Level4")
                {
                    Level2EnemySpawner.Instance.CheckLevelCompeletion();
                }
                if (part.name == "Part32" && SceneManager.GetActiveScene().name == "Level5")
                {
                    Level2EnemySpawner.Instance.CheckLevelCompeletion();
                }
                if (part.name == "Part32" && SceneManager.GetActiveScene().name == "Level6")
                {
                    Level2EnemySpawner.Instance.CheckLevelCompeletion();
                }
                part.SetActive(true);
                break;

            }

           
        }
        // shieldPartToActivate.gameObject.SetActive(true);

    }

    private void LoadShieldParts()
    {
       for(int i = 0; i <= 31; i++)
        {
            shieldPartToActivate = shield.transform.GetChild(31 - shieldPartNo);
            activeShieldParts.Add(shieldPartToActivate.gameObject);
            shieldPartNo++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (Level1EnemySpawner.Instance.scorpianSwampAttacks <= 0 & Level1EnemySpawner.Instance.enemiesOnField.Count <= 0)
            {
                if (!isShieldBooster)
                {
                    isShieldBooster = true;
                    SoundManager.Instance.Play("shieldGrower");
                    shieldPartActivationDelay *= 1 / 5;
                    Instantiate(shieldBoosterParticles, transform.position, Quaternion.identity);
                    shieldBoosterParticles.gameObject.SetActive(true);
                    shieldBoosterParticles.Play();

                }
            }
        }
        else
        {

            if (Level2EnemySpawner.Instance.scorpianSwampAttacks <= 0 & Level2EnemySpawner.Instance.enemiesOnField.Count <= 0)
            {
                if (!isShieldBooster)
                {
                    Debug.Log("Playing it");
                    isShieldBooster = true;
                    SoundManager.Instance.Play("shieldGrower");
                    shieldPartActivationDelay *= 1 / 5;
                    Instantiate(shieldBoosterParticles, transform.position+Vector3.up*3, Quaternion.identity);
                    shieldBoosterParticles.gameObject.SetActive(true);
                    shieldBoosterParticles.Play();
                }
            }

        }
    }
}
