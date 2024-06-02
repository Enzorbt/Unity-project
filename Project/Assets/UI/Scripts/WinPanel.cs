using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Supinfo.Project.UI.Scripts
{
    public class WinPanel : MonoBehaviour
    {
        public void OnClick()
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}