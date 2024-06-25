using BusinessLayer.DTO;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class KHENTHUONG
    {
        QLNHANSUEntities db = new QLNHANSUEntities();
        public tb_KHENTHUONG getItem(string id)
        {
            return db.tb_KHENTHUONG.FirstOrDefault(x => x.SOQUYETDINH == id);
        }
        public List<tb_KHENTHUONG> getList()
        {
            return db.tb_KHENTHUONG.ToList();
        }
        public List<KHENTHUONG_DTO> getListFull()
        {
            List<tb_KHENTHUONG> lsKT = db.tb_KHENTHUONG.ToList();
            List<KHENTHUONG_DTO> lstDTO = new List<KHENTHUONG_DTO>();
            KHENTHUONG_DTO kt;
            foreach (var item in lsKT)
            {
                kt = new KHENTHUONG_DTO();
                kt.SOQUYETDINH = item.SOQUYETDINH;
                kt.NGAY = item.NGAY;
                kt.LYDO = item.LYDO;
                kt.NOIDUNG = item.NOIDUNG;
                kt.MANV = item.MANV;
                var nv = db.tb_NHANVIEN.FirstOrDefault(n => n.MANV == item.MANV);
                kt.HOTEN = nv.HOTEN;
                lstDTO.Add(kt);
            }
            return lstDTO;
        }
        public tb_KHENTHUONG Add(tb_KHENTHUONG kt)
        {
            try
            {
                db.tb_KHENTHUONG.Add(kt);
                db.SaveChanges();
                return kt;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public tb_KHENTHUONG Update(tb_KHENTHUONG kt)
        {
            try
            {
                var _kt = db.tb_KHENTHUONG.FirstOrDefault(x => x.SOQUYETDINH == kt.SOQUYETDINH);
                _kt.NGAY = kt.NGAY;
                _kt.NOIDUNG = kt.NOIDUNG;
                _kt.LYDO = kt.LYDO;             
                _kt.MANV = _kt.MANV;
                db.SaveChanges();
                return kt;
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
                var _kt = db.tb_KHENTHUONG.FirstOrDefault(x => x.SOQUYETDINH == id);
                db.tb_KHENTHUONG.Remove(_kt);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public string MaxSoQuyetDinh()
        {
            var _kt = db.tb_KHENTHUONG.OrderByDescending(x => x.SOQUYETDINH).FirstOrDefault();
            if (_kt != null)
            {
                return _kt.SOQUYETDINH;
            }
            else
            { return "00000"; }
        }
    }
}
