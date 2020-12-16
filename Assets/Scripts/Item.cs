using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int cost;
    public int level;
    public string item;

    public void buy()
    {
        switch(item)
        {
            case "light":
                if (PlayerBehaviour.money >= cost && PlayerBehaviour.lightlvl < level)
                {
                    PlayerBehaviour.money -= cost;
                    PlayerBehaviour.lightlvl++;
                    PlayerBehaviour.maxLightRadius *= 1.5f;
                }
                break;

            case "oil":
                if (PlayerBehaviour.money >= cost && PlayerBehaviour.oillvl < level)
                {
                    PlayerBehaviour.money -= cost;
                    PlayerBehaviour.oillvl++;
                }
                break;

            case "life":
                if (PlayerBehaviour.money >= cost)
                {
                    switch (PlayerBehaviour.maxLife)
                    {
                        case 3:
                            PlayerBehaviour.maxLife = 6;
                            PlayerBehaviour.life = 6;
                            break;

                        case 5:
                            PlayerBehaviour.maxLife = 10;
                            PlayerBehaviour.life = 10;
                            break;
                    }
                    PlayerBehaviour.money -= cost;
                }
                break;
        }
    }
}
