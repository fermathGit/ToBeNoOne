using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Touch : MonoBehaviour {

    public Text txt;
    float distance = 0;//触控缩放的距离
    private float lastDist = 0;//用于计算触控缩放
    private float curDist = 0;//用于计算触控缩放
    int t;//判断缩放触控

    private Vector3 pos1;

    private Quaternion pos2;

    private void Start() {
        Input.simulateMouseWithTouches=true;

        pos1 = this.gameObject.transform.localScale;
        pos2 = this.gameObject.transform.rotation;
    }


    // Update is called once per frame
    void Update() {




        //单点触摸， 水平上下旋转  
        if ( 1 == Input.touchCount ) {

            var touch = Input.GetTouch( 0 );
            ShowTxt( "touch: " + touch.position );
            if ( touch.position.y > Screen.height * 0.25f &&
                touch.position.y < Screen.height * 0.93f ) {
                Vector2 deltaPos = touch.deltaPosition;
                transform.Rotate( Vector3.down * deltaPos.x * 0.05f, Space.World );
                transform.Rotate( Vector3.right * deltaPos.y * 0.05f, Space.World );
            }

        }

        //两点以上触控，且触控点发生移动
        if ( ( Input.touchCount > 1 ) && ( Input.GetTouch( 0 ).phase == TouchPhase.Moved
                                       || Input.GetTouch( 1 ).phase == TouchPhase.Moved ) ) {
            var touch1 = Input.GetTouch( 0 ); //第一根手指
            var touch2 = Input.GetTouch( 1 ); //第二根手指
            ShowTxt( "touch1: " + touch1.position );
            ShowTxt( "touch2: " + touch2.position );
            if ( touch1.position.y > Screen.height * 0.25f &&
                touch1.position.y < Screen.height * 0.93f &&
                touch2.position.y > Screen.height * 0.25f &&
                touch2.position.y < Screen.height * 0.93f ) {
                curDist = Vector2.Distance( touch1.position, touch2.position );//两指间距
                //当手指移动时，重置起始距离为当前距离
                if ( t == 0 ) {
                    lastDist = curDist;
                    t = 1;
                }
                distance = curDist - lastDist;
                if ( this.gameObject.transform.localScale.x >= 1 &&
                        this.gameObject.transform.localScale.x < this.gameObject.transform.localScale.x * 20 ) {
                    this.gameObject.transform.localScale += Vector3.one * distance * 0.05f * Time.deltaTime;
                } else {
                    this.gameObject.transform.localScale = new Vector3( 1, 1, 1 );
                }
            }




            lastDist = curDist;
        }

        //没有触控事件
        if ( Input.touchCount == 0 )
            t = 0;
    }

    private void ShowTxt( string info ) {
        txt.text = info + "\r\n" + txt.text;
    }
}