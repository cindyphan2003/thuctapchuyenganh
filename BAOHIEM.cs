using BusinessLayer.DTO;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BAOHIEM
    {
        QLNHANSUEntities db = new QLNHANSUEntities();
        public tb_BAOHIEM getItem(int id)
        {
            return db.tb_BAOHIEM.FirstOrDefault(x => x.IDBH == id);
        }
        public List<tb_BAOHIEM> getList()
        {
            return db.tb_BAOHIEM.ToList();
        }
        public List<BAOHIEM_DTO> getListFull()
        {
            List<tb_BAOHIEM> lsBH = db.tb_BAOHIEM.ToList();
            List<BAOHIEM_DTO> lstDTO = new List<BAOHIEM_DTO>();
            BAOHIEM_DTO bh;
            foreach (var item in lsBH)
            {
                bh = new BAOHIEM_DTO();
                bh.IDBH = item.IDBH;
                bh.SOBH = item.SOBH;
                bh.TICHLUONG = item.TICHLUONG;
                bh.NGAYCAP = item.NGAYCAP;
                bh.NOICAP = item.NOICAP;
                var nv = db.tb_NHANVIEN.FirstOrDefault(n => n.MANV == item.MANV);
                bh.HOTEN = nv.HOTEN;
                
                lstDTO.Add(bh);
            }
            return lstDTO;
        }
        public tb_BAOHIEM Add(tb_BAOHIEM bh)
        {
            try
            {
                db.tb_BAOHIEM.Add(bh);
                db.SaveChanges();
                return bh;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public tb_BAOHIEM Update(tb_BAOHIEM bh)
        {
            try
            {
                var _bh = db.tb_BAOHIEM.FirstOrDefault(x => x.IDBH == bh.IDBH);
                _bh.MANV=bh.MANV;
                _bh.NOICAP=bh.NOICAP;
                _bh.NGAYCAP = bh.NGAYCAP;
                _bh.TICHLUONG = bh.TICHLUONG;
                _bh.SOBH = bh.SOBH;
                db.SaveChanges();
                return bh;
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
                var _bh = db.tb_BAOHIEM.FirstOrDefault(x => x.IDBH == id);
                db.tb_BAOHIEM.Remove(_bh);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
    }
}
