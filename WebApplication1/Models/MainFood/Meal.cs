
namespace WebApplication1.Models.MainFood
{
    public class Meal
    {
        
        public int Id { get; set; }
        
        public string? Name { get; set; }

        public DateTime AddedOn { get; set; }

        public ICollection<Food>? Foods { get; set; }
    }
}
