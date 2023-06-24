using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviour
{
    public static PlayerScript Instance;
    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    public float playerLevelProgress;
    public int playerLevel = 1;
    float[] levelRequirementRef = { 0, 50, 100 };
    public float playerHealth = 100;
    public int maxHealth = 100;
    int[] maxHealthRef = { 100, 120, 150 };
    [SerializeField] Slider levelBar;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI lifeText;
    public void addPlayerLevel(float amount)
    {
        if (playerLevel < 3)
        {
            playerLevelProgress += amount;
            if (playerLevelProgress / levelRequirementRef[playerLevel] >= 1)
            {
                //level up
                playerLevelProgress -= levelRequirementRef[playerLevel];
                maxHealth = maxHealthRef[playerLevel];
                changePlayerHealth(30);
                playerLevel++;
                //levelText.text = playerLevel < 3 ? playerLevel.ToString() : "MAX";
                levelText.text = playerLevel.ToString();
            }
            if (playerLevel != 3)
            {
                levelBar.value = playerLevelProgress / (50 * playerLevel);
            }
            else
            {
                levelBar.value = 0;
            }
        }
    }
    public void changePlayerHealth(float amount)
    {
        playerHealth = Mathf.Clamp(playerHealth + amount, 0, maxHealth);
        healthBar.value = playerHealth / maxHealth;
        lifeText.text = playerHealth.ToString() + " / " + maxHealth.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(playerHealthTickDown());
        StartCoroutine(playerLevelTickUp());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator playerHealthTickDown()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            changePlayerHealth(-0.5f);
        }
    }
    IEnumerator playerLevelTickUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            addPlayerLevel(0.1f);
        }
    }
}
