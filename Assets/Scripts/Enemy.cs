using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject ShipdestroyingVFX;
    [SerializeField] int hitPoint = 3;
    [SerializeField] int scoreAmount = 50;
    ScoreBoard scoreBoard;
    void Start()
    {
        scoreBoard = FindAnyObjectByType<ScoreBoard>();
    }
    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        hitPoint -= 1;
        if (hitPoint <= 0)
        {
            scoreBoard.SetScore(scoreAmount);
            Instantiate(ShipdestroyingVFX, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
