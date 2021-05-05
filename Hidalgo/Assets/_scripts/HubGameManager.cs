using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class HubGameManager : MonoBehaviour
    {
        [SerializeField] private SceneManager _sceneManager;

        // Start is called before the first frame update
        void Start()
        {
            Init();
        }

        void Init()
        {
            Debug.Log("inicializando gamemanager hub");
        }

        public void LoadScene(string sceneName)
        {
            _sceneManager.LoadSync(sceneName);
        }
        public void LoadScene(Enums.Level sceneId)
        {
            _sceneManager.LoadSync((int)sceneId);
        }
    }
}