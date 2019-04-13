using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ProfessorSquish.Characters.Enemies
{
    class EnemyManager : MonoBehaviour
    {
        public List<GameObject> enemies = new List<GameObject>();
        public List<GameObject> spawnPoints = new List<GameObject>();
        public GameObject difficultySlider;
        public float difficultyModifier = 0f;
        private List<GameObject> spawnedEnemies = new List<GameObject>();
        private float spawnTime = 3f;
        private int maximumEnemiesSpawned = 50;

       public void onDifficultyChange(Slider slider)
        {
           
            difficultyModifier = slider.value;
            maximumEnemiesSpawned = (int)(maximumEnemiesSpawned * (difficultyModifier / 2));
        }
        // Start is called before the first frame update
        void Start()
        {
            difficultySlider = GameObject.FindWithTag("DifficultySlider");
           difficultyModifier = difficultySlider.GetComponent<DifficultySlider>().currentDiff;
            maximumEnemiesSpawned = (int)(maximumEnemiesSpawned * (difficultyModifier / 2));
            spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint").ToList();
            InvokeRepeating("Spawn", (2/ difficultyModifier) * spawnTime, (2/ difficultyModifier) * spawnTime);
        }

        // Update is called once per frame
        void Update()
        {
            var slider = GameObject.FindWithTag("DifficultySlider");
            if (slider!=null)
            {
                this.difficultyModifier = slider.GetComponent<DifficultySlider>().currentDiff;
            }
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
