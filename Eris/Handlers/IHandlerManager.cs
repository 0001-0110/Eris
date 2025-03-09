namespace Eris.Handlers;

internal interface IHandlerManager<THandler> where THandler : IHandler
{
    void AddHandler(THandler handler);
}
