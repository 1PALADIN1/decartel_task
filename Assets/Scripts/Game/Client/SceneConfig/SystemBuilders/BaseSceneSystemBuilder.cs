using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Client.SceneConfig
{
    public abstract class BaseSceneSystemBuilder : MonoBehaviour
    {
        public abstract void Build(EcsSystems ecsSystems);
    }
}