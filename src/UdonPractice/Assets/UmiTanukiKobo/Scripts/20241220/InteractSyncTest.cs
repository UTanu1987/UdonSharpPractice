
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace UmiTanuki.UdonPractice
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class InteractSyncTest : UdonSharpBehaviour
    {
        //出現させるCube
        [SerializeField]
        private GameObject m_cube = null;

        #region SyncVariable
        //出現切り替えフラグ(同期変数として宣言)
        [UdonSynced(UdonSyncMode.None), FieldChangeCallback(nameof(IsCubeActive))]
        private bool m_isCubeActive = false;
        public bool IsCubeActive 
        {
            get => m_isCubeActive;
            set
            {
                //値が変化したときに行う処理
                m_isCubeActive = value;
                ToggleCubeActive();
            }
        }
        #endregion

        void Start()
        {
            //nullチェック入れないと「Udon runtime exception detected!」エラーが出る
            if (m_cube != null)
            {
                m_cube.SetActive(IsCubeActive);
            }
        }

        public override void Interact()
        {
            //インタラクトしたプレイヤーをオーナーに再設定
            if (!Networking.IsOwner(this.gameObject))
            {
                Networking.SetOwner(Networking.LocalPlayer, this.gameObject);
            }

            //トグル切り替えののち、同期変数更新
            IsCubeActive = !IsCubeActive;
            RequestSerialization();
        }

        /// <summary>
        /// Cubeの出現切り替え
        /// </summary>
        public void ToggleCubeActive()
        {
            m_cube.SetActive(IsCubeActive);
        }
    }
}
