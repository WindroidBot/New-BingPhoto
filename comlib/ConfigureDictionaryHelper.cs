using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace comlib
{
    public class ConfigureDictionary
    {
        /// <summary>
        /// idx和事件对字典
        /// </summary>
        private Dictionary<string, string> dateDictionary = new Dictionary<string, string>();
        /// <summary>
        /// mkt和国家对字典
        /// </summary>
        private Dictionary<string, string> countryDictionary = new Dictionary<string, string>();
        /// <summary>
        /// mkt和数据库中表名对应字典
        /// </summary>
        private Dictionary<string, string> tableNameDictionary = new Dictionary<string, string>();

        public Dictionary<string, string> DateDictionary { get => dateDictionary; }
        public Dictionary<string, string> CountryDictionary { get => countryDictionary; }
        public Dictionary<string, string> TableNameDictionary { get => tableNameDictionary; }

        private void CreateDateDictionary()
        {
            dateDictionary.Add("-1", "明天");
            dateDictionary.Add("0", "今天");
            dateDictionary.Add("1", "昨天");
            dateDictionary.Add("2", "前天");
            dateDictionary.Add("3", "前3天");
            dateDictionary.Add("4", "前4天");
            dateDictionary.Add("5", "前5天");
            dateDictionary.Add("6", "前6天");
            dateDictionary.Add("7", "前7天");
            dateDictionary.Add("8", "前8天");
            dateDictionary.Add("9", "前9天");
            dateDictionary.Add("10", "前10天");
            dateDictionary.Add("11", "前11天");
            dateDictionary.Add("12", "前12天");
            dateDictionary.Add("13", "前13天");
            dateDictionary.Add("14", "前14天");
        }

        private void CreateCountryDictionary()
        {
            countryDictionary.Add("zh-cn", "中国");
            countryDictionary.Add("ja-jp", "日本");
            countryDictionary.Add("en-in", "印度");
            countryDictionary.Add("de-de", "德国");
            countryDictionary.Add("fr-fr", "法国");
            countryDictionary.Add("en-gb", "英国");
            countryDictionary.Add("pt-br", "巴西");
            countryDictionary.Add("en-ca", "加拿大");
            countryDictionary.Add("en-us", "美国");
            countryDictionary.Add("en-au", "澳大利亚");           
        }

        private void CreateTableNameDictionary()
        {
            tableNameDictionary.Add("zh-cn", "dbo.Table_cn");
            tableNameDictionary.Add("ja-jp", "dbo.Table_jp");
            tableNameDictionary.Add("en-in", "dbo.Table_in");
            tableNameDictionary.Add("de-de", "dbo.Table_de");
            tableNameDictionary.Add("fr-fr", "dbo.Table_fr");
            tableNameDictionary.Add("en-gb", "dbo.Table_gb");
            tableNameDictionary.Add("pt-br", "dbo.Table_br");
            tableNameDictionary.Add("en-ca", "dbo.Table_ca");
            tableNameDictionary.Add("en-us", "dbo.Table_us");
            tableNameDictionary.Add("en-au", "dbo.Table_au");
        }

        public ConfigureDictionary()
        {
            CreateDateDictionary();
            CreateCountryDictionary();
            CreateTableNameDictionary();
        }
    }
}