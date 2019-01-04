namespace FazelMan.Domain.Entities
{
    public interface ISoftDelete
    {
        bool IsRemoved { get; set; }
    }
}
