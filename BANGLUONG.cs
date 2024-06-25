using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BANGLUONG
    {
        QLNHANSUEntities db = new QLNHANSUEntities();
        public tb_BANGLUONG getItem(int id, int makycong, int manv)
        {
            return db.tb_BANGLUONG.FirstOrDefault(x => x.MAKYCONG == makycong && x.MANV == manv && x.ID == id );
        }
        public List<tb_BANGLUONG> getList(int makycong)
        {
            return db.tb_BANGLUONG.Where(x => x.MAKYCONG == makycong).ToList();
        }
        public void TinhLuongNhanVien (int makycong)
        {
            double luongngaythuong, luongtangca, luongchunhat, luongngayle, ungluong, thuclanh, hesoluong, dochai, chuyencan =0, bhxh, phat, com, tongcom = 0, xang, luonggio, thuetro, luonggiohd, tinhbhxh;
            var lstNV = db.tb_NHANVIEN.Where(x => x.DATHOIVIEC == null).ToList();
            var lstKCCT = db.tb_KYCONGCHITIET.Where(x=>x.GIOCONG != 0).ToList();
            foreach (var item in lstNV)
            {
                var hd = db.tb_HOPDONG.FirstOrDefault(x=>x.MANV==item.MANV);
                var bcctnv = db.tb_BANGCONG_NHANVIEN_CHITIET.FirstOrDefault(x=> x.MANV==item.MANV);
                var tc = db.tb_TANGCA.FirstOrDefault(x=>x.MANV == item.MANV);
                if (hd != null)
                {
                    var kcct = db.tb_KYCONGCHITIET.FirstOrDefault(x => x.MAKYCONG == makycong && x.MANV == item.MANV);
                    var bh = db.tb_BAOHIEM.FirstOrDefault(x => x.MANV == item.MANV);
                    hesoluong = Convert.ToDouble(hd.HESOLUONG);
                    luonggiohd = Convert.ToDouble(hd.LUONGGIO);
                    var luong1ngaycong = hd.LUONGCOBAN * hesoluong / Convert.ToDouble(kcct.NGAYCONG);
                    if (kcct != null && kcct.GIOCONG != null && kcct.GIOCONG >= 0)
                    {
                        // Tính lương theo công thức của bạn
                        //luongngaythuong = Convert.ToDouble(kcct.TONGNGAYCONG * luong1ngaycong);
                        // ... Các tính toán khác không thay đổi...
                        luongngaythuong = Convert.ToDouble(kcct.TONGNGAYCONG * luong1ngaycong);
                       
                        luongchunhat = Convert.ToDouble(kcct.CONGCHUNHAT * luong1ngaycong * 2);
                        luongngayle = Convert.ToDouble(kcct.CONGNGAYLE * luong1ngaycong * 3);
                        luongtangca = Convert.ToDouble(db.tb_TANGCA.Where(x => (x.NAM * 100 + x.THANG) == makycong && x.MANV == item.MANV).Sum(x => x.SOTIEN));
                        //phucap = Convert.ToDouble(db.tb_NHANVIEN_PHUCAP.Where(x => x.MANV == item.MANV).Sum(x => x.SOTIEN));
                        ungluong = Convert.ToDouble(db.tb_UNGLUONG.Where(x => x.MANV == item.MANV && (x.NAM * 100 + x.THANG) == makycong).Sum(x => x.SOTIEN));
                        xang = Convert.ToDouble(kcct.TIENXANG);
                        com = Convert.ToDouble(kcct.TIENCOM);
                        dochai = Convert.ToDouble(kcct.DOCHAI);
                        thuetro = Convert.ToDouble(kcct.THUETRO);
                        if(kcct.TONGNGAYCONG >= kcct.NGAYCONG)
                        {
                            chuyencan = chuyencan + 300000;
                        }
                        else
                        {
                            chuyencan = 0;
                        }
                        phat = Convert.ToDouble(db.tb_PHAT.Where(x => x.MANV == item.MANV && (x.NAM * 100 + x.THANG) == makycong).Sum(x => x.SOTIEN));
                        bhxh = Convert.ToDouble(db.tb_BAOHIEM.Where(x => x.MANV == item.MANV).Sum(x => x.TICHLUONG));
                        tinhbhxh = luongngaythuong * bhxh;
                        //thuclanh = luongngaythuong + luongphep + luongngayle + luongchunhat + luongtangca + phucap - ungluong;
                        // Tính giờ công và cập nhật thuộc tính LUONGGIO
                        luonggio = Convert.ToDouble(kcct.GIOCONG * hd.LUONGGIO);
                        //làm câu điều kiện để tranh sai số
                        thuclanh = luongngaythuong + luongngayle + luongchunhat + luongtangca + luonggio + xang + com + dochai + thuetro + chuyencan - ungluong - phat - tinhbhxh;
                        tb_BANGLUONG bl = new tb_BANGLUONG();
                        bl.MAKYCONG = makycong;
                        bl.MANV = item.MANV;
                        bl.HOTEN = item.HOTEN;
                        bl.NGAYCONGTRONGTHANG = int.Parse(kcct.NGAYCONG.ToString());
                        bl.NGAYPHEP = kcct.NGAYPHEP;
                        bl.NGAYCHUNHAT = luongchunhat;
                        bl.NGAYLE = luongngayle;
                        bl.NGAYTHUONG = luongngaythuong;                     
                        bl.LUONGGIO = hd.LUONGGIO;
                        bl.TANGCA = luongtangca;
                        bl.UNGLUONG = ungluong;
                        bl.THANHTIEN = luonggio;
                        bl.THUCLANH = thuclanh;
                        bl.TIENXANG = xang;
                        bl.TIENCOM = com;
                        bl.DOCHAI = dochai;
                        bl.THUETRO = thuetro;
                        bl.GIOCONG = kcct.GIOCONG;
                        bl.PHAT = phat;
                        bl.CHUYENCAN = chuyencan;
                        bl.BHXH = tinhbhxh;
                        Add(bl);
                    }
                    //tính lương
                    //luongngaythuong = Convert.ToDouble(kcct.TONGNGAYCONG * luong1ngaycong);
                    //luongphep = Convert.ToDouble(kcct.NGAYPHEP * luong1ngaycong * 0.3);
                    //luongchunhat = Convert.ToDouble(kcct.CONGCHUNHAT * luong1ngaycong * 2);
                    //luongngayle = Convert.ToDouble(kcct.CONGNGAYLE * luong1ngaycong * 3);
                    //luongtangca = Convert.ToDouble(db.tb_TANGCA.Where(x => (x.NAM * 100 + x.THANG) == makycong && x.MANV == item.MANV).Sum(x => x.SOTIEN));
                    //phucap = Convert.ToDouble(db.tb_NHANVIEN_PHUCAP.Where(x => x.MANV == item.MANV).Sum(x => x.SOTIEN));
                    //ungluong = Convert.ToDouble(db.tb_UNGLUONG.Where(x => x.MANV == item.MANV && (x.NAM * 100 + x.THANG) == makycong).Sum(x => x.SOTIEN) );

                    //thuclanh = luongngaythuong + luongphep + luongngayle + luongchunhat + luongtangca + phucap - ungluong;
                    //tb_BANGLUONG bl = new tb_BANGLUONG();
                    //bl.MAKYCONG = makycong;
                    //bl.MANV = item.MANV;
                    //bl.HOTEN = item.HOTEN;
                    //bl.NGAYCONGTRONGTHANG = int.Parse(kcct.NGAYCONG.ToString());
                    //bl.NGAYPHEP = luongphep;
                    //bl.NGAYCHUNHAT = luongchunhat;
                    //bl.NGAYLE = luongngayle;
                    //bl.NGAYTHUONG = luongngaythuong;
                    //bl.PHUCAP = phucap;
                    //bl.TANGCA = luongtangca;
                    //bl.UNGLUONG = ungluong;
                    //bl.THANHTIEN = luonggio;
                    //bl.THUCLANH = thuclanh;
                    //Add(bl);
                }             
            }   
        }
        public tb_BANGLUONG Add(tb_BANGLUONG bl)
        {
            try
            {
                db.tb_BANGLUONG.Add(bl);
                db.SaveChanges();
                return bl;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }

        }
        public tb_BANGLUONG Update(tb_BANGLUONG bl)
        {
            try
            {
                tb_BANGLUONG _bl = db.tb_BANGLUONG.FirstOrDefault(x => x.MAKYCONG == bl.MAKYCONG &&  x.MANV == bl.MANV && x.ID == bl.ID);
                _bl.MANV = bl.MANV;
                _bl.MAKYCONG = bl.MAKYCONG;
                _bl.HOTEN = bl.HOTEN;
                _bl.NGAYPHEP = bl.NGAYPHEP;
                _bl.KHONGPHEP = bl.KHONGPHEP;
                _bl.NGAYLE = bl.NGAYLE;
                _bl.UNGLUONG = bl.UNGLUONG;
                _bl.THUETRO = bl.THUETRO;
                _bl.TANGCA = bl.TANGCA;
                _bl.GIOCONG = bl.GIOCONG;
                _bl.TIENCOM = bl.TIENCOM;
                _bl.NGAYCHUNHAT = bl.NGAYCHUNHAT;
                _bl.NGAYCONGTRONGTHANG = bl.NGAYCONGTRONGTHANG;
                _bl.NGAYTHUONG = bl.NGAYTHUONG;
                _bl.THANHTIEN = bl.THANHTIEN;
                _bl.TIENCOM = bl.TIENCOM;
                _bl.TIENXANG = bl.TIENXANG;
                _bl.THUETRO = bl.THUETRO;
                _bl.DOCHAI = bl.DOCHAI;
                _bl.THUCLANH = bl.THUCLANH;
                _bl.PHAT = bl.PHAT;
                _bl.CHUYENCAN = bl.CHUYENCAN;
                _bl.BHXH = bl.BHXH;
                db.SaveChanges();
                return bl;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi: " + ex.Message);
            }
        }
        
    }
    //public void TinhLuongNhanVien(int makycong)
    //{
    //    // Các biến khác không thay đổi...

    //    var lstNV = db.tb_NHANVIEN.Where(x => x.DATHOIVIEC == null).ToList();

    //    foreach (var item in lstNV)
    //    {
    //        var hd = db.tb_HOPDONG.FirstOrDefault(x => x.MANV == item.MANV);
    //        if (hd != null)
    //        {
    //            var kcct = db.tb_KYCONGCHITIET.FirstOrDefault(x => x.MAKYCONG == makycong && x.MANV == item.MANV);
    //            hesoluong = Convert.ToDouble(hd.HESOLUONG);

    //            if (kcct != null && kcct.GIOCONG != null && kcct.GIOCONG > 0)
    //            {
    //                // Tính lương theo công thức của bạn
    //                luongngaythuong = Convert.ToDouble(kcct.TONGNGAYCONG * luong1ngaycong);
    //                // ... Các tính toán khác không thay đổi...

    //                // Tính giờ công và cập nhật thuộc tính LUONGGIO
    //                luonggio = Convert.ToDouble(kcct.GIOCONG * LUONGGIO);
    //            }

    //            // Các bước khác không thay đổi...
    //        }
    //    }
    //}

}
