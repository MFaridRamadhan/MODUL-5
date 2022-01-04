using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static int playerHp = 100;
    public TextMeshProUGUI playerHPText;

    public static bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        playerHp = 100;
    }

    // Update is called once per frame
    void Update()
    {
        playerHPText.text = "+" + playerHp;
        if (isGameOver)
        {
            SceneManager.LoadScene("Game");
        }
    }

    public IEnumerator TakeDamage(int damageAmount)
    {
        playerHp -= damageAmount;
        if (playerHp <= 0)
            isGameOver = true;

        yield return new WaitForSeconds(1.5f);

    }
}
