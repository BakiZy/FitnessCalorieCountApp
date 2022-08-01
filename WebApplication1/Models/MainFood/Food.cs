

namespace WebApplication1.Models.MainFood
{

    public class Food
    {
        
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Proteins { get; set; }

        public int CarboHydrates { get; set; }

        public int Fats { get; set; }

        private int _kcals;


        public int Kcals
        {
            get
            {
                _kcals = Proteins * 4 + CarboHydrates * 4 + Fats * 9;
                return _kcals;
            }

            set
            {
                _kcals = value;
            }
        }
       
        
        public double Amount { get; set; }
        
        public int FoodTypeId { get; set; }
      
        public FoodType? FoodType { get; set; }

        public ICollection<Meal>? Meals { get; set; }
        
    }
}
