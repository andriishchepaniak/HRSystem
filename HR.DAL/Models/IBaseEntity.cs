namespace HR.DAL.Models
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        string Email { get; set; }
    }
}
