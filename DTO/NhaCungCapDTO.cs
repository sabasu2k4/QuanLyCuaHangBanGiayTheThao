using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NhaCungCapDTO
    {
        private string maNCC;
        private string tenNCC;
        private string email;
        private string sDTLH;

        public string MaNCC { get; set; }
        public string TenNCC { get; set; }
        public string Email { get; set; }
        public string SDTLH { get; set; }

        public NhaCungCapDTO() { }

        public NhaCungCapDTO(string maNCC, string tenNCC, string email, string sDTLH)
        {
            this.maNCC = maNCC;
            this.tenNCC = tenNCC;
            this.email = email;
            this.sDTLH = sDTLH;
        }
    }
}
