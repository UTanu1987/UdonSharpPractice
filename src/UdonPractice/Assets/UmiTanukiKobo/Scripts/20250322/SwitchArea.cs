
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class SwitchArea : UdonSharpBehaviour
{
    [SerializeField]
    private GameObject m_inAreaObj = null;
    private bool m_isInArea = false;

    [SerializeField]
    CheckArea m_checkArea = null;


    private void Start()
    {
        m_isInArea = false;
        ToggleActiveObj(m_isInArea);
    }

    private void ToggleActiveObj(bool flag)
    {
        //プレイヤーが青いCubeを通過していたら
        if (m_checkArea.IsThroughThisObj)
        {
            m_inAreaObj.SetActive(flag);
        }
        //プレイヤーが青いCubeを通過していなかったら
        else
        {
            m_inAreaObj.SetActive(false);
        }
    }

    //このスクリプトがアタッチされたオブジェクト（＝赤いCube）にプレイヤーが入った時の処理
    public override void OnPlayerTriggerEnter(VRCPlayerApi player)
    {
        m_isInArea = true;
        ToggleActiveObj(m_isInArea);
        //5秒後にIsThroughThisObjをfalseにする
        SendCustomEventDelayedSeconds(nameof(Reset), 5);
    }

    public void Reset()
    {
        m_isInArea = false;
    }
}
