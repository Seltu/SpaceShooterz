using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private List<PlayerController> players;
    [SerializeField] private List<EnemyLine> enemyLines;
    [SerializeField] private List<CurveEnemyAi> enemyPrefabs;
    [SerializeField] private string nextLevel;
    [SerializeField] private LevelInfoSO infoSO;
    
    // [SerializeField] private List<Pickup> pickupPrefabs;
    // [SerializeField] private BossEnemy _boss;
    public int progress;
    private float _levelTimer = 1;
    private bool _bossFight;
    private bool _makeBoss;

    // public MusicController audioController;

    private void Start()
    {
        infoSO.players = new List<PlayerController>();
        for (var index = 0; index < players.Count; index++)
        {
            var player = players[index];
            infoSO.players.Add(player);
            player.OnDeath.AddListener(PlayerDied);
            players.Remove(player);
        }
    }

    private void Update() {
        if (_levelTimer > 0) {
            _levelTimer-=Time.deltaTime;
            return;
        }
        if (_bossFight) {
            /*if (level == 0 && !bossChannel.isPlaying) {
                bossChannel.clip = vsBaronMusic;
                bossChannel.Play();
            }
            if (level == 1 && !bossChannel.isPlaying) {
                bossChannel.clip = vsJesterMusic;
                bossChannel.Play();
            }
            if (level == 2 && !bossChannel.isPlaying) {
                bossChannel.clip = vsMonarchMusic;
                bossChannel.Play();
            }
            */
            /*
            if (_boss.summon) {
                StartCoroutine(AddRound(_boss.CreateWaves()));
            }
            return;
            */
        }
        /*if (level > 2) {
            done = true;
            next_state = "WIN";
            gameLevel = 0;
            on_boss = false;
            return;
        }*/
        if (progress >= rounds.Count) {
            /*
            if (_makeBoss)
            {
                _boss = Instantiate(levels[level].boss);
                _boss.onDefeat.AddListener(NextLevel);
                _bossFight = true;
            }
            else
            {
                _levelTimer = 8f;
                audioController.PlayWarning();
            }
            _makeBoss = true;
            */
            return;
        }
        var currentRound = rounds[progress];
        StartCoroutine(AddRound(currentRound));
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
    
    private IEnumerator AddRound(Round currentRound)
    {
        _levelTimer = currentRound.duration;
        foreach (var wave in currentRound.waves)
        {
            StartCoroutine(AddWave(wave));
        }
        yield return new WaitForSeconds(8f);
        progress += 1;
        if (_bossFight)
        {
            //_boss.summon = false;
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
        var enemy = Instantiate(enemyPrefabs[((int)wave.enemy)]);
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
        if (players.Count <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
