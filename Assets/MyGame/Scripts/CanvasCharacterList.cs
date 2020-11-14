using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;



public class CanvasCharacterList : MonoBehaviourPunCallbacks
{

    private void Start() {

        SetCharacterID();

    }

    void SetCharacterID() {
        int i = 1;

        foreach (Transform item in transform) {

            item.GetComponent<CanvasCharacter>().SetID(i);
            i++;
        }

    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps) {
        Debug.Log("OnPlayerPropertiesUpdate");

        object characterID_Obj;

        if (changedProps.TryGetValue("PERSONAGEM", out characterID_Obj)) {

            int characterID = (int) characterID_Obj;

            if (characterID > 0) {

                foreach (Transform item in transform) {

                    if (item.GetComponent<CanvasCharacter>().id == characterID) {
                        item.GetComponent<CanvasCharacter>().SelectCharacter(targetPlayer.NickName, targetPlayer.ActorNumber);
                    }
                }

            }

        }

    }

}
