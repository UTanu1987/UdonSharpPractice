
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UmiTanuki.UdonPractice
{
    public class LocalSwitchBlackOut : UdonSharpBehaviour
    {
        //暗転→明転を行うAnimator
        [SerializeField]
        private Animator m_animator_BlackOut = null;
        private bool m_isBlackOut = false;

        //暗転アニメーションの所要時間
        [SerializeField]
        private float m_timeInterval = 2.0f;

        //テレポート先
        [SerializeField]
        private Transform m_destination = null;

        public override void Interact()
        {
            //暗転アニメーション起動
            ToggleAnimation();

            //暗転アニメーション終了タイミングで発火させる
            //SendCustomEvent系列の操作はpublic関数しか呼び出せない
            SendCustomEventDelayedSeconds(nameof(TeleportPlayer), m_timeInterval);
        }

        /// <summary>
        /// 暗転ギミックのアニメーターのトグル操作
        /// </summary>
        private void ToggleAnimation()
        {
            m_isBlackOut = !m_isBlackOut;
            
            m_animator_BlackOut.SetBool("isBlackOut", m_isBlackOut);
        }

        /// <summary>
        /// プレイヤーをテレポートさせて明転アニメーションさせる
        /// SendCustomEventで呼び出すためにpublicで宣言
        /// </summary>
        public void TeleportPlayer()
        {
            var player = Networking.LocalPlayer;

            if (player == null) return;

            if (m_destination == null) return;

            player.TeleportTo(m_destination.position, m_destination.rotation);

            ToggleAnimation();
        }
    }
}
