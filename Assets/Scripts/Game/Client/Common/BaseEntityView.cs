using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Client
{
	public abstract class BaseEntityView : MonoBehaviour
	{
		protected EcsWorld World { get; private set; }

		public void Initialize(EcsWorld ecsWorld)
		{
			World = ecsWorld;
			
			OnInitialized();
		}

		protected virtual void OnInitialized() { }
	}
}