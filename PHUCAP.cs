using BusinessLayer.DTO;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PHUCAP
    {
        QLNHANSUEntities db = new QLNHANSUEntities();
        public tb_NHANVIEN_PHUCAP getItem(int id)
        {
            return db.tb_NHANVIEN_PHUCAP.FirstOrDefault(x =>x.ID == id);
        }
        public List<NHANVIEN_PHUCAP_DTO> getListFull()
        {
            var lstNVPC = db.tb_NHANVIEN_PHUCAP.ToList();
            List<NHANVIEN_PHUCAP_DTO> lstDTO = new List<NHANVIEN_PHUCAP_DTO>();
            NHANVIEN_PHUCAP_DTO nvpc;
            NHANVIEN _nhanvien = new NHANVIEN();
            foreach(var item in lstNVPC)
            {
                nvpc = new NHANVIEN_PHUCAP_DTO();
                nvpc.ID = item.ID;
                nvpc.MANV = item.MANV;
                nvpc.IDPC = item.IDPC;
                var nv = _nhanvien.getItemFull(int.Parse(item.MANV.ToString()));
                var pc = db.tb_PHUCAP.FirstOrDefault(x=>x.IDPC==item.IDPC);
                nvpc.HOTEN = nv.HOTEN;
                nvpc.TENPC = pc.TENPC;
                nvpc.NOIDUNG = item.NOIDUNG;
                nvpc.NGAY = item.NGAY;
                nvpc.SOTIEN = item.SOTIEN;
                lstDTO.Add(nvpc);
            }
            return lstDTO;
        }
        public tb_PHUCAP getItemPC(int id)
        {
            return db.tb_PHUCAP.FirstOrDefault(x=>x.IDPC == id);
        }
        public List<tb_PHUCAP> getListPC()
        {
            return db.tb_PHUCAP.ToList();
        }
        public tb_NHANVIEN_PHUCAP Add(tb_NHANVIEN_PHUCAP pc)
        {
            try
            {
                db.tb_NHANVIEN_PHUCAP.Add(pc);
                db.SaveChanges();
                return pc;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: "+ex.Message);
            }
        }
        public tb_NHANVIEN_PHUCAP Update(tb_NHANVIEN_PHUCAP pc)
        {
            try
            {
                var _pc = db.tb_NHANVIEN_PHUCAP.FirstOrDefault(x=>x.ID==pc.ID);
                _pc.IDPC = pc.IDPC;
                _pc.MANV = pc.MANV;
                _pc.NGAY = pc.NGAY;
                _pc.NOIDUNG = pc.NOIDUNG;
                _pc.SOTIEN = pc.SOTIEN;
                db.SaveChanges();
                return pc;
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
                var _pc = db.tb_NHANVIEN_PHUCAP.FirstOrDefault(x => x.ID == id);
                db.tb_NHANVIEN_PHUCAP.Remove(_pc);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public double getGia(int IDPC)
        {
            var sotien = db.tb_PHUCAP.FirstOrDefault(x => x.IDPC == IDPC);
            if (sotien != null)
            {
                return (double)sotien.SOTIEN;
            }
            return 0; // Hoặc giá trị mặc định khác nếu không tìm thấy thông tin
        }

    }
}
