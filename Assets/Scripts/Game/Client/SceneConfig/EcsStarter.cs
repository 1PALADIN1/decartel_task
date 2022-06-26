using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Client.SceneConfig
{
    public class EcsStarter : MonoBehaviour
    {
        [SerializeField] private BaseSceneSystemBuilder _sceneSystemBuilder;
        
        private EcsWorld _world;
        private EcsSystems _systems;
        
        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            
            if (_sceneSystemBuilder)
                _sceneSystemBuilder.Build(_systems);
            
            _systems.Init();
        }

        private void Update() => _systems.Run();

        private void OnDestroy()
        {
            _systems?.Destroy();
            _world?.Destroy();
        }
    }
}
