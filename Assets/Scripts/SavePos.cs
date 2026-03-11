using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePos : MonoBehaviour
{

    void Start(){
        LoadPlayerPos();
    }


     void OnApplicationQuit(){
        SavePlayerPos();
    }
    void SavePlayerPos(){
        Vector3 PlayerPos = transform.position;
        PlayerPrefs.SetFloat("PlayerX", PlayerPos.x);
        PlayerPrefs.SetFloat("PlayerY", PlayerPos.y);
        PlayerPrefs.SetFloat("PlayerZ", PlayerPos.z);
        PlayerPrefs.Save();


    }
    void LoadPlayerPos(){
            if(PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ")){
                float x = PlayerPrefs.GetFloat("PlayerX");
                float y = PlayerPrefs.GetFloat("PlayerY");
                float z = PlayerPrefs.GetFloat("PlayerZ");

                transform.position = new Vector3(x, y, z);

            }
        }
}
