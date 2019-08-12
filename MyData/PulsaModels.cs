namespace MyData
{
    using System;
    using System.Collections.Generic;
    public class CallPulsa
    {
        public string commands { get; set; }
        public string username { get; set; }
        public string sign { get; set; }
        public string status { get; set; }
        public string ref_id { get; set; }
        public string hp { get; set; }
        public string pulsa_code { get; set; }

    }
    public class PulsaBalance
    {
        public string balance { get; set; }
    }
    public class PulsaPrice
    {
        public string pulsa_code { get; set; }
        public string pulsa_op { get; set; }
        public string pulsa_nominal { get; set; }
        public decimal pulsa_price { get; set; }
        public string pulsa_type { get; set; }
        public string masaaktif { get; set; }
        public string status { get; set; }
    }
    public class PulsaTopUp
    {
        public string ref_id { get; set; }
        public decimal status { get; set; }
        public string code { get; set; }
        public string hp { get; set; }
        public decimal price { get; set; }
        public string message { get; set; }
        public decimal balance { get; set; }
        public decimal tr_id { get; set; }
        public string rc { get; set; }
    }
    public class PlusaPriceList
    {
        public List<MyData.PulsaPrice> data { get; set; }
    }
}
