using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TIENCOM
    {
        QLNHANSUEntities db = new QLNHANSUEntities();
        public tb_TIENCOM getItem(int id)
        {
            return db.tb_TIENCOM.FirstOrDefault(x => x.IDTIENCOM == id);
        }
        public List<tb_TIENCOM> getList()
        {
            return db.tb_TIENCOM.ToList();
        }
        public tb_TIENCOM Add(tb_TIENCOM tiencom)
        {
            try
            {
                db.tb_TIENCOM.Add(tiencom);
                db.SaveChanges();
                return tiencom;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public tb_TIENCOM Update(tb_TIENCOM tiencom)
        {
            try
            {
                var _tiencom = db.tb_TIENCOM.FirstOrDefault(x => x.IDTIENCOM == tiencom.IDTIENCOM);
                _tiencom.TENCOM = tiencom.TENCOM;
                _tiencom.GIA = tiencom.GIA;
                db.SaveChanges();
                return tiencom;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public void Delete(int id)
        {

            try
            {
                var _tiencom = db.tb_TIENCOM.FirstOrDefault(x => x.IDTIENCOM == id);
                db.tb_TIENCOM.Remove(_tiencom);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public double getGia(int idTienCom)
        {
            var tienComInfo = db.tb_TIENCOM.FirstOrDefault(x => x.IDTIENCOM == idTienCom);
            if (tienComInfo != null)
            {
                return (double)tienComInfo.GIA;
            }
            return 0; // Hoặc giá trị mặc định khác nếu không tìm thấy thông tin
        }

    }
}
