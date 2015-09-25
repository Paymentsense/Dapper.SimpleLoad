using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.SimpleSave;

namespace Dapper.SimpleLoad.Tests.RealisticDtos
{
    [Table("[app].[APPLICATION_MST]")]
    public class APPLICATION_MST
    {
        public APPLICATION_MST()
        {
            
            LOCATION_MST = new List<LOCATION_MST>();
            BUSINESS_LEGAL_INFO_LNK = new List<BUSINESS_LEGAL_INFO_LNK>();
            SHAREHOLDER_MST = new List<SHAREHOLDER_MST>();
            PRINCIPAL_MST = new List<PRINCIPAL_MST>();
        }
        [PrimaryKey]
        public Guid? ApplicationGUID { get; set; }
        public Guid MerchantGUID { get; set; }
        public DateTime? ApplicationSignedDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public Guid UpdateSessionGUID { get; set; }
        public long V1ApplicationId { get; set; }
        public Guid SalesChannelGUID { get; set; }
        public GenCountryEnum CountryKey { get; set; }
        public string ApplicationReference { get; set; }
        public bool IsValidated { get; set; }

        [OneToOne, Column("MerchantGUID")]
        public virtual MERCHANT_MST MERCHANT_MST { get; set; }
        [OneToOne, Column("SalesChannelGUID")]
        public virtual SALES_CHANNEL_LNK SALES_CHANNEL_LNK { get; set; }
        [OneToMany, Column("LocationGUID")]
        public virtual IList<LOCATION_MST> LOCATION_MST { get; set; }
        [OneToOne("ApplicationGUID")]
        public virtual LEGAL_INFO_MST LEGAL_INFO_MST { get; set; }
        [OneToMany, Column("BusinessLegalInfoGUID")]
        public virtual IList<BUSINESS_LEGAL_INFO_LNK> BUSINESS_LEGAL_INFO_LNK { get; set; }
        [OneToMany]
        public virtual IList<SHAREHOLDER_MST> SHAREHOLDER_MST { get; set; }
        [OneToMany]
        public virtual IList<PRINCIPAL_MST> PRINCIPAL_MST { get; set; }
    }
}
