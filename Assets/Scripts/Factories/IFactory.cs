namespace Assets.Scripts.Factories
{
	public interface IFactory<T, TOut>
		where T : struct
		where TOut : class
	{
		TOut Create(T param);
	}
}
