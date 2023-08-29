using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Level1EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemiesOnField;
    public static Level1EnemySpawner Instance;
    [SerializeField] public int scorpianSwampAttacks;//2
    [SerializeField] private GameObject scorpian;
    [SerializeField] private int delayBetweenEachSwamp;
    public List<GameObject> scorpianSpawnPoints;
    public Transform Level1Sp1, Level1Sp2, Level1Sp3;
     int HowManyScorpians;
    [SerializeField] int maxRangeOfScorpians;
    GameObject[] ships;
    private bool levelComplete;
    int countShieldCompletion;
    [SerializeField]Button pistolButton, LaserButton, ShotGunButton;
    [SerializeField] GameObject pistolBullet, LaserBullet, ShotGunBullet;
    [SerializeField]float PistolDamgeForLevel1;
    public bool isNotLock;
    public float boostShiledTime;
    public bool isShieldBooster;
    public void checkNoEnemyLeftToSpawnNewWave()
    {
        if (enemiesOnField.Count == 0)
        {
            isNotLock = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        isNotLock = true;
        enemiesOnField = new List<GameObject>();
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            pistolButton.gameObject.SetActive(true);
            LaserButton.gameObject.SetActive(false);
            ShotGunButton.gameObject.SetActive(false);

            if(pistolBullet.TryGetComponent(out BulletMovement b))
            {
                b.damageOfWeapon = PistolDamgeForLevel1;
            }

        }
        Instance = this;
        levelComplete = false;
        countShieldCompletion = 0;

        scorpianSpawnPoints.Add(Level1Sp1.gameObject);
        scorpianSpawnPoints.Add(Level1Sp2.gameObject);
        scorpianSpawnPoints.Add(Level1Sp3.gameObject);
       // ShieldGrower.Instance.OnShieldPartActivation += CheckLevelCompeletion;

        LoadShields();


    }
    

    public  void CheckLevelCompeletion()
    {
     
            countShieldCompletion++;
        Debug.Log("Total shield complete = " + countShieldCompletion);

        if (countShieldCompletion == ships.Length)
        {
            levelComplete = true;
            if (levelComplete)
            {

                levelComplete = false;
                GameplayUIManager.Instance.LevelCompletePanel();


/*
                Debug.Log(countShieldCompletion + " Total shield COmplete " + ships.Length + "Ships total ");
                Debug.Log("LevelComplete");

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);*/
            }
        }


    }

    private void LoadShields()
    {
        ships = GameObject.FindGameObjectsWithTag("zPoint");


    }




    IEnumerator Level1TimeDelayBetweenS_Swamps()
    {

        Debug.Log(enemiesOnField.Count);
        int randomScorpianSpwanTime = UnityEngine.Random.Range(2, delayBetweenEachSwamp);
      
            yield return new WaitForSeconds(randomScorpianSpwanTime);
            ProduceLevel1Scorpian();

   
    }



    void ProduceLevel1Scorpian()
    {


        HowManyScorpians = Random.Range(1, maxRangeOfScorpians);
        for (int i = 0; i <= HowManyScorpians; i++)
        {
            GameObject e=Instantiate(scorpian, new Vector3(Level1Sp1.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp1.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
           
            GameObject f= Instantiate(scorpian, new Vector3(Level1Sp2.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp2.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
            GameObject  g= Instantiate(scorpian, new Vector3(Level1Sp3.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp3.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
            enemiesOnField.Add(e);
            enemiesOnField.Add(f);
            enemiesOnField.Add(g);
        }
        scorpianSwampAttacks--;
  
        
     /*   if (scorpianSwampAttacks > 0)
        {
            StartCoroutine(Level1TimeDelayBetweenS_Swamps());
       
        }*/

        /*   Vector3 SpawnAreaVector1 = Vector3.zero;
           Vector3 SpawnAreaVector2= Vector3.zero;
           Vector3 SpawnAreaVector3 = Vector3.zero;*/
        /* if (scorpianSpawnPoints.Count == 1)
         {
             SpawnAreaVector = scorpianSpawnPoints[0].transform.position;
             scorpianSpawnPoints.Remove(scorpianSpawnPoints[0]);
         }
         if (scorpianSpawnPoints.Count > 1)
         {
             foreach (GameObject a in scorpianSpawnPoints)
             {
                 SpawnAreaVector = a.transform.position;

                 scorpianSpawnPoints.Remove(a);
                 break;
             }
         }

         for (int i = 0; i < HowManyScorpians; i++)
         {

             Debug.Log("producing....");

             Instantiate(scorpian, new Vector3(SpawnAreaVector.x + VariationInSpawnPosition(), 5, SpawnAreaVector.z + VariationInSpawnPosition()), Quaternion.identity);
         }

         if (scorpianSwampAttacks> 0)
         {
             StartCoroutine(Level1TimeDelayBetweenS_Swamps());
         }
 */

    }


    private float VariationInSpawnPosition()
    {
        float variationFactor = UnityEngine.Random.Range(-5, +5);
        return variationFactor;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesOnField.Count == 0 & scorpianSwampAttacks>0 & isNotLock)
        {
            isNotLock = false;
            StartCoroutine(Level1TimeDelayBetweenS_Swamps());
            
        }
        if (scorpianSwampAttacks <= 0 & enemiesOnField.Count <= 0)
        {
            ShieldGrower.Instance.shieldPartActivationDelay *= 1 / 5;
        }
    }
}
