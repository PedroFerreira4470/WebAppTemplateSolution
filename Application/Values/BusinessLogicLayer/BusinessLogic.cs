
using Domain.Entities;

namespace Application.Values.BusinessLogic
{
    public static class BusinessLogic
    {
        public static void IncrementByOneIfBiggerThan(this Value value,int number) 
        {
            if (value.ValueNumber > number)
                value.ValueNumber++;
        }
    }
}
