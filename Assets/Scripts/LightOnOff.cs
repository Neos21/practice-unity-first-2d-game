using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnOff : MonoBehaviour {
  // 0 ならライトが消えている・1 なら点いているとする
  public int isOff;
  
  // クリアしたかどうか
  public bool isClear;
  
  // GameController スクリプトを動かすため定義する
  private GameController gameControllerCS;
  
  // ライトがタップされた時の処理
  public void TurnOnLight() {
    // ライトが点いていたら消す
    if(isOff == 1) {
      isOff = 0;
      GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);  // 灰色
    }
    // ライトが消えていたら点ける
    else if(isOff == 0) {
      isOff = 1;
      GetComponent<SpriteRenderer>().color = new Color(1, 1, 0);  // 黄色
    }
  }
  
  // この関数を、Collider を持つオブジェクトに付けておくと、タップされた時に働く関数になる
  private void OnMouseDown() {
    if(isClear) {
      return;  // クリアしていればタップしても何もしない
    }
    
    TurnOnLight();
    Ray();
    gameControllerCS.AnswerCheck();
  }
  
  // タップされた Light オブジェクトから上下左右に Ray (レーザー) を飛ばし、その先にある Light オブジェクトの TurnOnLight() 関数を実行させる
  // コレにより、タップされた周囲の Light オブジェクトが反転する動きになる
  void Ray() {
    // 上のライト
    RaycastHit2D hitUp = Physics2D.Raycast(transform.position + Vector3.up, Vector2.up, 0.1f);
    if(hitUp.collider != null) {
      hitUp.collider.GetComponent<LightOnOff>().TurnOnLight();
    }
    
    // 下のライト
    RaycastHit2D hitDown = Physics2D.Raycast(transform.position + Vector3.down, Vector2.down, 0.1f);
    if(hitDown.collider != null) {
      hitDown.collider.GetComponent<LightOnOff>().TurnOnLight();
    }
    
    // 右のライト
    RaycastHit2D hitRight = Physics2D.Raycast(transform.position + Vector3.right, Vector2.right, 0.1f);
    if(hitRight.collider != null) {
      hitRight.collider.GetComponent<LightOnOff>().TurnOnLight();
    }
    
    // 左のライト
    RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + Vector3.left, Vector2.left, 0.1f);
    if(hitLeft.collider != null) {
      hitLeft.collider.GetComponent<LightOnOff>().TurnOnLight();
    }
  }
  
  
  // Start is called before the first frame update
  private void Start() {
    gameControllerCS = FindObjectOfType<GameController>();
  }
  
  // Update is called once per frame
  void Update() {
    // ...
  }
}
