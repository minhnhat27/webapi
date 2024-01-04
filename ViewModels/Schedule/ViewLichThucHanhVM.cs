using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webapi.ViewModels.Schedule
{
    public class ViewLichThucHanhVM
    {
        public DateTime ngaythuchanh { get; set; }
        public string buoi { get; set; }
        public int tuan { get; set; }
        public string mscb { get; set; }
        public string hoten { get; set; }
        public int sttbuoithuchanh { get; set; }
        public string manhomhp { get; set; }
        public string tenhp { get; set; }
        public int? phong { get; set; }
    }
}
