namespace OneLine.Bases
{
    /// <summary>
    /// Defines that the class is http serviceable
    /// </summary>
    /// <typeparam name="THttpService"></typeparam>
    public interface IHttpServiceable<THttpService>
    {
        THttpService HttpService { get; set; }
    }
}
