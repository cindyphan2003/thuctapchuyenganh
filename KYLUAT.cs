using BusinessLayer.DTO;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class KYLUAT
    {
        QLNHANSUEntities db = new QLNHANSUEntities();
        public tb_KYLUAT getItem(string id)
        {
            return db.tb_KYLUAT.FirstOrDefault(x => x.SOQUYETDINH == id);
        }
        public List<tb_KYLUAT> getList()
        {
            return db.tb_KYLUAT.ToList();
        }
        public List<KYLUAT_DTO> getListFull()
        {
            List<tb_KYLUAT> lskl = db.tb_KYLUAT.ToList();
            List<KYLUAT_DTO> lstDTO = new List<KYLUAT_DTO>();
            KYLUAT_DTO kl;
            foreach (var item in lskl)
            {
                kl = new KYLUAT_DTO();
                kl.SOQUYETDINH = item.SOQUYETDINH;
                kl.TUNGAY = item.TUNGAY;
                kl.DENNGAY = item.DENNGAY;
                kl.LYDO = item.LYDO;
                kl.NOIDUNG = item.NOIDUNG;
                kl.MANV = item.MANV;
                var nv = db.tb_NHANVIEN.FirstOrDefault(n => n.MANV == item.MANV);
                kl.HOTEN = nv.HOTEN;
                lstDTO.Add(kl);
            }
            return lstDTO;
        }
        public tb_KYLUAT Add(tb_KYLUAT kl)
        {
            try
            {
                db.tb_KYLUAT.Add(kl);
                db.SaveChanges();
                return kl;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public tb_KYLUAT Update(tb_KYLUAT kl)
        {
            try
            {
                var _kl = db.tb_KYLUAT.FirstOrDefault(x => x.SOQUYETDINH == kl.SOQUYETDINH);
                _kl.TUNGAY = kl.TUNGAY;
                _kl.DENNGAY = kl.DENNGAY;
                _kl.NOIDUNG = kl.NOIDUNG;
                _kl.LYDO = kl.LYDO;
                _kl.MANV = _kl.MANV;
                db.SaveChanges();
                return kl;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public void Delete(string id)
        {

            try
            {
                var _kl = db.tb_KYLUAT.FirstOrDefault(x => x.SOQUYETDINH == id);
                db.tb_KYLUAT.Remove(_kl);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public string MaxSoQuyetDinh()
        {
            var _kl = db.tb_KYLUAT.OrderByDescending(x => x.SOQUYETDINH).FirstOrDefault();
            if (_kl != null)
            {
                return _kl.SOQUYETDINH;
            }
            else
            { return "00000"; }
        }
    }
}
