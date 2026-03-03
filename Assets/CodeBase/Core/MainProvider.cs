using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Core
{
    public class MainProvider : MonoBehaviour
    {
        [SerializeField] private Main _mainPrefab;

        private void Awake()
        {
            Main main = Object.FindObjectOfType<Main>();
            if (main == null)
                Instantiate(_mainPrefab);
            
            Destroy(gameObject);
        }
    }
}