using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Promax.Business
{
    public static class Extensions
    {
        public static IMappingExpression<TSource, TDestination> IgnoreMember<TSource, TDestination, TMember>(this IMappingExpression<TSource, TDestination> mappingExpression, Expression<Func<TDestination, TMember>> destinationMember)
        {
            mappingExpression.ForMember(destinationMember, opt => opt.Ignore());
            return mappingExpression;
        }
        public static IMappingExpression<TSource, TDestination> IgnoreMembers<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression, params Expression<Func<TDestination, object>>[] destinationMember)
        {
            foreach (var item in destinationMember)
            {
                mappingExpression.IgnoreMember(item);
            }
            return mappingExpression;
        }
    }
}
