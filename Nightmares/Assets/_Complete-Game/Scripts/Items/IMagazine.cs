namespace CompleteProject
{
	public interface IMagazine : IObservable<IMagazine>
	{
		int AmmoCount { get; }
		int Capacity { get; }

		bool CanFeed();
		void Reload();
	}
}