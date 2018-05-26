using System;
using System.Web;
using Microsoft.Practices.Unity;

namespace Contract.Core
{
    public class PerHttpRequestLifetime : PerThreadLifetimeManager
    {
        // This is very important part and the reason why I believe mentioned
        // PerCallContext implementation is wrong.
        private readonly Guid _key = Guid.NewGuid();

        public override object GetValue()
        {
            return HttpContext.Current != null ? HttpContext.Current.Items[_key] : base.GetValue();
        }

        public override void SetValue(object newValue)
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items[_key] = newValue;
            else
                base.SetValue(newValue);
        }

        public override void RemoveValue()
        {
            if (HttpContext.Current != null)
                HttpContext.Current.Items.Remove(GetValue());
            else
                base.RemoveValue();
        }
    }
}