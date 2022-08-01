

namespace WebApplication1.Models.MainFood
{
    public class FoodType
    {
        
        public int Id { get; set; }

        public string? Name { get; set; }
        
        public ICollection<Food>? Foods { get; set; }

        
    }
}
