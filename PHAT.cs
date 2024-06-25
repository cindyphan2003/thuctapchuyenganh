using BusinessLayer.DTO;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class PHAT
    {
        QLNHANSUEntities db = new QLNHANSUEntities();
        public tb_PHAT getItem(int id)
        {
            return db.tb_PHAT.FirstOrDefault(x => x.IDPHAT == id);
        }
        public List<PHAT_DTO> getListFull()
        {
            var lst = db.tb_PHAT.ToList();
            List<PHAT_DTO> lstDTO = new List<PHAT_DTO>();
            PHAT_DTO dto;
            foreach (var item in lst)
            {
                dto = new PHAT_DTO();
                dto.IDPHAT = item.IDPHAT;
                dto.NGAY = item.NGAY;
                dto.THANG = item.THANG;
                dto.NAM = item.NAM;
                dto.MANV = item.MANV;
                var nv = db.tb_NHANVIEN.FirstOrDefault(n => n.MANV == item.MANV);
                dto.HOTEN = nv.HOTEN;
                dto.SOTIEN = item.SOTIEN;
                dto.NOIDUNG = item.NOIDUNG;

                lstDTO.Add(dto);
            }
            return lstDTO;
        }
        public tb_PHAT Add(tb_PHAT ul)
        {
            try
            {
                db.tb_PHAT.Add(ul);
                db.SaveChanges();
                return ul;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public tb_PHAT Update(tb_PHAT ul)
        {
            try
            {
                var _ul = db.tb_PHAT.FirstOrDefault(x => x.IDPHAT == ul.IDPHAT);
                _ul.NGAY = ul.NGAY;
                _ul.THANG = ul.THANG;
                _ul.NAM = ul.NAM;
                _ul.MANV = ul.MANV;
                _ul.SOTIEN = ul.SOTIEN;
                _ul.NOIDUNG = ul.NOIDUNG;

                db.SaveChanges();
                return ul;
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
                var _ul = db.tb_PHAT.FirstOrDefault(x => x.IDPHAT == id);
                db.tb_PHAT.Remove(_ul);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
    }
}
