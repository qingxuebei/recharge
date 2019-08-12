namespace Model
{
    using System;
    using System.Collections.Generic;
    public partial class PulsaProduct
    {
        public string pulsa_code { get; set; }
        public string pulsa_op { get; set; }
        public string pulsa_nominal { get; set; }
        public decimal pulsa_price { get; set; }
        public string pulsa_type { get; set; }
        public string masaaktif { get; set; }
        public string status { get; set; }
        public string cn_quatity { get; set; }
        public string cn_op { get; set; }
        public int cn_status { get; set; }
        public decimal cn_price { get; set; }
        public System.DateTime create_time { get; set; }
        public System.DateTime update_time { get; set; }
        public string pulsa_channel { get; set; }
        public decimal cn_oldprice { get; set; }
    }
    public partial class PulsaProductShow
    {
        public string pulsa_code { get; set; }
        public string pulsa_op { get; set; }
        public string pulsa_nominal { get; set; }
        public string pulsa_type { get; set; }
        public string masaaktif { get; set; }
        public string status { get; set; }
        public string cn_quatity { get; set; }
        public string cn_op { get; set; }
        public int cn_status { get; set; }
        public decimal cn_price { get; set; }
        public string pulsa_channel { get; set; }
        public decimal cn_oldprice { get; set; }
    }
    public partial class PulsaPerfixProductShow
    {
        public List<Model.PulsaPrefix> pulsaPerfixList { get; set; }
        public List<Model.PulsaProductShow> pulsaProductShowList { get; set; }
    }
}
