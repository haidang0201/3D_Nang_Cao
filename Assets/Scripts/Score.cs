using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public int score = 0;
    public TextMeshProUGUI FramerateCounter;

    void Update(){
        FramerateCounter.text = score.ToString();

        if(Input.GetKeyDown(KeyCode.Space)){
            score ++;
        }

        if(Input.GetKeyDown(KeyCode.S)){
            SaveScore();
        }
         if(Input.GetKeyDown(KeyCode.L)){
            LoadScore();
        }
        if(Input.GetKeyDown(KeyCode.X)){
            DeleteScore();
        }
    }

     public void SaveScore(){
        PlayerPrefs.SetInt("Score", score);
        Debug.Log("da luu du lieu");
        PlayerPrefs.Save();
    }

     public void LoadScore()
    {
        if(PlayerPrefs.HasKey("Score")){
            score = PlayerPrefs.GetInt("Score");
            Debug.Log("du lieu da luu" +"" +score);

        }

    }

    public void DeleteScore(){
        PlayerPrefs.DeleteAll();
        Debug.Log("da xoa het du lieu");
    }

    private void OnCollisionEnter(Collision other) {
    if(other.gameObject.CompareTag("coin")){
        score += 10;
        Destroy(other.gameObject);
        Debug.Log("Score: " + score);

    }
    }

   
}
