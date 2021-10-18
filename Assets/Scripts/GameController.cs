using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
  // 画面上に配置する Light のプレハブ
  public GameObject LightPrefab;
  
  // 「CLEAR!」と表示するためのテキスト
  public Text ClearText;
  
  // 問題を作成する際に一時的に使用する、Light のリスト
  private List<GameObject> lightList = new List<GameObject>();
  
  // 「New Game」ボタン押下時 Game シーンに移動する
  public void NewGame() {
    SceneManager.LoadScene("Game");
  }
  
  // Light をタップした時に、消灯している Light の数を計算する
  public void AnswerCheck() {
    // LightOnOff の isOff の合計値を計算するための変数
    int count = 0;
    
    // ゲーム上の LightOnOff スクリプトを探す配列
    LightOnOff[] allLights = FindObjectsOfType<LightOnOff>();
    foreach(var item in allLights) {
      count += item.isOff;
    }
    
    // 全ての Light が消えているのでクリアとする
    if(count == 0) {
      ClearText.text = "CLEAR!";
      // 全ての Light を「クリア」状態に変更し、タップできないようにする
      foreach(var item in allLights) {
        item.isClear = true;
      }
    }
    
    // デバッグ用に Console ビューに count 値を出力する
    Debug.Log(count);
  }
  
  // 問題を作成する
  void MakeQuestion() {
    int r = Random.Range(2, 9);  // 2～8 の値をランダムに作る
    
    // r の数だけ、点灯している Light を生成する
    for(int i = 0; i < r; i++) {
      GameObject OnLight = Instantiate(LightPrefab);
      OnLight.GetComponent<LightOnOff>().isOff = 1;  // 点灯させる
      OnLight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0);
      lightList.Add(OnLight);
    }
    
    // 9 - r の数だけ、消えている Light を生成する
    for(int i = 0; i < (9 - r); i++) {
      GameObject OffLight = Instantiate(LightPrefab);
      OffLight.GetComponent<LightOnOff>().isOff = 0;  // 消灯させる
      OffLight.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
      lightList.Add(OffLight);
    }
    
    // 9つの Light が追加されているリストをシャッフルする
    for(int i = 0; i < 9; i++) {
      int x = Random.Range(0, 9);
      GameObject temp = lightList[x];
      lightList[x] = lightList[i];
      lightList[i] = temp;
    }
    
    // 3×3 で並べる (リストは空になる)
    for(int i = 0; i < 3; i++) {
      for(int j = 0; j < 3; j++) {
        lightList[0].transform.position = new Vector2(i, j);
        lightList.RemoveAt(0);
      }
    }
  }
  
  // Start is called before the first frame update
  void Start() {
    ClearText.text = ""; // テキストを空にする
    MakeQuestion();
  }
  
  // Update is called once per frame
  void Update() {
    // ...
  }
}
