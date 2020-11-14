using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

using Hashtable = ExitGames.Client.Photon.Hashtable;

public class CanvasCharacter : MonoBehaviour, IPointerClickHandler
{
    public int id = 0;
    public bool isChosen;
    public string playerOwner = "";
    public int playerOwnerID = 0;


    private void Start() {
        isChosen = true;
    }

    public void SetID(int value) {
        id = value;
    }

    public void OnPointerClick(PointerEventData eventData) {

        Debug.Log(id);

        if (!isChosen) {
            return;
        }

        Hashtable props = new Hashtable {
            { "PERSONAGEM", id }
        };

        PhotonNetwork.LocalPlayer.SetCustomProperties(props);

    }


    public void SelectCharacter(string p_playerName, int p_playerId) {


        Transform allButtons = this.transform.parent;

        foreach (Transform item in allButtons) {

            if (item.GetComponent<CanvasCharacter>().playerOwnerID == p_playerId) {
                item.GetComponent<CanvasCharacter>().ResetCharacter();
            }

        }


        isChosen = false;
        playerOwner = p_playerName;
        playerOwnerID = p_playerId;
        GetComponentInChildren<Text>().text = p_playerName;//Texto
        GetComponentInChildren<Text>().color = ColorPlayer(p_playerId);//Cor
        GetComponent<Button>().interactable = false;

    }

    public void ResetCharacter() {
        
        isChosen = true;
        playerOwner = "";
        playerOwnerID = 0;
        GetComponentInChildren<Text>().text = "";//Texto
        GetComponentInChildren<Text>().color = ColorPlayer(0);//Cor
        GetComponent<Button>().interactable = true;
    }

    Color ColorPlayer(int playerID) {

        Color colorReturn;

        switch (playerID) {
            case 1:
                colorReturn = new Color(255, 0, 0); //Red
                break;
            case 2:
                colorReturn = new Color(0, 0, 255); //Blue
                break;
            case 3:
                colorReturn = new Color(255, 255, 0); //Yellow
                break;
            case 4:
                colorReturn = new Color(255, 0, 255); //Pink
                break;
            case 5:
                colorReturn = new Color(255, 213, 0); //Brown
                break;
            case 6:
                colorReturn = new Color(255, 213, 0); //Orange
                break;

            default:
                colorReturn = new Color(255, 255, 255); //White
                break;
        }

        return colorReturn;
    }


}
