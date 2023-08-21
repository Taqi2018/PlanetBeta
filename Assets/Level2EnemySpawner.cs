using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Level2EnemySpawner : MonoBehaviour
{
    public static Level2EnemySpawner Instance;
    [SerializeField] private int scorpianSwampAttacks;//2
    [SerializeField] private GameObject scorpian;
    [SerializeField] private GameObject alien;
    [SerializeField] private GameObject drone;
    [SerializeField] private int delayBetweenEachSwamp;
    public List<GameObject> scorpianSpawnPoints;
    public Transform Level1Sp1, Level1Sp2, Level1Sp3, Level5sp;
    int HowManyScorpians;
    [SerializeField] int maxRangeOfScorpians;
    GameObject[] ships;
    private bool levelComplete;
    int countShieldCompletion;
    [SerializeField] Button pistolButton, LaserButton, ShotGunButton;
    [SerializeField] GameObject pistolBullet, LaserBullet, ShotGunBullet,alienBullet;
    [SerializeField] float PistolDamgeForLevel1;
    [SerializeField] float laserDamgeForLevel3;
    [SerializeField] float alienDamgeForLevel3;
    [SerializeField] float shotGunDamgeLevel6;
    /* p alienSpawnEffect;*/

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            pistolButton.gameObject.SetActive(true);
            LaserButton.gameObject.SetActive(false);
            ShotGunButton.gameObject.SetActive(false);

            if (pistolBullet.TryGetComponent(out BulletMovement b))
            {
                b.damageOfWeapon = PistolDamgeForLevel1;
            }

        }
        if (SceneManager.GetActiveScene().name == "Level3"  || SceneManager.GetActiveScene().name == "Level4" || SceneManager.GetActiveScene().name == "Level5")
        {
            pistolButton.gameObject.SetActive(true);
            LaserButton.gameObject.SetActive(true);
          ShotGunButton.gameObject.SetActive(false);

            if (pistolBullet.TryGetComponent(out BulletMovement b))
            {
                b.damageOfWeapon = PistolDamgeForLevel1;
            }
            if (LaserBullet.TryGetComponent(out BulletMovement l))
            {
                l.damageOfWeapon = laserDamgeForLevel3;
            }

            if (alienBullet.TryGetComponent(out BulletMovement a))
            {
                a.damageOfWeapon = alienDamgeForLevel3;
            }


        }

        if (SceneManager.GetActiveScene().name == "Level6" )
        {
            pistolButton.gameObject.SetActive(true);
            LaserButton.gameObject.SetActive(true);
            ShotGunButton.gameObject.SetActive(true);

            if (pistolBullet.TryGetComponent(out BulletMovement b))
            {
                b.damageOfWeapon = PistolDamgeForLevel1;
            }
            if (LaserBullet.TryGetComponent(out BulletMovement l))
            {
                l.damageOfWeapon = laserDamgeForLevel3;
            }

            if (ShotGunBullet.TryGetComponent(out BulletMovement s))
            {
                s.damageOfWeapon = shotGunDamgeLevel6;
            }

            if (alienBullet.TryGetComponent(out BulletMovement a))
            {
                a.damageOfWeapon = alienDamgeForLevel3;
            }


        }
        Instance = this;
        levelComplete = false;
        countShieldCompletion = 0;

        scorpianSpawnPoints.Add(Level1Sp1.gameObject);
        scorpianSpawnPoints.Add(Level1Sp2.gameObject);
        scorpianSpawnPoints.Add(Level1Sp3.gameObject);
        if(SceneManager.GetActiveScene().name == "Level5")
        {
            scorpianSpawnPoints.Add(Level5sp.gameObject);
        }
        // ShieldGrower.Instance.OnShieldPartActivation += CheckLevelCompeletion;
        StartCoroutine(Level1TimeDelayBetweenS_Swamps());
        LoadShields();


    }

    public void CheckLevelCompeletion()
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


        int randomScorpianSpwanTime = UnityEngine.Random.Range(2, delayBetweenEachSwamp);
        yield return new WaitForSeconds(randomScorpianSpwanTime);
        ProduceLevel1Scorpian();
    }



    void ProduceLevel1Scorpian()
    {


        HowManyScorpians = Random.Range(1, maxRangeOfScorpians);
        for (int i = 0; i <= HowManyScorpians; i++)
        {
            Instantiate(scorpian, new Vector3(Level1Sp1.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp1.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
            Instantiate(scorpian, new Vector3(Level1Sp2.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp2.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
         
     
            Instantiate(alien, new Vector3(Level1Sp3.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp3.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
            if (SceneManager.GetActiveScene().name == "Level5" || SceneManager.GetActiveScene().name == "Level6")
            {
                Instantiate(drone, new Vector3(Level5sp.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp3.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
            }
            if (SceneManager.GetActiveScene().name == "Level6")
            {
                Instantiate(drone, new Vector3(Level5sp.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp3.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
            }
        }
        scorpianSwampAttacks--;
        if (scorpianSwampAttacks > 0)
        {
            StartCoroutine(Level1TimeDelayBetweenS_Swamps());

        }

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

    }
}
