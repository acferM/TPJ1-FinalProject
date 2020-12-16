using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Battlehub.Dispatcher;

public class StoreManager : MonoBehaviour
{
    float positionY = 165.5f;

    [SerializeField] GameObject loja;
    [SerializeField] Transform moneyCost;
    [SerializeField] Text moneyCostTxt;

    bool openable;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycasts = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycasts);

            for (int i = 0; i < raycasts.Count; i++)
            {
                if (raycasts[i].gameObject.name.StartsWith("Light") || raycasts[i].gameObject.name.StartsWith("Oil") || raycasts[i].gameObject.name.StartsWith("Hearts"))
                {
                    var cost = raycasts[i].gameObject.GetComponent<Item>().cost;
                    Dispatcher.Current.BeginInvoke(() => {
                        moneyCostTxt.color = new Color(255, 255, 255, 255);
                        moneyCostTxt.text = "Cost: " + cost;
                        moneyCost.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
                    });
                    positionY = raycasts[i].gameObject.transform.position.y;

                    if (Input.GetMouseButtonDown(0))
                    {
                        switch(raycasts[i].gameObject.GetComponent<Item>().item)
                        {
                            case "light":
                                if (PlayerBehaviour.lightlvl < int.Parse(raycasts[i].gameObject.name.Split('_')[1]))
                                {
                                    raycasts[i].gameObject.GetComponent<Item>().buy();
                                }
                                break;

                            case "oil":
                                if (PlayerBehaviour.oillvl < int.Parse(raycasts[i].gameObject.name.Split('_')[1]))
                                {
                                    raycasts[i].gameObject.GetComponent<Item>().buy();
                                }
                                break;

                            case "life":
                                raycasts[i].gameObject.GetComponent<Item>().buy();
                                break;
                        }
                    }
                }
            }
        }
        else
        {
            moneyCostTxt.color = new Color(255, 255, 255, 0);
            moneyCost.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
        moneyCost.position = new Vector3(moneyCost.position.x, positionY, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            loja.SetActive(true);
        }

        if (!openable)
        {
            loja.SetActive(false);
        }
    }

    public void setOpanebale(bool value)
    {
        openable = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            openable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            openable = false;
        }
    }
}
