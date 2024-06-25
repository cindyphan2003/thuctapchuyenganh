using BusinessLayer.DTO;
using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class UNGLUONG
    {
        QLNHANSUEntities db = new QLNHANSUEntities();
        public tb_UNGLUONG getItem(int id)
        {
            return db.tb_UNGLUONG.FirstOrDefault(x=>x.ID==id);
        }
        public List<UNGLUONG_DTO> getListFull()
        {
            var lst=db.tb_UNGLUONG.ToList();
            List<UNGLUONG_DTO> lstDTO = new List<UNGLUONG_DTO>();
            UNGLUONG_DTO dto;
            foreach (var item in lst)
            {
                dto = new UNGLUONG_DTO();
                dto.ID = item.ID;
                dto.NGAY = item.NGAY;
                dto.THANG = item.THANG;
                dto.NAM = item.NAM;
                dto.MANV = item.MANV;
                var nv = db.tb_NHANVIEN.FirstOrDefault(n=>n.MANV==item.MANV);
                dto.HOTEN = nv.HOTEN;
                dto.SOTIEN = item.SOTIEN;
                dto.GHICHU = item.GHICHU;
                
                lstDTO.Add(dto);
            }
            return lstDTO;
        }
        public tb_UNGLUONG Add(tb_UNGLUONG ul)
        {
            try
            {
                db.tb_UNGLUONG.Add(ul);
                db.SaveChanges();
                return ul;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        public tb_UNGLUONG Update(tb_UNGLUONG ul)
        {
            try
            {
                var _ul = db.tb_UNGLUONG.FirstOrDefault(x => x.ID == ul.ID);
                _ul.NGAY = ul.NGAY;
                _ul.THANG = ul.THANG;
                _ul.NAM = ul.NAM;
                _ul.MANV = ul.MANV;
                _ul.SOTIEN = ul.SOTIEN;
                _ul.GHICHU = ul.GHICHU;
          
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
                var _ul = db.tb_UNGLUONG.FirstOrDefault(x => x.ID == id);
                db.tb_UNGLUONG.Remove(_ul);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
    }
}
