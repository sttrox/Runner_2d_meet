using UnityEngine;

namespace RunnerMeet
{
    public class Removal : MonoBehaviour
    {
        [SerializeField] private BackgroundPool _backgroundPool;
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent(out Background background))
            {
                _backgroundPool.Put(background);
            }
        }
    }
}