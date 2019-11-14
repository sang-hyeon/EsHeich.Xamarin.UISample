
namespace EsHeichSample.Client
{
    public interface IBaseEntity : IBaseEntity<int>
    {

    }

    public interface IBaseEntity<TKey>
        where TKey : struct
    {
        TKey ID { get; set; }
    }
}
