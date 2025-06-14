
using UdonSharp;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CheckArea : UdonSharpBehaviour
{
    public bool IsThroughThisObj { get; private set; } = false;


    private void Start()
    {
        IsThroughThisObj = false;
    }

    //このスクリプトがアタッチされたオブジェクト（＝青いCube）にプレイヤーが入った時の処理
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        IsThroughThisObj = true;

        //5秒後にIsThroughThisObjをfalseにする
        SendCustomEventDelayedSeconds(nameof(Reset), 5);
    }

    public void Reset()
    {
        IsThroughThisObj = false;
    }
}
