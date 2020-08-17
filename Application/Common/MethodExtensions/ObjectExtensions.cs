using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.MethodExtensions
{
    public static class ObjectExtensions
    {
        public static bool IsNotTrue(this bool source)
        {
            return source == false;
        }
        public static bool IsTrue(this bool source)
        {
            return source == true;
        }
        public static bool IsOneOf<T>(this T source, params T[] list)
        {
            if (source is null)
            {
                throw new ArgumentNullException("source");
            }
            if (list is null)
            {
                throw new ArgumentNullException("list");
            }
            if (list.Any() == false)
            {
                return false;
            }

            return list.Contains(source);
        }

        //Check to see if a obj is between two dates Inclusive.
        public static bool IsBetween<T>(this T actual, T lower, T upper, bool inclusive = true) where T : IComparable<T>
        {
            if (inclusive)
            {
                return actual.CompareTo(lower) >= 0 && actual.CompareTo(upper) <= 0;
            }
            return actual.CompareTo(lower) > 0 && actual.CompareTo(upper) < 0;
        }

        public static T AddTo<T>(this T self, params ICollection<T>[] colls)
        {
            foreach (var coll in colls)
            {
                coll.Add(self);
            }

            return self;
        }
        public static TEntity AddTo<TEntity>(this TEntity self, DbSet<TEntity> entityDbSet) where TEntity : class
        {
            entityDbSet.Add(self);
            return self;
        }

        public static TResult With<TInput, TResult>(this TInput input, Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            return input is null ? null : evaluator(input);
        }

        public static TInput If<TInput>(this TInput input, Func<TInput, bool> evaluator)
            where TInput : class
        {
            if (input is null)
            {
                return null;
            }

            return evaluator(input) ? input : null;
        }

        public static TInput Do<TInput>(this TInput input, Action<TInput> action)
            where TInput : class
        {
            if (input is null)
            {
                return null;
            }

            action(input);
            return input;
        }

        public static TResult Return<TInput, TResult>(this TInput input,
            Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            return input is null ? failureValue : evaluator(input);
        }

        public static TResult WithValue<TInput, TResult>(this TInput input, Func<TInput, TResult> evaluator)
            where TInput : struct
        {
            return evaluator(input);
        }

        public struct BoolMarker<T>
        {
            public readonly bool Result;
            public readonly T Self;
            public readonly Operation PendingOp;
            public enum Operation
            {
                None,
                And,
                Or
            };


            private BoolMarker(bool result, T self, Operation pendingOp)
            {
                Result = result;
                Self = self;
                PendingOp = pendingOp;
            }

            public BoolMarker(bool result, T self)
                : this(result, self, Operation.None)
            {
            }

            public BoolMarker<T> And => new BoolMarker<T>(Result, Self, Operation.And);
            public BoolMarker<T> Or => new BoolMarker<T>(Result, Self, Operation.Or);

            public static implicit operator bool(BoolMarker<T> marker)
            {
                return marker.Result;
            }
        }

        public static BoolMarker<TSubject> HasNo<TSubject, T>(this TSubject self,
            Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(!props(self).Any(), self);
        }

        public static BoolMarker<TSubject> HasSome<TSubject, T>(this TSubject self,
            Func<TSubject, IEnumerable<T>> props)
        {
            return new BoolMarker<TSubject>(props(self).Any(), self);
        }

        public static BoolMarker<T> HasNo<T, U>(this BoolMarker<T> marker,
            Func<T, IEnumerable<U>> props)
        {
            return marker.PendingOp switch
            {
                BoolMarker<T>.Operation.And when marker.Result == false => marker,
                BoolMarker<T>.Operation.Or when marker.Result => marker,//TODO test this
                _ => new BoolMarker<T>(props(marker.Self).Any() == false, marker.Self)
            };
        }

        public static BoolMarker<T> HasSome<T, U>(this BoolMarker<T> marker,
            Func<T, IEnumerable<U>> props)
        {
            return marker.PendingOp switch
            {
                BoolMarker<T>.Operation.And when marker.Result == false => marker,
                BoolMarker<T>.Operation.Or when marker.Result => marker, //TODO test this
                _ => new BoolMarker<T>(props(marker.Self).Any(), marker.Self)
            };
        }

    }
}

//      Person p;
//      string postcode;
//      if (p != null)
//      {
//        if (HasMedicalRecord(p) && p.Address != null)
//        {
//          CheckAddress(p.Address);
//          if (p.Address.PostCode != null)
//            postcode = p.Address.PostCode.ToString();
//          else
//            postcode = "UNKNOWN";
//        }
//      }

//      string postcode = p.With(x => x.Address).With(x => x.PostCode);

//      postcode = p
//      .If(HasMedicalRecord)
//      .With(x => x.Address)
//      .Do(CheckAddress)
//      .Return(x => x.PostCode, "UNKNOWN");

//      if (person.Names.Count == 0) {}
//      if (!person.Names.Any())
//      if (person.HasSome(p => p.Names).And.HasNo(p => p.Children)) {}
