using System.Collections.Generic;
using Game.Core.Common;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Core.Doors
{
	/// <summary>
	/// Система для открывания дверей, если нажата соответстующая кнопка
	/// </summary>
	public sealed class DoorOpenSystem : IEcsInitSystem, IEcsRunSystem
	{
		private EcsFilter _doorFilter;
		private EcsFilter _buttonPressedFilter;
		
		private EcsPool<Door> _doorPool;
		private EcsPool<FloorButtonPressed> _buttonPressedPool;
		private EcsPool<DeltaTime> _deltaTimePool;

		private HashSet<DoorColor> _openingDoorColors; 

		public void Init(EcsSystems systems)
		{
			var world = systems.GetWorld();

			_doorFilter = world
				.Filter<Door>()
				.Inc<DeltaTime>()
				.End();

			_buttonPressedFilter = world
				.Filter<FloorButtonPressed>()
				.End();

			_doorPool = world.GetPool<Door>();
			_buttonPressedPool = world.GetPool<FloorButtonPressed>();
			_deltaTimePool = world.GetPool<DeltaTime>();

			_openingDoorColors = new HashSet<DoorColor>();
		}

		public void Run(EcsSystems systems)
		{
			_openingDoorColors.Clear();
			
			foreach (var buttonEntity in _buttonPressedFilter)
			{
				ref var pressedButton = ref _buttonPressedPool.Get(buttonEntity);
				if (!_openingDoorColors.Contains(pressedButton.Color))
					_openingDoorColors.Add(pressedButton.Color);
			}

			foreach (var doorEntity in _doorFilter)
			{
				ref var door = ref _doorPool.Get(doorEntity);
				if (_openingDoorColors.Contains(door.Color))
				{
					ref var deltaTime = ref _deltaTimePool.Get(doorEntity);
					door.CurrentOpenValue = Mathf.Max(0f, door.CurrentOpenValue - door.OpenSpeed * deltaTime.Value);
				}
			}
		}
	}
}