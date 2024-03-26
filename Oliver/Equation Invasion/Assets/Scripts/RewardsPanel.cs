using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardsPanel : MonoBehaviour
{
    public QuizManager quiz;
    public GameObject rewardsPanel;
    public GameObject collectible2;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (quiz.rewardspanel)
        {
            rewardsPanel.SetActive(true);
        }
    }

    public void secondOption()
    {
        float SpacingY = 5.0f;

        GameObject powerup = Instantiate<GameObject>(collectible2);

        powerup.transform.position = player.transform.position +
            player.transform.forward * SpacingY;
        rewardsPanel.SetActive(false);
    }
}
