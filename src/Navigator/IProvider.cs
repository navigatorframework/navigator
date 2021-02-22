namespace Navigator
{
    public interface IProvider
    {
        INavigatorClient GetClient(); 
        string GetActionType(object original);
    }
}