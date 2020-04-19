namespace OneLine.Bases
{
    public interface IHttpServiceable<THttpService>
    {
        THttpService HttpService { get; set; }
    }
}
