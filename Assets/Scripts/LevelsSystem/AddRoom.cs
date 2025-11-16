using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Cysharp.Threading.Tasks;
using System; 

public class AddRoom : MonoBehaviour
{
   [Header("Walls")]
   public GameObject[] walls;
   //public GameObject door;

   [Header("Enemies")]
   public GameObject[] enemyTypes;
   public Transform[] enemySpawners;

  
   //private RoomVariants variants;
  [HideInInspector] public List<GameObject> enemies;
   private bool wallsDestroyed;
    private bool spawned;
   private void Start(){
    //variants = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomVariants>();
    
   }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !spawned)
        {
            spawned = true;

            foreach (Transform spawner in enemySpawners)
            {
                GameObject enemyType = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Length)];
                GameObject enemy = Instantiate(enemyType, spawner.position, Quaternion.identity) as GameObject;
                //enemy.transform.parent = transform;
                enemies.Add(enemy);
            }
            CheckEnemies().Forget();
            //Debug.Log("Destroy Walls");
        }
    }

    private async UniTask CheckEnemies()
    {
        await UniTask.Delay(1000);
        if (enemies.Count == 0)
        {
            DestroyWalls();
            return;
        }
        else
        {
            CheckEnemies();

        }
    }
   /* IEnumerator CheckEnemies(){
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => enemies.Count == 0);
        DestroyWalls();
       
    }*/
    public void DestroyWalls(){
        //Debug.Log("void Destroy Walls");
        foreach (GameObject wall in walls)
        {
            Destroy(wall);
        }

        wallsDestroyed = true;
        //Destroy(gameObject);
        
    }

   private void OnTriggerStay2D(Collider2D other) {
        if (wallsDestroyed && other.CompareTag("Wall")){
            Destroy(other.gameObject);
        }
    }
   
}
