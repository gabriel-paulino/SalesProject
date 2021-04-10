using Microsoft.AspNetCore.Mvc.Rendering;
using SalesProject.ViewModels;
using System;
using System.Linq;

namespace SalesProject.Helper
{
    public class EnumeratorExtension
    {

        public static SelectList GetSelectListEnumAddressType() =>
            new SelectList(Enum.GetValues(typeof(AddressType)).
                Cast<AddressType>().Select(v =>
                new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList(), "Value", "Text");

        /*public static SelectList GetSelectListEnumOrderStatus() =>
            new SelectList(Enum.GetValues(typeof(OrderStatus)).
                Cast<OrderStatus>().Select(v =>
                new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList(), "Value", "Text");*/

    }
}