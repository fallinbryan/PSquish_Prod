using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ProfessorSquish.Characters.Enemies
{
    class EnemyManager : MonoBehaviour
    {
        public List<GameObject> enemies = new List<GameObject>();
        public List<GameObject> spawnPoints = new List<GameObject>();

        private List<GameObject> spawnedEnemies = new List<GameObject>();
        private float spawnTime = 3f;
        private readonly int maximumEnemiesSpawned = 50;

        // Start is called before the first frame update
        void Start()
        {
            spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();
            InvokeRepeating("Spawn", spawnTime, spawnTime);
        }

        // Update is called once per frame
        void Update()
        {
            //Remove enemies that are null
            spawnedEnemies.RemoveAll(f => (f == null) || f.Equals(null));
           
        }
        void Spawn()
        {
            if (spawnedEnemies.Count >= maximumEnemiesSpawned)
            {
                return;
            }
            int spawnPointIndex = Random.Range(0, spawnPoints.Count);
            int enemyIndex = Random.Range(0, enemies.Count);
            GameObject newEnemy = Instantiate(enemies[enemyIndex], spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation);


        }
    }
}
