
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;


namespace UmiTanuki.UdonPractice
{
    public class IntaractTset : UdonSharpBehaviour
    {
        //出現させるCube
        [SerializeField]
        private GameObject m_cube = null;

        //出現切り替えフラグ
        private bool m_isCubeActive = false;

        void Start()
        {
            //nullチェック入れないと「Udon runtime exception detected!」エラーが出る
            if (m_cube != null)
            {
                m_cube.SetActive(m_isCubeActive);
            }
        }

        public override void Interact()
        {
            m_isCubeActive = !m_isCubeActive;
            m_cube.SetActive(m_isCubeActive);
        }
    }
}