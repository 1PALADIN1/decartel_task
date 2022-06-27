using Leopotam.EcsLite;

namespace Game.Core.Doors
{
	public sealed class DoorSetupSystem : IEcsInitSystem
	{
		public void Init(EcsSystems systems)
		{
			var world = systems.GetWorld();
			var doorFilter = world.Filter<Door>().End();
			var doorPool = world.GetPool<Door>();
			
			foreach (var entity in doorFilter)
			{
				ref var door = ref doorPool.Get(entity);
				door.CurrentOpenValue = 1f;
			}
		}
	}
}