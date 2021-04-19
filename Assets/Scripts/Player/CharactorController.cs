using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorController : MonoBehaviour
{
    /* 操作方法
    
        移動      - 十字キー左右
        ジャンプ  - SPACE
    */

    // パラメータ
    public float _Speed     = 5.0f;     // キャラクターの移動速度
    public float _JumpSpeed = 10.0f;    // ジャンプ力
    public float _Gravity   = 20.0f;    // 重力の大きさ

    private CharacterController _Controller;        // コンポーネントの取得
    private Vector3 _MoveDirection = Vector3.zero;  // キャラクターの移動量
    private float h;                                // キー入力取得用



    // Start is called before the first frame update
    void Start()
    {
        // CharactorControllerのコンポーネントを取得
        _Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // キー入力取得
        h = Input.GetAxis("Horizontal");    // 値の範囲(-1.0f~1.0f)

        // キャラクターの移動
        if(_Controller.isGrounded)// キャラクターが地面についているとき
        {
            _MoveDirection = new Vector3(h, 0.0f, 0.0f);                    // キー入力でx成分のみ移動量に加える
            _MoveDirection = transform.TransformDirection(_MoveDirection);  // キャラクターの移動に慣性をかける
            _MoveDirection *= _Speed;                                       // キャラクターの設定スピードを乗算

            // ジャンプ
            if(Input.GetKey(KeyCode.Space))
            {
                _MoveDirection.y = _JumpSpeed;  // y成分にキャラクターのジャンプ力を加算
            }
        }

        // 重力設定
        _MoveDirection.y -= _Gravity * Time.deltaTime;
        _Controller.Move(_MoveDirection * Time.deltaTime);

    }//Update
}
