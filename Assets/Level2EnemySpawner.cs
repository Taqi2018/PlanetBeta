using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Level2EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemiesOnField;
    public static Level2EnemySpawner Instance;
    [SerializeField] public int scorpianSwampAttacks;//2
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
   public  bool isNotLock;
    private float boostShiledTime;
    public bool isShieldBooster;
    private bool isOk;
    private int isCounter;

    /* p alienSpawnEffect;*/

    // Start is called before the first frame update
    void Start()
    {
        isCounter = 0;
        isOk = true;
        isNotLock = true;
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
       
        LoadShields();

        StartCoroutine(LevelCompeletionForced());
    }

    private IEnumerator LevelCompeletionForced()
    {
        yield return new WaitForSeconds(5.0f);
        if (scorpianSwampAttacks <= 0 && enemiesOnField.Count == 0 )
        {
            GameplayUIManager.Instance.LevelCompletePanel();
        }
        StartCoroutine(LevelCompeletionForced());
    }

    public void CheckLevelCompeletion()
    {

        countShieldCompletion++;
        Debug.Log("Total shield complete = " + countShieldCompletion);

        if (countShieldCompletion == ships.Length)
        {
            if (scorpianSwampAttacks <= 0 && enemiesOnField.Count == 0)
            {
                levelComplete = true;
            }
            if (levelComplete)
            {

                levelComplete = false;
                StartCoroutine(LevelCompDelay());
      


                /*
                                Debug.Log(countShieldCompletion + " Total shield COmplete " + ships.Length + "Ships total ");
                                Debug.Log("LevelComplete");

                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);*/
            }
        }


    }

    private IEnumerator LevelCompDelay()
    {
        yield return new WaitForSeconds(2.0f);
        GameplayUIManager.Instance.LevelCompletePanel();
    }

    private void LoadShields()
    {
        ships = GameObject.FindGameObjectsWithTag("zPoint");


    }




    IEnumerator Level1TimeDelayBetweenS_Swamps()
    {


        int randomScorpianSpwanTime = delayBetweenEachSwamp;

        if (enemiesOnField.Count == 0)
        {
            yield return new WaitForSeconds(randomScorpianSpwanTime);
            ProduceLevel1Scorpian();


        }
     
 
    }

    public void checkNoEnemyLeftToSpawnNewWave()
    {
        if (enemiesOnField.Count == 0)
        {
            isNotLock = true;
        }
    }



    void ProduceLevel1Scorpian()
    {


        HowManyScorpians = Random.Range(1, maxRangeOfScorpians);
        for (int i = 0; i <= HowManyScorpians; i++)
        {
            Debug.Log(i + " No ");
            GameObject e=  Instantiate(scorpian, new Vector3(Level1Sp1.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp1.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
            enemiesOnField.Add(e);
         
            GameObject f=Instantiate(scorpian, new Vector3(Level1Sp2.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp2.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
            enemiesOnField.Add(f);



            GameObject z = Instantiate(alien, new Vector3(Level1Sp3.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp3.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
            enemiesOnField.Add(z);
            if (SceneManager.GetActiveScene().name == "Level5" || SceneManager.GetActiveScene().name == "Level6")
            {
                GameObject g=  Instantiate(drone, new Vector3(Level5sp.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp3.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
             
                enemiesOnField.Add(g);
            }
        /*    if (SceneManager.GetActiveScene().name == "Level6")
            {
                GameObject h= Instantiate(drone, new Vector3(Level5sp.transform.position.x + VariationInSpawnPosition(), 5, Level1Sp3.transform.position.z + VariationInSpawnPosition()), Quaternion.identity);
                enemiesOnField.Add(h);
            }*/
        }
        scorpianSwampAttacks--;



    }


    private float VariationInSpawnPosition()
    {
        float variationFactor = UnityEngine.Random.Range(-5, +5);
        return variationFactor;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesOnField.Count == 0 & scorpianSwampAttacks > 0 & isNotLock)
        {
            isNotLock = false;
            StartCoroutine(Level1TimeDelayBetweenS_Swamps());
        }

        if (scorpianSwampAttacks <= 0 && enemiesOnField.Count == 0 &isCounter < ships.Length)
        {
            isCounter++;
         
            CheckLevelCompeletion();
        }
   


      

    }


}
