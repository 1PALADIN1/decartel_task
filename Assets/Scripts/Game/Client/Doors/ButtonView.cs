using Game.Core.Common;
using Game.Core.Doors;
using UnityEngine;

namespace Game.Client
{
	public class ButtonView : BaseEntityView
	{
		[SerializeField] private DoorColor _color;
		[SerializeField, Min(0f)] private float _radius = 0.5f;
		
		protected override void OnInitialized()
		{
			var entity = World.NewEntity();
			ref var button = ref World.GetPool<FloorButton>().Add(entity);
			button.Color = _color;
			button.Radius = _radius;

			ref var position = ref World.GetPool<Position>().Add(entity);
			position.Value = transform.position;
		}
	}
}