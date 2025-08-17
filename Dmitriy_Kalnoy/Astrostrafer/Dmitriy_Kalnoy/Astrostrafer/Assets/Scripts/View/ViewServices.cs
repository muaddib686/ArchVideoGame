using UnityEngine;

namespace Astrostrafer.View
{
    public static class ViewServices
    {
        public static Transform ViewRoot { get; private set; }
        public static GameObject PlayerPrefab { get; private set; }
        public static GameObject AsteroidPrefab { get; private set; }

        public static void Initialize(Transform viewRoot, GameObject playerPrefab, GameObject asteroidPrefab)
        {
            ViewRoot = viewRoot;
            PlayerPrefab = playerPrefab;
            AsteroidPrefab = asteroidPrefab;
        }
    }
}


