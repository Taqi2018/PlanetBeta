using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class changePosition : MonoBehaviour
{
    public static changePosition Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }
    public IEnumerator UpdatePosition()
    {
        yield return new WaitForSeconds(0.0f);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + VariationInSpawnPosition());

    }
    private float VariationInSpawnPosition()
    {
        float variationFactor = UnityEngine.Random.Range(-10, +10);
        return variationFactor;

    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (Level1EnemySpawner.Instance.isNotLock)
            {
                StartCoroutine(UpdatePosition());
            }
            
        }
        else
        {
            if (Level2EnemySpawner.Instance.isNotLock)
            {
                StartCoroutine(UpdatePosition());
            }

        }

    }
}
