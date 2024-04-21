using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int HP;
    public GameManager GameManager;

    private void Start()
    {
        HP = 1;
    }

    private void Update()
    {
        if (HP <= 0)
        {
            GameManager.EndGame();
        };
    }
  }
