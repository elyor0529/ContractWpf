using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Services
{
    public static class ExportConfig
    {
        public struct KEYS
        {
            public const string CONTRACT_NUMBER = "#contract_number#";
            public const string CONTRACT_DATE = "#contract_date#";
            public const string CONTRACT_TOTAL_PRICE = "#contract_total_price#";
            public const string CONTRACT_TOTAL_PRICE_STR = "#contract_total_price_str#";
            public const string ORGANIZATION_NAME = "#organization_name#";
            public const string ORGANIZATION_PHONE_NUMBER = "#organization_phone_number#";
            public const string ORGANIZATION_OKOHX = "#organization_okohx#";
            public const string ORGANIZATION_MFO1 = "#organization_mfo1#";
            public const string ORGANIZATION_INN = "#organization_inn#";
            public const string ORGANIZATION_ADDRESS = "#organization_address#";
            public const string ORGANIZATION_ACCOUNT_NUMBER = "#organization_account_number#";
            public const string OBJECT_NAME = "#object_name#";
            public const string ACTINVOICE_NUMBER = "#actinvoice_number#";
            public const string ACTINVOICE_DATE = "#actinvoice_date#";
            public const string CONTRACT_WORK_TYPE = "#contract_work_types#";
            public const string CONTRACT_WORK_TOTAL_AMOUNT = "#contract_work_total_amount#";
            public const string CONTRACT_WORK_TOTAL_AMOUNT_STR = "#contract_work_total_amount_str#";
        }
    }
}
