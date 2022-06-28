using Game.Core.Doors;
using UnityEngine;

namespace Game.Client
{
	public class DoorView : BaseEntityView
	{
		[SerializeField] private DoorColor _doorColor;
		[SerializeField, Min(0f)] private float _doorOpenSpeed;
		
		protected override void OnInitialized()
		{
			var entity = World.NewEntity();
			ref var door = ref World.GetPool<Door>().Add(entity);
			door.Color = _doorColor;
			door.OpenSpeed = _doorOpenSpeed;
		}
	}
}