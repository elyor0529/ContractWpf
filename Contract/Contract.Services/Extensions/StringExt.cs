using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Data.Models.Enums;

namespace Contract.Services.Extensions
{
    public static class StringExt
    {
        public static BranchCode GetBranchCode(this string s)
        {
            if (String.IsNullOrWhiteSpace(s))
                return BranchCode.Brach1;

            if (s.IndexOf("/", StringComparison.InvariantCultureIgnoreCase) != -1 &&
                s.IndexOf("-", StringComparison.InvariantCultureIgnoreCase) != -1)
                return BranchCode.Brach1;
            {
                var array = s.Split(new string[] { "/", "-" }, StringSplitOptions.RemoveEmptyEntries);

                if (array.Length == 3)
                {
                    int brachId;

                    if (int.TryParse(array[0], out brachId))
                        return (BranchCode)brachId;
                }
            }

            return BranchCode.Brach1;
        }
    }
}
