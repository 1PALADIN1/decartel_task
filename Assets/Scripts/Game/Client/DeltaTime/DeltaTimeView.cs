using Game.Core.Common;

namespace Game.Client
{
	public class DeltaTimeView : BaseEntityView
	{
		protected override void OnInitialized()
		{
			var entity = World.NewEntity();
			World.GetPool<DeltaTime>().Add(entity);
		}
	}
}