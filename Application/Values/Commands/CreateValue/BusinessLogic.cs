using Application.Values.Commands.CreateValue;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Values.Commands.CreateValue
{
    internal static class BusinessLogic
    {
        public static void HandleBLL(this Value value, string example) 
        {
            _ = value;
            _ = example;
            //If as logic, goes here
        }
    }
}
