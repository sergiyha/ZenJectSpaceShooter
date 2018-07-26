namespace Assets.Scripts.All_In.Commands
{
	public interface ICommand<T> where T: class
	{
		 void Execute(T payload);
	}
}
