using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace KKN_UI.Helper
{
    public class DecimalModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            ModelState modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                actualValue = Convert.ToDecimal(valueResult.AttemptedValue, CultureInfo.CurrentCulture);
                if (actualValue == null)
                {
                    actualValue = Decimal.Parse(valueResult.AttemptedValue, NumberStyles.Currency);
                }

            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }

    public class DateModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            ModelState modelState = new ModelState { Value = valueResult };

            if (valueResult == null)
                return null;

            if (valueResult.AttemptedValue.Contains("/Date"))
            {
                var sa = @"""" + valueResult.AttemptedValue + @"""";
                DateTime date = JsonConvert.DeserializeObject<DateTime>(sa);
                if (date.Hour == 17 && date.Millisecond == 0 && date.Second == 0)
                {
                    date = date.AddDays(1);
                }
                return date;
            }
            DateTime outVal;
            string[] formats = {"dd/MM/yyyy", "dd-MMM-yyyy", "yyyy-MM-dd",
                   "dd-MM-yyyy", "M/d/yyyy", "dd MMM yyyy","dd/MM/yyyy HH:mm:ss.fff"};
            if (DateTime.TryParseExact(valueResult.AttemptedValue, formats, null, System.Globalization.DateTimeStyles.None, out outVal))
            {

                return outVal;
            }
            else
            {
                if (DateTime.TryParseExact(valueResult.AttemptedValue, formats, null, System.Globalization.DateTimeStyles.None, out outVal))
                {
                    return outVal;
                }
                return null;
            }
        }
    }

    public class DoubleModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            ModelState modelState = new ModelState { Value = valueResult };
            object actualValue = null;
            try
            {
                actualValue = Convert.ToDouble(valueResult.AttemptedValue, CultureInfo.CurrentCulture);
                if (actualValue == null)
                {
                    actualValue = Double.Parse(valueResult.AttemptedValue, NumberStyles.Currency);
                }

            }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }


}