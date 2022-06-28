using Game.Core.Common;
using Game.Core.Movement;
using UnityEngine;

namespace Game.Client
{
	public class PlayerView : BaseEntityView
	{
		[SerializeField, Min(0f)] private float _movementSpeed;
		[SerializeField, Min(0f)] private float _stoppingDistance;
		
		protected override void OnInitialized()
		{
			var startPosition = transform.position;
			
			var playerEntity = World.NewEntity();
			World.GetPool<Player>().Add(playerEntity);
			
			ref var position = ref World.GetPool<Position>().Add(playerEntity);
			position.Value = startPosition;
			
			ref var movement = ref World.GetPool<Movement>().Add(playerEntity);
			movement.Speed = _movementSpeed;
			movement.StoppingDistance = _stoppingDistance;

			ref var view = ref World.GetPool<View>().Add(playerEntity);
			view.Value = gameObject;
		}
	}
}