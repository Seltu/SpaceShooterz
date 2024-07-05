using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;

[System.Serializable]
public class Round
{
    public List<Wave> waves;
    public float duration;

    public Round(List<Wave> list)
    {
        waves = list;
    }

    public void AddWave(Wave wave)
    {
        waves.Add(wave);
    }
}
public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Round> rounds;
    [SerializeField] private List<EnemyLine> enemyLines;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private string nextLevel;
    [SerializeField] private LevelInfoSO infoSO;
    
    // [SerializeField] private List<Pickup> pickupPrefabs;
    [SerializeField] private BossEnemyAi boss;

    // public MusicController audioController;

    private void OnEnable()
    {
        //GameEventsManager.summonRound += o => AddRound(o);
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        infoSO.players = new List<PlayerController>();
        foreach(PlayerController player in infoSO.players)
        {
            player.OnDeath.AddListener(PlayerDied);
        }
        StartCoroutine(AddRounds());
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }

    private IEnumerator AddRounds()
    {
        foreach (var round in rounds)
        {
            AddRound(round);
            yield return new WaitForSeconds(round.duration);
        }
        Instantiate(boss);
    }

    private void AddRound(Round round)
    {
        foreach (var wave in round.waves)
        {
            StartCoroutine(AddWave(wave));
        }
    }

    private IEnumerator AddWave(Wave wave)
    {
        for (var i=0; i < wave.number; i++)
        {
            yield return new WaitForSeconds(2f / wave.number);
            MakeEnemy(wave, i);
            yield return new WaitForSeconds(2f / wave.number);
        }
    }

    private void MakeEnemy(Wave wave, float delay)
    {
        var enemy = (CurveEnemyAi) PoolManager.Instance.ReuseComponent(enemyPrefabs[(int)wave.enemy].gameObject, transform.position, Quaternion.identity);
        enemy.gameObject.SetActive(true);
        enemy.OnSetMovement.Invoke(enemyLines[wave.curve], wave.offset, wave.layer, wave.speedMultiplier);
        enemy.SetDelay(delay);
        enemy.OnDeath.AddListener(SpawnPickup);
    }

    private void SpawnPickup(Vector2 pos)
    {
        /*if(Random.Range(1, 10)==1)
            Instantiate(pickupPrefabs[Random.Range(0, pickupPrefabs.Count)], pos, quaternion.identity);
        */
    }

    private void PlayerDied()
    {
        if (infoSO.players.Count <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
