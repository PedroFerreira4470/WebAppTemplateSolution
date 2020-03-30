using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Values._ValueBLL
{
    public static class ValueBLL
    {
        public static void ChangeValueIfBiggerThan(this Value value,int number) {
            if (value.ValueNumber > number) value.ValueNumber *= 5;
        }

        public static bool IsValueNumberEven(this Value value) => (value.ValueNumber % 2) == 0;
        public static bool IsValueNumberOdd(this Value value) => (value.ValueNumber % 2) != 0;
    }
}
