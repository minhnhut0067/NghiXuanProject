using Newtonsoft.Json.Linq;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace APIDev.Models
{
    public class Data
    {
        public static string ngaytinhbaohiem = "10/10/2016";

        public class Benhnhan
        {
            public string mabn { get; set; }
            public string mavaovien { get; set; }
            public string madoituong { get; set; }
            public string maql { get; set; }
            public string tenbn { get; set; }
            public string ngaysinh { get; set; }
            public string gioitinh { get; set; }
            public string mathe { get; set; }
            public string ngayvao { get; set; }
            public string sovaovien { get; set; }
            public string makp { get; set; }
            public string tenkp { get; set; }
            public string mabs { get; set; }
            public string tungaybhyt { get; set; }
            public string denngaybhyt { get; set; }

            public static IEnumerable<Benhnhan> Get(string s_makp)
            {
                try
                {
                    DataSet ds = new DataSet();
                    List<Benhnhan> lts = new List<Benhnhan>();
                    string sql = "";
                    sql = "select a.maql as maql,a.mabn, a.done,";
                    sql += "\ncase when a.done in ('?') then 'Đi làm CLS' else case when a.done='d' then 'Có kết quả' else case when a.noitiepdon=16 then 'Tái khám' else 'Chờ khám' end end end as tk,";
                    sql += "\na.makp,to_char(a.ngay,'mmddyy:hh') as ngayvao,case when a.doituonguutien is null then 0 else a.doituonguutien end as doituonguutien,medibv.khongdaucokhoangtrang(b.hoten) as hotenkhongdau,";
                    sql += "\nhoten,ngaysinh,case when b.phai=0 then 'Nam' else 'Nữ' end as gioitinh,c.sothe as mathe";
                    sql += "\nfrom xxx.tiepdon a inner join medibv.btdbn b on a.mabn=b.mabn";
                    sql += "\nleft join xxx.bhyt c on a.maql = c.maql where 1=1 and a.ngay between now()-1 and now()";
                    string strMakp = "";
                    if (s_makp != "")
                    {
                        string s = s_makp.Replace(",", "','");
                        //sql += " and a.makp in ('" + s.Substring(0, s.Length - 3) + "')";
                        if (s.Length > 3) s = s.Substring(0, s.Length - 3);
                        {
                            sql += " and makp in ('" + s + "')";
                            strMakp += " and a.makp in ('" + s + "')";
                        }
                    }
                    string ammyy = "";
                    ammyy = funcmmyy(DateTime.Today.ToString("dd/MM/yyyy"));
                    sql = sql.Replace("xxx.", "medibv" + ammyy + ".");
                    string sqlLower = sql.ToLower();
                    ds = dbHelper.getDataSetbySql(sql);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Benhnhan item = new Benhnhan();
                            item.maql = dr["maql"].ToString();
                            item.mabn = dr["mabn"].ToString();
                            item.tenbn = dr["hoten"].ToString();
                            item.ngaysinh = dr["ngaysinh"].ToString();
                            item.gioitinh = dr["gioitinh"].ToString();
                            item.mathe = dr["mathe"].ToString();
                            item.ngayvao = dr["ngayvao"].ToString();
                            item.mabs = dr["mabs"].ToString();
                            item.makp = dr["makp"].ToString();
                            lts.Add(item);
                        }
                    }
                    return lts;
                }
                catch
                {
                    return null;
                }
            }

            public static IEnumerable<Benhnhan> Getmmyy(string s_makp, string s_tungay, string s_denngay, string loai, string mabn, string mabs)
            {
                DateTime dt1 = StringToDate(s_tungay);
                DateTime dt2 = StringToDate(s_denngay);

                //  d2.Day.ToString().PadLeft(2, '0') + "/" + d2.Month.ToString().PadLeft(2, '0') + "/" + d2.Year.ToString()
                int y1 = dt1.Year, m1 = dt1.Month;
                int y2 = dt2.Year, m2 = dt2.Month;
                int itu, iden;
                string mmyy = "";
                string sql = "";
                List<Benhnhan> lts = new List<Benhnhan>();
                if (loai == "x")
                {
                    for (int i = y1; i <= y2; i++)
                    {
                        itu = (i == y1) ? m1 : 1;
                        iden = (i == y2) ? m2 : 12;
                        for (int j = itu; j <= iden; j++)
                        {
                            sql = "select * from (";
                            sql += "select a.maql as maql,a.mabn, case when a.done is null then '' else a.done end as done,";
                            sql += "\ncase when a.done in ('?') then 'Đi làm CLS' else case when a.done='d' then 'Có kết quả' else case when a.noitiepdon=16 then 'Tái khám' else 'Chờ khám' end end end as tk,";
                            sql += "\na.makp, d.tenkp,to_char(a.ngay,'mmddyy:hh') as ngayvao,case when a.doituonguutien is null then 0 else a.doituonguutien end as doituonguutien,medibv.khongdaucokhoangtrang(b.hoten) as hotenkhongdau,";
                            sql += "\nhoten,ngaysinh,case when b.phai=0 then 'Nam' else 'Nữ' end as gioitinh,c.sothe as mathe, c.tungay, c.denngay, a.mavaovien, a.sovaovien, a.madoituong";
                            sql += "\nfrom xxx.tiepdon a inner join medibv.btdbn b on a.mabn=b.mabn";
                            sql += "\nleft join xxx.bhyt c on a.maql = c.maql";
                            sql += "\ninner join medibv.btdkp_bv d on a.makp = d.makp where d.loai = 1 ";
                            string strMakp = "";
                            if (s_makp != "" && s_makp != null)
                            {
                                string s = s_makp.Replace(",", "','");
                                //sql += " and a.makp in ('" + s.Substring(0, s.Length - 3) + "')";
                                if (s.Length > 3) s = s.Substring(0, s.Length - 3);
                                {
                                    sql += " and a.makp in ('" + s + "') ";
                                    strMakp += " and a.makp in ('" + s + "') ";
                                }
                            }
                            //if (mabn != "" && mabn != null)
                            //{
                            //    sql += " and a.mabn='" + mabn + "%'";
                            //}
                            if (s_tungay != "" && s_tungay != null)
                            {                                
                                s_tungay = s_tungay.Substring(6,4) + s_tungay.Substring(3,2) + s_tungay.Substring(0,2); 
                                sql += " and to_char(a.ngay,'yyyymmdd') = " + s_tungay + ")";
                            }
                            //if (mabs != "" && mabs != null)
                            //{
                            //    sql += "and a.mabs='" + mabs + "'";
                            //}
                            mmyy = j.ToString().PadLeft(2, '0') + i.ToString().Substring(2, 2);
                            //if (bMmyy(mmyy))
                            sql += " where (done is null or done = '')";
                            DataSet ds = new DataSet();
                            sql = sql.Replace("xxx", "medibv" + mmyy);
                            ds = dbHelper.getDataSetbySql(sql);
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    Benhnhan item = new Benhnhan();
                                    item.maql = dr["maql"].ToString();
                                    item.mabn = dr["mabn"].ToString();
                                    item.tenbn = dr["hoten"].ToString();
                                    item.makp = dr["makp"].ToString();
                                    item.tenkp = dr["tenkp"].ToString();
                                    item.ngaysinh = dr["ngaysinh"].ToString();
                                    item.gioitinh = dr["gioitinh"].ToString();
                                    item.mathe = dr["mathe"].ToString();
                                    item.ngayvao = dr["ngayvao"].ToString();
                                    item.tungaybhyt = dr["tungay"].ToString();
                                    item.denngaybhyt = dr["denngay"].ToString();
                                    item.mavaovien = dr["mavaovien"].ToString();
                                    item.sovaovien = dr["sovaovien"].ToString();
                                    item.madoituong = dr["madoituong"].ToString();
                                    lts.Add(item);
                                }
                            }
                        }
                    }
                    if (lts != null && lts.Count > 0)
                    {
                        return lts;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (loai != "x")
                {
                    //Npgsql.NpgsqlConnection connct = new NpgsqlConnection(ConStr);
                    //connct.Open();
                    for (int i = y1; i <= y2; i++)
                    {
                        itu = (i == y1) ? m1 : 1;
                        iden = (i == y2) ? m2 : 12;
                        for (int j = itu; j <= iden; j++)
                        {
                            sql = "select a.maql as maql,a.mabn, a.ttlucrv,";
                            sql += "\ncase when a.ttlucrv in (0) then 'Đi làm CLS' else 'Đã khám' end as tk,";
                            sql += "\na.makp, e.tenkp,to_char(a.ngay,'mmddyy:hh') as ngayvao,case when d.doituonguutien is null then 0 else d.doituonguutien end as doituonguutien,medibv.khongdaucokhoangtrang(b.hoten) as hotenkhongdau,";
                            sql += "\nhoten,ngaysinh,case when b.phai=0 then 'Nam' else 'Nữ' end as gioitinh,c.sothe as mathe,a.mabs, a.mavaovien,c.tungay, c.denngay, a.sovaovien, a.madoituong";
                            sql += "\nfrom xxx.benhanpk a inner join medibv.btdbn b on a.mabn=b.mabn";
                            sql += "\nleft join xxx.bhyt c on a.maql = c.maql";
                            sql += "\nleft join xxx.tiepdon d on a.maql = d.maql";
                            sql += "\ninner join medibv.btdkp_bv e on a.makp = e.makp where e.loai = 1";
                            string strMakp = "";
                            if (s_makp != "" && s_makp != null)
                            {
                                string s = s_makp.Replace(",", "','");
                                //sql += " and a.makp in ('" + s.Substring(0, s.Length - 3) + "')";
                                if (s.Length > 3) s = s.Substring(0, s.Length - 3);
                                {
                                    sql += " and a.makp in ('" + s + "') ";
                                    strMakp += " and a.makp in ('" + s + "') ";
                                }
                            }
                            //if (mabn != "" && mabn != null)
                            //{
                            //    sql += " and a.mabn='%" + mabn + "%'";
                            //}
                            mmyy = j.ToString().PadLeft(2, '0') + i.ToString().Substring(2, 2);
                            //if (bMmyy(mmyy))                            
                            //if (mabs != "" && mabs != null)
                            //{
                            //    sql += "and a.mabs='" + mabs + "'";
                            //}
                            if (loai == "?")
                            {
                                sql += " and a.ttlucrv=0";
                            }
                            if (loai == "d")
                            {
                                sql += " and a.ttlucrv!=0";
                            }
                            if (s_tungay != "" && s_tungay != null)
                            {
                                sql += " and to_char(a.ngay,'yyyymmdd') between to_char(to_date('" + s_tungay + "','dd/mm/yyyy'),'yyyymmdd') and to_char(to_date('" + s_denngay + "','dd/mm/yyyy'),'yyyymmdd')";
                            }
                            DataSet ds = new DataSet();
                            sql = sql.Replace("xxx", "medibv" + mmyy);
                            ds = dbHelper.getDataSetbySql(sql);
                            if (ds != null && ds.Tables[0].Rows.Count > 0)
                            {
                                foreach (DataRow dr in ds.Tables[0].Rows)
                                {
                                    Benhnhan item = new Benhnhan();
                                    item.maql = dr["maql"].ToString();
                                    item.mabn = dr["mabn"].ToString();
                                    item.tenbn = dr["hoten"].ToString();
                                    item.makp = dr["makp"].ToString();
                                    item.tenkp = dr["tenkp"].ToString();
                                    item.ngaysinh = dr["ngaysinh"].ToString();
                                    item.gioitinh = dr["gioitinh"].ToString();
                                    item.mathe = dr["mathe"].ToString();
                                    item.ngayvao = dr["ngayvao"].ToString();
                                    item.mavaovien = dr["mavaovien"].ToString();
                                    item.tungaybhyt = dr["tungay"].ToString();
                                    item.denngaybhyt = dr["denngay"].ToString();
                                    item.sovaovien = dr["sovaovien"].ToString();
                                    item.madoituong = dr["madoituong"].ToString();
                                    lts.Add(item);
                                }
                            }
                        }
                    }
                    if (lts != null && lts.Count > 0)
                    {
                        return lts;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        public class Benhanpk
        {
            public string mabn { get; set; }
            public string mavaovien { get; set; }
            public string maql { get; set; }
            public string tenbn { get; set; }
            public string ngaysinh { get; set; }
            public string gioitinh { get; set; }
            public string mathe { get; set; }
            public string ngayvao { get; set; }
            public string makp { get; set; }
            public string tungaybhyt { get; set; }
            public string denngaybhyt { get; set; }
            public string chandoan { get; set; }
            public string mabs { get; set; }
            public string sovaovien { get; set; }
            public string userid { get; set; }
            public string madoituong { get; set; }
            public string ipcomputer { get; set; }
        }

        public static DateTime StringToDate(string ngay)
        {
            DateTime dt = DateTime.ParseExact(ngay, "dd/MM/yyyy", new CultureInfo("vi-VN"));
            Console.WriteLine("Year: {0}, Month: {1}, Day: {2}", dt.Year, dt.Month, dt.Day);
            return dt;
        }

        public class Login
        {
            public string userid { get; set; }
            public string password { get; set; }
            public string id { get; set; }
            public string hoten { get; set; }
            public string makp { get; set; }
            public string mabs { get; set; }
            public string tenkp { get; set; }

            public static IEnumerable<Login> Get()
            {
                try
                {
                    DataSet ds = new DataSet();
                    List<Login> lts = new List<Login>();
                    string sql = "";
                    sql = @"select a.id,a.hoten,a.password_,a.userid,c.makp,mabs,b.hoten as hotenbs,c.tenkp from medibv.dlogin a
                            left join medibv.dmbs b on a.mabs = b.ma
                            left join medibv.btdkp_bv c on (case when substring(a.makp,length(a.makp),1) = ',' then replace(a.makp,',','') else a.makp end) = c.makp
                            where 1=1";
                    ds = dbHelper.getDataSetbySql(sql);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Login item = new Login();
                            item.userid = dr["userid"].ToString();
                            item.password = dr["password_"].ToString();
                            item.id = dr["id"].ToString();
                            item.hoten = dr["hotenbs"].ToString();
                            item.makp = dr["makp"].ToString();
                            item.mabs = dr["mabs"].ToString();
                            item.tenkp = dr["tenkp"].ToString();
                            lts.Add(item);
                        }
                    }
                    return lts;
                }
                catch
                {
                    return null;
                }
            }
        }

        public class Bacsi
        {
            public string mabs { get; set; }
            public string hoten { get; set; }
            public string makp { get; set; }

            public static IEnumerable<Bacsi> Getall()
            {
                try
                {
                    DataSet ds = new DataSet();
                    List<Bacsi> lts = new List<Bacsi>();
                    string sql = "select ma as mabs,hoten,makp from medibv.dmbs where 1=1 limit 50 ";
                    ds = dbHelper.getDataSetbySql(sql);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Bacsi item = new Bacsi();
                            item.mabs = dr["mabs"].ToString();
                            item.hoten = dr["hoten"].ToString();
                            item.makp = dr["makp"].ToString();
                            lts.Add(item);
                        }
                    }
                    return lts;
                }
                catch
                {
                    return null;
                }
            }
            public static IEnumerable<Bacsi> Getallbytenbs(string hoten, string mabs)
            {
                try
                {
                    DataSet ds = new DataSet();
                    List<Bacsi> lts = new List<Bacsi>();
                    string sql = "select ma as mabs,hoten,makp from medibv.dmbs where 1=1 and hoten like '%" + hoten + "%' or ma like '%" + mabs + "%' limit 200";
                    ds = dbHelper.getDataSetbySql(sql);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Bacsi item = new Bacsi();
                            item.mabs = dr["mabs"].ToString();
                            item.hoten = dr["hoten"].ToString();
                            item.makp = dr["makp"].ToString();
                            lts.Add(item);
                        }
                    }
                    return lts;
                }
                catch
                {
                    return null;
                }
            }

        }

        public class DataPostBenhnhan
        {
            public string s_makp { get; set; }
            public string s_tungay { get; set; }
            public string s_denngay { get; set; }
            public string s_loai { get; set; }
            public string s_mabn { get; set; }
            public string s_mabs { get; set; }
        }

        public class Khoaphong
        {
            public string makp { get; set; }
            public string ten { get; set; }

            public static IEnumerable<Khoaphong> Getall()
            {
                try
                {
                    DataSet ds = new DataSet();
                    List<Khoaphong> lts = new List<Khoaphong>();
                    string sql = "select makp,tenkp from medibv.btdkp_bv where loai=1";
                    ds = dbHelper.getDataSetbySql(sql);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Khoaphong item = new Khoaphong();
                            item.makp = dr["makp"].ToString();
                            item.ten = dr["tenkp"].ToString();
                            lts.Add(item);
                        }
                    }
                    return lts;
                }
                catch
                {
                    return null;
                }
            }

            public static IEnumerable<Khoaphong> Getallbytenkp(string makp, string ten, string search)
            {
                try
                {
                    DataSet ds = new DataSet();
                    List<Khoaphong> lts = new List<Khoaphong>();
                    string sql = "select makp,tenkp from medibv.btdkp_bv where loai=1 and (makp like '%" + search + "%' or tenkp like '%" + ten + "%') ";
                    if (makp != "" && makp != null)
                    {
                        string s = makp.Replace(",", "','");
                        //sql += " and a.makp in ('" + s.Substring(0, s.Length - 3) + "')";
                        if (s.Length > 3) s = s.Substring(0, s.Length - 3);
                        {
                            sql += " and makp in ('" + s + "') ";
                            makp += " and a.makp in ('" + s + "') ";
                        }
                    }
                    ds = dbHelper.getDataSetbySql(sql);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Khoaphong item = new Khoaphong();
                            item.makp = dr["makp"].ToString();
                            item.ten = dr["tenkp"].ToString();
                            lts.Add(item);
                        }
                    }
                    return lts;
                }
                catch
                {
                    return null;
                }
            }
        }

        public class Doituong
        {
            public string madoituong { get; set; }
            public string doituong { get; set; }

            public static IEnumerable<Doituong> Getall()
            {
                try
                {
                    DataSet ds = new DataSet();
                    List<Doituong> lts = new List<Doituong>();
                    string sql = "select madoituong,doituong from medibv.doituong where 1=1 ";
                    ds = dbHelper.getDataSetbySql(sql);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Doituong item = new Doituong();
                            item.madoituong = dr["madoituong"].ToString();
                            item.doituong = dr["doituong"].ToString();
                            lts.Add(item);
                        }
                    }
                    return lts;
                }
                catch
                {
                    return null;
                }
            }
        }

        public class DataPostVienphi
        {
            public string noidung { get; set; }
        }

        public class Vienphi
        {
            public string id { get; set; }
            public string ma { get; set; }
            public string ten { get; set; }
            public string tennhom { get; set; }
            public string dvt { get; set; }
            public string gia_th { get; set; }
            public string gia_bh_t3 { get; set; }
            public string gia_bh_t7 { get; set; }
            public string gia_dv { get; set; }

            public static IEnumerable<Vienphi> Getall(string search)
            {
                try
                {
                    DataSet ds = new DataSet();
                    List<Vienphi> lts = new List<Vienphi>();
                    string sql = "select a.id, a.ma, a.ten, c.ten as tennhom, a.dvt, a.gia_dv ,a.gia_th ,a.gia_bh_t3, a.gia_bh_t7 from medibv.v_giavp a ";
                    sql += "left join medibv.v_loaivp b on a.id_loai = b.id ";
                    sql += "left join medibv.v_nhomvp c on b.id_nhom = c.ma where 1=1 and lower(a.ten) like lower('%" + search + "%') limit 50";
                    ds = dbHelper.getDataSetbySql(sql);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Vienphi item = new Vienphi();
                            item.id = dr["id"].ToString();
                            item.ma = dr["ma"].ToString();
                            item.ten = dr["ten"].ToString();
                            item.tennhom = dr["tennhom"].ToString();
                            item.dvt = dr["dvt"].ToString();
                            item.gia_th = dr["gia_th"].ToString();
                            item.gia_dv = dr["gia_dv"].ToString();
                            item.gia_bh_t7 = dr["gia_bh_t7"].ToString();
                            item.gia_bh_t7 = dr["gia_bh_t3"].ToString();
                            lts.Add(item);
                        }
                    }
                    return lts;
                }
                catch
                {
                    return null;
                }
            }
        }

        public class ThongtinChidinh
        {
            public string mabn { get; set; }
            public string hoten { get; set; }
            public string namsinh { get; set; }
            public string phai { get; set; }
            public string diachi { get; set; }
            public string mavaovien { get; set; }
            public string maql { get; set; }
            public string mabs { get; set; }
            public string tenbs { get; set; }
            public string makp { get; set; }
            public string tenkp { get; set; }
            public string madoituong { get; set; }
            public string doituong { get; set; }
            public string maicd { get; set; }
            public string chandoan { get; set; }
            public string tungaybhyt { get; set; }
            public string denngaybhyt { get; set; }
            public IEnumerable<Chidinh> chidinh { get; set; }

            public ThongtinChidinh(DataPost.Chidinh data)
            {
                string sqlQuery = "";
                DataSet ds = new DataSet();
                string schema = "medibv" + data.ngay;
                sqlQuery = "select distinct a.mavaovien,a.maql,b.mabn,b.hoten,case when b.ngaysinh != null then to_char(b.ngaysinh,'yyyy') else b.namsinh end as namsinh";
                sqlQuery += "\n, a.mabs, d.hoten as tenbs, a.makp, e.tenkp as tenkp,c.ten as phai, b.sonha||(case when b.thon != '' then ','||b.thon else '' end) as diachi";
                sqlQuery += "\n, a.madoituong,f.doituong,a.maicd,case when a.chandoan = '' or a.chandoan is null then g.ten else a.chandoan end as chandoan";
                sqlQuery += "\n, to_char(h.tungay,'dd/mm/yyyy') as tungay, to_char(h.denngay,'dd/mm/yyyy') as denngay";
                sqlQuery += "\nfrom " + schema + ".benhanpk a inner join medibv.btdbn b on b.mabn = a.mabn left join medibv.dmphai c on c.ma = b.phai";
                sqlQuery += "\nleft join medibv.dmbs d on d.ma = a.mabs left join medibv.btdkp_bv e on e.makp = a.makp left join medibv.doituong f on f.madoituong = a.madoituong";
                sqlQuery += "\nleft join " + schema + ".trieuchung g on g.maql = a.maql";
                sqlQuery += "\nleft join " + schema + ".bhyt h on a.maql = h.maql";
                sqlQuery += "\nwhere a.maql=" + data.maql;
                ds = dbHelper.getDataSetbySql(sqlQuery);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    this.mabn = ds.Tables[0].Rows[0]["mabn"].ToString();
                    this.mavaovien = ds.Tables[0].Rows[0]["mavaovien"].ToString();
                    this.maql = ds.Tables[0].Rows[0]["maql"].ToString();
                    this.hoten = ds.Tables[0].Rows[0]["hoten"].ToString();
                    this.namsinh = ds.Tables[0].Rows[0]["namsinh"].ToString();
                    this.phai = ds.Tables[0].Rows[0]["phai"].ToString();
                    this.diachi = ds.Tables[0].Rows[0]["diachi"].ToString();
                    this.makp = ds.Tables[0].Rows[0]["makp"].ToString();
                    this.tenkp = ds.Tables[0].Rows[0]["tenkp"].ToString();
                    this.mabs = ds.Tables[0].Rows[0]["mabs"].ToString();
                    this.tenbs = ds.Tables[0].Rows[0]["tenbs"].ToString();
                    this.madoituong = ds.Tables[0].Rows[0]["madoituong"].ToString();
                    this.doituong = ds.Tables[0].Rows[0]["doituong"].ToString();
                    this.maicd = ds.Tables[0].Rows[0]["maicd"].ToString();
                    this.chandoan = ds.Tables[0].Rows[0]["chandoan"].ToString();
                    this.tungaybhyt = ds.Tables[0].Rows[0]["tungay"].ToString();
                    this.denngaybhyt = ds.Tables[0].Rows[0]["denngay"].ToString();
                }
                this.chidinh = Chidinh.Getallbymaql(data);
            }
        }

        public class Chidinh
        {
            public string tenkp { get; set; }
            public string noidung { get; set; }
            public string dvt { get; set; }
            public string soluong { get; set; }
            public string dongia { get; set; }
            public string doituong { get; set; }
            public string phanloai { get; set; }
            public string ghichu { get; set; }
            public string id { get; set; }
            public string mabn { get; set; }
            public string mavaovien { get; set; }
            public string maql { get; set; }
            public string ngay { get; set; }
            public string gio { get; set; }
            public string loai { get; set; }
            public string makp { get; set; }
            public string madoituong { get; set; }
            public string mavp { get; set; }
            public string paid { get; set; }
            public string tinhtrang { get; set; }
            public string thuchien { get; set; }
            public string computer { get; set; }
            public string userid { get; set; }
            public string ngayup { get; set; }
            public string mmyy { get; set; }
            public string idchidinh { get; set; }
            public string idttrv { get; set; }
            public string loaiba { get; set; }
            public string maicd { get; set; }
            public string chandoan { get; set; }
            public string mabs { get; set; }
            public string tenbs { get; set; }
            public string thanhtien { get; set; }
            public string traituyen { get; set; }
            public string done { get; set; }

            public static IEnumerable<Chidinh> Getallbymaql(DataPost.Chidinh chidinh)
            {
                try
                {
                    DataSet ds = new DataSet();
                    List<Chidinh> lts = new List<Chidinh>();
                    string schema = "medibv" + chidinh.ngay;
                    //sql = sql.Replace("xxx", mmyy);
                    string sql = "select f.hoten as tenbs, e.tenkp, b.ten as noidung, b.dvt, a.soluong, a.dongia, d.doituong,c.ten as phanloai,a.ghichu, a.id, a.mabn, " +
                        "a.mavaovien, a.maql, to_char(a.ngay,'dd/mm/yyyy') as ngay,to_char(a.ngay,'hh24:mi') as gio, a.loai, a.makp, a.madoituong, a.mavp, a.paid, a.tinhtrang, a.thuchien, a.computer, " +
                        "a.userid, a.ngayud, a.mmyy, a.idchidinh, a.idttrv, a.loaiba, a.maicd, a.chandoan, a.mabs, " +
                        "round(a.soluong*a.dongia,2) as thanhtien,a.done,a.mavp " +
                        "from " + schema + ".v_chidinh a " +
                        "left join medibv.v_giavp b on a.mavp = b.id " +
                        "left join medibv.v_loaivp c on b.id_loai = c.id " +
                        "left join medibv.doituong d on a.madoituong = d.madoituong " +
                        "left join medibv.btdkp_bv e on a.makp = e.makp " +
                        "left join medibv.dmbs f on a.mabs = f.ma " +
                        "where maql=" + chidinh.maql;
                    //if (chidinh.mavp != "" && chidinh.mavp != null)
                    //{
                    //    sql += " and mavp=" + chidinh.mavp;
                    //}
                    string sqlLower = sql.ToLower();
                    ds = dbHelper.getDataSetbySql(sql);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            Chidinh item = new Chidinh();
                            item.noidung = dr["noidung"].ToString();
                            item.dvt = dr["dvt"].ToString();
                            item.soluong = dr["soluong"].ToString();
                            item.dongia = dr["dongia"].ToString();
                            item.doituong = dr["doituong"].ToString();
                            item.phanloai = dr["phanloai"].ToString();
                            item.ghichu = dr["ghichu"].ToString();
                            item.id = dr["id"].ToString();
                            item.mabn = dr["mabn"].ToString();
                            item.mavaovien = dr["mavaovien"].ToString();
                            item.maql = dr["maql"].ToString();
                            item.ngay = dr["ngay"].ToString();
                            item.gio = dr["gio"].ToString();
                            item.loai = dr["loai"].ToString();
                            item.makp = dr["makp"].ToString();
                            item.madoituong = dr["madoituong"].ToString();
                            item.mavp = dr["mavp"].ToString();
                            item.paid = dr["paid"].ToString();
                            item.tinhtrang = dr["tinhtrang"].ToString();
                            item.thuchien = dr["thuchien"].ToString();
                            item.computer = dr["computer"].ToString();
                            item.userid = dr["userid"].ToString();
                            item.ngayup = dr["ngayud"].ToString();
                            item.chandoan = dr["chandoan"].ToString();
                            item.idchidinh = dr["idchidinh"].ToString();
                            item.idttrv = dr["idttrv"].ToString();
                            item.loaiba = dr["loaiba"].ToString();
                            item.maicd = dr["maicd"].ToString();
                            item.chandoan = dr["chandoan"].ToString();
                            item.mabs = dr["mabs"].ToString();
                            item.tenkp = dr["tenkp"].ToString();
                            item.tenbs = dr["tenbs"].ToString();
                            item.thanhtien = dr["thanhtien"].ToString();
                            item.mmyy = dr["mmyy"].ToString();
                            item.done = dr["done"].ToString();
                            lts.Add(item);
                        }
                    }
                    return lts;
                }
                catch
                {
                    return null;
                }
            }

            public static string f_get_id_seq(int v_length)
            {
                string areturn = "";
                try
                {
                    string atmp = "";
                    atmp += dbHelper.getDataSetbySql("select to_char(now(),'yymmddhhmissms') as id").Tables[0].Rows[0][0].ToString().Trim();
                    if (atmp.Length < v_length)
                    {
                        atmp += dbHelper.getDataSetbySql("select trunc(random() * 999 + 1)").Tables[0].Rows[0][0].ToString().Trim().PadLeft(v_length - atmp.Length, '0');
                    }
                    areturn = atmp.Substring(0, v_length);
                }
                catch
                {
                }
                return areturn;
            }

            public static Data.ThongtinChidinh InsertChidinh(Data.Chidinh data)
            {
                try
                {
                    string mmyyupdate = "medibv" + data.mmyy;
                    //string sql = "select soluong,id,mavp,dongia from " + mmyyupdate + ".v_chidinh where id=" + data.id + " and maql = " + data.maql + " and mavp=" + data.mavp;
                    //DataSet dt = new DataSet();
                    //dt = dbHelper.getDataSetbySql(sql);
                    //string maql = dt.Tables[0].Rows[0]["maql"].ToString();
                    //string id = dt.Tables[0].Rows[0]["id"].ToString();
                    //string mavp = dt.Tables[0].Rows[0]["mavp"].ToString();
                    //string dongia = dt.Tables[0].Rows[0]["dongia"].ToString();
                    //string sql = String.Format("UPDATE {26}.v_chidinh SET mabn='{0}', mavaovien={1}, maql={2}, loai={3}, makp='{4}'," +
                    //"madoituong={5}, soluong={6}, dongia={7}, paid={8}, tinhtrang={9}, thuchien={10}, computer='{11}', userid={12}," +
                    //"ngayud='{13}', mmyy='{14}', idchidinh={15}, idttrv={16}, loaiba={17}, maicd='{18}', chandoan='{19}', mabs='{22}', " +
                    //"traituyen={23}" +
                    //"WHERE 1=1 and id={24} and mavp={25}", data.mabn, data.mavaovien, data.maql, data.loai, data.makp, data.madoituong, data.soluong,
                    //data.dongia, data.paid, data.tinhtrang, data.thuchien, data.computer, data.userid, DateTime.Now, data.mmyy, data.idchidinh, data.idttrv,
                    //data.loaiba, data.maicd, data.chandoan, data.mabs, data.traituyen, data.id, data.mavp, mmyyupdate);
                    string sql = @"UPDATE " + mmyyupdate + ".v_chidinh SET " +
                        "\nmabn='" + data.mabn + "'" + ",mavaovien=" + data.mavaovien + ",maql=" + data.maql + ",makp='" + data.makp + "'" +
                        "\n,madoituong=" + data.madoituong + ",soluong=" + data.soluong + ",dongia=" + data.dongia + ",paid=" + data.paid +
                        "\n,tinhtrang=" + data.tinhtrang + ",thuchien=" + data.thuchien + ",computer='" + data.computer + "'" + ",userid=" + data.userid +
                        "\n,ngayud=now()" + ",mmyy='" + data.mmyy + "'" + ",idchidinh=" + data.idchidinh + ",idttrv=" + data.idttrv +
                        "\n,loaiba=" + data.loaiba + ",maicd='" + data.maicd + "'" + ",chandoan='" + data.chandoan + "'" + ",mabs='" + data.mabs + "'" + ",traituyen=" + data.traituyen +
                        "\nwhere 1=1 " + "and id=" + data.id;
                    if (!dbHelper.ExecuteQuery(sql))
                    {
                        //string id = f_get_id_seq(18);
                        //string idchidinh = data.mavaovien;
                        string mmyyinsert = "medibv" + funcmmyy(DateTime.Today.ToString("dd/MM/yyyy"));
                        string loai = "2";
                        string loaiba = "3";
                        string sqlinsert = String.Format("INSERT INTO {0}.v_chidinh (" +
                            "id, mabn, mavaovien, maql, ngay, loai, makp, madoituong, mavp, soluong," +
                            "dongia, computer, userid, ngayud, mmyy, idchidinh, " +
                            "loaiba, maicd, chandoan, mabs) " +
                        "VALUES ("
                        + "{1},'{2}',{3},{4},{5},{6},'{7}',{8},{9},{10},"
                        + "{11},'{12}',{13},{14},'{15}',{16},"
                        + "{17},'{18}','{19}','{20}')",
                        mmyyinsert, data.id, data.mabn, data.mavaovien, data.maql, "now()", loai, data.makp, data.madoituong, data.mavp,
                        data.soluong, data.dongia, data.computer, data.userid, "now()", data.mmyy, data.idchidinh,
                        loaiba, data.maicd, data.chandoan, data.mabs);//
                        if (dbHelper.ExecuteQuery(sqlinsert))
                        {
                            return new Data.ThongtinChidinh(new DataPost.Chidinh(data.maql, funcmmyy(DateTime.Today.ToString("dd/MM/yyyy"))));
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return new Data.ThongtinChidinh(new DataPost.Chidinh(data.maql, funcmmyy(DateTime.Today.ToString("dd/MM/yyyy"))));
                    }
                }
                catch
                {
                    return null;
                }
            }
        }

        public class XetNghiem
        {
            public string mabn { get; set; }
            public string tungay { get; set; }
            public string denngay { get; set; }
            public string check { get; set; }
            public string nhomxn { get; set; }
            //public string id { get; set; }
            //public string mabn { get; set; }
            //public string so_ma { get; set; }
            //public string so_ten { get; set; }
            //public string sophieu { get; set; }
            //public string nhom_ma { get; set; }
            //public string nhom_ten { get; set; }
            //public string ten_ma { get; set; }
            //public string ten_ten { get; set; }
            //public string ngay { get; set; }
            //public string _laymau { get; set; }

            public static DataTable Getvalue(XetNghiem xetnghiem)
            {
                DateTime dt1 = StringToDate(xetnghiem.tungay);
                DateTime dt2 = StringToDate(xetnghiem.denngay);
                string mabn = xetnghiem.mabn;
                //  d2.Day.ToString().PadLeft(2, '0') + "/" + d2.Month.ToString().PadLeft(2, '0') + "/" + d2.Year.ToString()
                int y1 = dt1.Year, m1 = dt1.Month;
                int y2 = dt2.Year, m2 = dt2.Month;
                int itu, iden;
                string mmyy = "";
                string sql = "select * from (", sqlngay = "";
                DataSet ds = new DataSet();
                DataSet dsngay = new DataSet();
                DataTable dt = new DataTable();
                for (int i = y1; i <= y2; i++)
                {
                    itu = (i == y1) ? m1 : 1;
                    iden = (i == y2) ? m2 : 12;
                    for (int j = itu; j <= iden; j++)
                    {
                        mmyy = j.ToString().PadLeft(2, '0') + i.ToString().Substring(2, 2);
                        if (sql != "select * from (")
                        {
                            sql += "\nunion all\n";
                        }
                        sql += string.Format(@"select distinct * from (select a.id,c.stt, c.id_bv_ten,to_number(to_char(ngaylaymau,'yyyyddmm')) as _laymau,to_char(a.ngaylaymau,'dd/mm/yyyy hh24:mi') as ngay, to_char(a.ngaylaymau,'HH:MM AM') as gio,f.stt as stt_so, f.ten as nhomxn, d.ma,  d.ten as tenxn, b.ketqua, b.ghichu,d.id as id_ten, d.csbt_nam, d.csbt_nu, d.mincsbt_nam, d.maxcsbt_nam,  d.mincsbt_nu, d.maxcsbt_nu, g.ten as donvi,e.ten as tenvp
                            from xxx.xn_phieu a 
                            inner join xxx.xn_ketqua b on a.id=b.id 
                            inner join medibv.xn_bv_chitiet c on b.id_ten=c.id 
                            inner join medibv.xn_ten d on c.id_ten=d.id 
                            inner join medibv.xn_bv_ten e on c.id_bv_ten=e.id 
                            inner join medibv.xn_bv_so f on e.id_bv_so=f.id 
                            left join medibv.xn_donvi g on d.donvi= g.id where a.mabn={0}
                            union all 
                            select a.id,c.stt, c.id_bv_ten,to_number(to_char(ngaylaymau,'yyyyddmm')) as _laymau,to_char(a.ngaylaymau,'dd/mm/yyyy hh24:mi') as ngay, to_char(a.ngaylaymau,'HH:MM AM') as gio,f.stt as stt_so, f.ten as nhomxn, d.ma,  d.ten as tenxn, b.ketqua, b.ghichu,d.id as id_ten, d.csbt_nam, d.csbt_nu, d.mincsbt_nam, d.maxcsbt_nam,  d.mincsbt_nu, d.maxcsbt_nu, g.ten as donvi,e.ten as tenvp
                            from xxx.xn_phieu a 
                            inner join xxx.xn_ketqua b on a.id=b.id 
                            inner join medibv.xn_bv_chitiet c on b.id_ten=c.id 
                            inner join medibv.xn_ten d on c.id_ten=d.id 
                            inner join medibv.xn_bv_ten e on c.id_bv_ten=e.id 
                            inner join medibv.xn_bv_so f on e.id_bv_so=f.id 
                            left join medibv.xn_donvi g on d.donvi= g.id 
                            where a.mabn={0} and b.ketqua_hople=1 order by stt_so,id_bv_ten,stt)", mabn);
                        // or b.id_ten in(77,101)) 
                        sql = sql.Replace("xxx", "medibv" + mmyy);
                        if (sqlngay != "")
                        {
                            sqlngay += "\nunion all\n";
                        }
                        sqlngay += string.Format(@"select * from (select distinct ngay,_laymau from
                            (select a.id,c.stt, c.id_bv_ten,to_number(to_char(ngaylaymau,'yyyyddmm')) as _laymau,to_char(a.ngaylaymau,'dd/mm/yyyy hh24:mi') as ngay, to_char(a.ngaylaymau,'HH:MM AM') as gio,f.stt as stt_so, f.ten as nhomxn, d.ma,  d.ten as tenxn, b.ketqua, b.ghichu,d.id as id_ten, d.csbt_nam, d.csbt_nu, d.mincsbt_nam, d.maxcsbt_nam,  d.mincsbt_nu, d.maxcsbt_nu, g.ten as donvi,e.ten as tenvp
                            from xxx.xn_phieu a 
                            inner join xxx.xn_ketqua b on a.id=b.id 
                            inner join medibv.xn_bv_chitiet c on b.id_ten=c.id 
                            inner join medibv.xn_ten d on c.id_ten=d.id 
                            inner join medibv.xn_bv_ten e on c.id_bv_ten=e.id 
                            inner join medibv.xn_bv_so f on e.id_bv_so=f.id 
                            left join medibv.xn_donvi g on d.donvi= g.id where a.mabn={0}
                            union all select a.id,c.stt, c.id_bv_ten,to_number(to_char(ngaylaymau,'yyyyddmm')) as _laymau,to_char(a.ngaylaymau,'dd/mm/yyyy hh24:mi') as ngay, to_char(a.ngaylaymau,'HH:MM AM') as gio,f.stt as stt_so, f.ten as nhomxn, d.ma,  d.ten as tenxn, b.ketqua, b.ghichu,d.id as id_ten, d.csbt_nam, d.csbt_nu, d.mincsbt_nam, d.maxcsbt_nam,  d.mincsbt_nu, d.maxcsbt_nu, g.ten as donvi,e.ten as tenvp
                            from xxx.xn_phieu a 
                            inner join xxx.xn_ketqua b on a.id=b.id 
                            inner join medibv.xn_bv_chitiet c on b.id_ten=c.id 
                            inner join medibv.xn_ten d on c.id_ten=d.id 
                            inner join medibv.xn_bv_ten e on c.id_bv_ten=e.id 
                            inner join medibv.xn_bv_so f on e.id_bv_so=f.id 
                            left join medibv.xn_donvi g on d.donvi= g.id 
                            where a.mabn={0} and b.ketqua_hople=1)      
                            order by ngay desc) ", mabn);
                        sqlngay = sqlngay.Replace("xxx", "medibv" + mmyy);
                    }
                    sql += ") where 1=1";
                    if (xetnghiem.nhomxn != null && xetnghiem.nhomxn != "")
                    {
                        sql += " and nhomxn in ('" + xetnghiem.nhomxn + "')";
                    }
                    sql += " order by tenvp";
                    ds = dbHelper.getDataSetbySql(sql);
                    dsngay = dbHelper.getDataSetbySql(sqlngay);
                    string tenvp = "";
                    dt.Columns.Add(new DataColumn("ma", typeof(string)));
                    dt.Columns.Add(new DataColumn("ten", typeof(string)));
                    dt.Columns.Add(new DataColumn("gtbtminnam", typeof(string)));
                    dt.Columns.Add(new DataColumn("gtbtmaxnam", typeof(string)));
                    dt.Columns.Add(new DataColumn("gtbtminnu", typeof(string)));
                    dt.Columns.Add(new DataColumn("gtbtmaxnu", typeof(string)));
                    dt.Columns.Add(new DataColumn("donvi", typeof(string)));
                    dt.Columns.Add(new DataColumn("gio", typeof(string)));
                    dt.Columns.Add(new DataColumn("nhomxn", typeof(string)));
                    if (dsngay != null && dsngay.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drngay in dsngay.Tables[0].Rows)
                        {
                            dt.Columns.Add(new DataColumn(drngay["ngay"].ToString().Replace(" ", "-"), typeof(string)));
                        }
                    }
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        int @mID = 1;
                        foreach (DataRow drxetnghiem in ds.Tables[0].Rows)
                        {
                            DataRow dr = dt.NewRow();
                            bool newrow = false;
                            if (drxetnghiem["tenvp"].ToString() != tenvp)
                            {
                                tenvp = drxetnghiem["tenvp"].ToString();
                                string @newID = "";
                                if (@mID >= 1 && @mID <= 9)
                                {
                                    @newID = "00" + @mID;
                                }
                                if (@mID >= 10 && @mID <= 99)
                                {
                                    @newID = "0" + @mID;
                                }
                                if (@mID >= 100 && @mID <= 999)
                                {
                                    @newID = @mID.ToString();
                                }
                                dr["ma"] = @newID;
                                dr["ten"] = tenvp;
                                dt.Rows.Add(dr);
                                @mID++;
                                //dr = dt.NewRow();
                            }
                            if (dt.Select("ma = '" + drxetnghiem["ma"].ToString() + "'").Length > 0)
                            {
                                dr = dt.Select("ma = '" + drxetnghiem["ma"].ToString() + "'")[0];
                            }
                            else
                            {
                                newrow = true;
                                dr = dt.NewRow();
                            }
                            dr["ma"] = drxetnghiem["ma"].ToString();
                            dr["ten"] = drxetnghiem["tenxn"].ToString();
                            dr["gtbtminnam"] = drxetnghiem["mincsbt_nam"].ToString();
                            dr["gtbtmaxnam"] = drxetnghiem["maxcsbt_nam"].ToString();
                            dr["gtbtminnu"] = drxetnghiem["mincsbt_nu"].ToString();
                            dr["gtbtmaxnu"] = drxetnghiem["maxcsbt_nu"].ToString();
                            dr["donvi"] = drxetnghiem["donvi"].ToString();
                            dr["gio"] = drxetnghiem["gio"].ToString();
                            dr["nhomxn"] = drxetnghiem["nhomxn"].ToString();
                            dr[drxetnghiem["ngay"].ToString().Replace(" ", "-")] = drxetnghiem["ketqua"].ToString();

                            if (newrow == true)
                            {
                                dt.Rows.Add(dr);
                            }
                            //foreach (DataRow drngay in dsngay.Tables[0].Rows)
                            //{
                            //    if (drngay["_laymau"].ToString() == drxetnghiem["_laymau"].ToString())
                            //    {
                            //        dr[drngay["_laymau"].ToString()] = drxetnghiem["ketqua"].ToString();
                            //    }
                            //    else
                            //    {
                            //        dr[drngay["_laymau"].ToString()] = "";
                            //    }
                            //}                            

                        }
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }

            public static DataTable Getdsxetnghiem(XetNghiem xetnghiem)
            {
                DateTime dt1 = StringToDate(xetnghiem.tungay);
                DateTime dt2 = StringToDate(xetnghiem.denngay);
                string mabn = xetnghiem.mabn;
                //  d2.Day.ToString().PadLeft(2, '0') + "/" + d2.Month.ToString().PadLeft(2, '0') + "/" + d2.Year.ToString()
                int y1 = dt1.Year, m1 = dt1.Month;
                int y2 = dt2.Year, m2 = dt2.Month;
                int itu, iden;
                string mmyy = "";
                string sqlngay = "select * from (";
                List<Object> lts = new List<Object>();
                DataSet ds = new DataSet();
                DataSet dsngay = new DataSet();
                DataTable dt = new DataTable();
                for (int i = y1; i <= y2; i++)
                {
                    itu = (i == y1) ? m1 : 1;
                    iden = (i == y2) ? m2 : 12;
                    for (int j = itu; j <= iden; j++)
                    {
                        mmyy = j.ToString().PadLeft(2, '0') + i.ToString().Substring(2, 2);
                        if (sqlngay != "select * from (")
                        {
                            sqlngay += "\nunion all\n";
                        }
                        sqlngay += string.Format(@"select * from (select distinct ngay,_laymau from
                            (select a.id,c.stt, c.id_bv_ten,to_number(to_char(ngaylaymau,'yyyymmdd')) as _laymau,to_char(a.ngaylaymau,'dd/mm/yyyy hh24:mi') as ngay, to_char(a.ngaylaymau,'HH:MM AM') as gio,f.stt as stt_so, f.ten as nhomxn, d.ma,  d.ten as tenxn, b.ketqua, b.ghichu,d.id as id_ten, d.csbt_nam, d.csbt_nu, d.mincsbt_nam, d.maxcsbt_nam,  d.mincsbt_nu, d.maxcsbt_nu, g.ten as donvi,e.ten as tenvp
                            from xxx.xn_phieu a 
                            inner join xxx.xn_ketqua b on a.id=b.id 
                            inner join medibv.xn_bv_chitiet c on b.id_ten=c.id 
                            inner join medibv.xn_ten d on c.id_ten=d.id 
                            inner join medibv.xn_bv_ten e on c.id_bv_ten=e.id 
                            inner join medibv.xn_bv_so f on e.id_bv_so=f.id 
                            left join medibv.xn_donvi g on d.donvi= g.id where a.mabn={0}
                            union all select a.id,c.stt, c.id_bv_ten,to_number(to_char(ngaylaymau,'yyyymmdd')) as _laymau,to_char(a.ngaylaymau,'dd/mm/yyyy hh24:mi') as ngay, to_char(a.ngaylaymau,'HH:MM AM') as gio,f.stt as stt_so, f.ten as nhomxn, d.ma,  d.ten as tenxn, b.ketqua, b.ghichu,d.id as id_ten, d.csbt_nam, d.csbt_nu, d.mincsbt_nam, d.maxcsbt_nam,  d.mincsbt_nu, d.maxcsbt_nu, g.ten as donvi,e.ten as tenvp
                            from xxx.xn_phieu a 
                            inner join xxx.xn_ketqua b on a.id=b.id 
                            inner join medibv.xn_bv_chitiet c on b.id_ten=c.id 
                            inner join medibv.xn_ten d on c.id_ten=d.id 
                            inner join medibv.xn_bv_ten e on c.id_bv_ten=e.id 
                            inner join medibv.xn_bv_so f on e.id_bv_so=f.id 
                            left join medibv.xn_donvi g on d.donvi= g.id 
                            where a.mabn={0} and b.ketqua_hople=1)      
                            order by ngay desc) ", mabn);
                        sqlngay = sqlngay.Replace("xxx", "medibv" + mmyy);
                    }
                    sqlngay += ") where 1=1";
                    if (xetnghiem.tungay != null && xetnghiem.tungay != "" && xetnghiem.denngay != null && xetnghiem.denngay != "")
                    {
                        sqlngay += " and to_number(_laymau) between to_number(to_char(to_date('" + xetnghiem.tungay + "','dd/mm/yyyy'),'yyyymmdd')) and " + "to_number(to_char(to_date('" + xetnghiem.denngay + "','dd/mm/yyyy'),'yyyymmdd'))";
                    }
                    dsngay = dbHelper.getDataSetbySql(sqlngay);
                    dt.Columns.Add(new DataColumn("ngay", typeof(string)));
                    dt.Columns.Add(new DataColumn("_laymau", typeof(string)));
                    if (dsngay != null && dsngay.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow drngay in dsngay.Tables[0].Rows)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ngay"] = drngay["ngay"].ToString();
                            dr["_laymau"] = drngay["_laymau"].ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt;
                }
                else
                {
                    return null;
                }
            }
        }

        public class Chandoanhinhanh
        {
            public string ten { get; set; }
            public string ngay { get; set; }
            public string tenkt { get; set; }
            public string id { get; set; }
            public string mota { get; set; }
            public string ketluan { get; set; }

            public static IEnumerable<Chandoanhinhanh> Getvalue(DataPost.Chandoanhinhanh cdha)
            {
                DateTime dt1 = StringToDate(cdha.tungay);
                DateTime dt2 = StringToDate(cdha.denngay);

                //  d2.Day.ToString().PadLeft(2, '0') + "/" + d2.Month.ToString().PadLeft(2, '0') + "/" + d2.Year.ToString()
                int y1 = dt1.Year, m1 = dt1.Month;
                int y2 = dt2.Year, m2 = dt2.Month;
                int itu, iden;
                string mmyy = "";
                string sql = "select * from (";
                DataSet ds = new DataSet();
                List<Chandoanhinhanh> lts = new List<Chandoanhinhanh>();
                //DataTable dt = new DataTable();
                //dt.Columns.Add(new DataColumn("ten", typeof(string)));
                //dt.Columns.Add(new DataColumn("m_ngay", typeof(string)));
                //dt.Columns.Add(new DataColumn("tenkt", typeof(string)));
                //dt.Columns.Add(new DataColumn("id", typeof(string)));
                //dt.Columns.Add(new DataColumn("mota", typeof(string)));
                //dt.Columns.Add(new DataColumn("ketluan", typeof(string)));
                for (int i = y1; i <= y2; i++)
                {
                    itu = (i == y1) ? m1 : 1;
                    iden = (i == y2) ? m2 : 12;
                    for (int j = itu; j <= iden; j++)
                    {
                        mmyy = j.ToString().PadLeft(2, '0') + i.ToString().Substring(2, 2);
                        if (sql != "select * from (")
                        {
                            sql += "\nunion all\n";
                        }
                        sql += string.Format(@"select d.ten,to_char(a.ngayud,'dd/mm/yyyy HH24:mi') as m_ngay,c.tenkt,a.id,b.mota,b.ketluan
                        from xxx.cdha_bnll a
                        left join xxx.cdha_bnct b on a.id=b.id
                        left join medibv.cdha_kythuat c on b.makt=c.makt
                        left join medibv.cdha_loai d on a.id_loai = d.id
                        where a.mabn='{0}'", cdha.mabn);
                        sql = sql.Replace("xxx", "medibv" + mmyy);
                    }
                }
                sql += ") where 1=1";
                if (cdha.tungay != null && cdha.tungay != "" && cdha.denngay != null && cdha.denngay != "")
                {
                    sql += " and to_number(m_ngay) between to_number(to_char(to_date('" + cdha.tungay + "','dd/mm/yyyy'),'yyyymmdd')) and " + "to_number(to_char(to_date('" + cdha.denngay + "','dd/mm/yyyy'),'yyyymmdd'))";
                }
                sql += " order by m_ngay";
                ds = dbHelper.getDataSetbySql(sql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Chandoanhinhanh item = new Chandoanhinhanh();
                        item.ten = dr["ten"].ToString();
                        item.ngay = dr["m_ngay"].ToString();
                        item.tenkt = dr["tenkt"].ToString();
                        item.id = dr["id"].ToString();
                        item.mota = dr["mota"].ToString();
                        item.ketluan = dr["ketluan"].ToString();
                        lts.Add(item);
                    }
                    //dt = ds.Tables[0];
                    //return dt;                    
                }
                if (lts != null && lts.Count > 0)
                {
                    return lts;
                }
                else
                {
                    return null;
                }
            }

            //private static string Getthumuc()
            //{                
            //    return dbHelper.getFieldbyWhere("medibv.cdha_thongso","ten","id=21");
            //}

            //public static string f_Load_Image_Server()
            //{
            //    string apath_thumuc_maychu = "", ayymmdd = "", afilename = "", amabn = "", acount_cdha = "", amakt = "", achon = "0", adk = "";
            //    //ayymmdd = cdha.ngaycdha.Substring(8, 2) + cdha.ngaycdha.Substring(3, 2) + cdha.ngaycdha.Substring(0, 2);
            //    apath_thumuc_maychu = Getthumuc();
            //    //adk = cdha.mabn + "_" + cdha.count_cdha + "_" + cdha.makt + "*";
            //    adk = "16700636301";
            //    string imgstr = "";
            //    if (Directory.Exists(apath_thumuc_maychu))
            //    {
            //        string[] sf = System.IO.Directory.GetFiles(apath_thumuc_maychu, adk);
            //        for (int i = 0; i < sf.Length; i++)
            //        {
            //            afilename = sf[i].Substring(sf[i].LastIndexOf("\\") + 1);
            //            if (afilename != "" && (afilename.IndexOf(".jpg") != -1 || afilename.IndexOf(".bmp") != -1))
            //            {
            //                //amabn = afilename.Split('_')[0];
            //                //acount_cdha = afilename.Split('_')[1];
            //                //amakt = afilename.Split('_')[2];
            //                //achon = afilename.Split('_')[3];
            //                Bitmap bm = new Bitmap(apath_thumuc_maychu + "\\" + afilename);
            //                MemoryStream ms = new MemoryStream();
            //                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //                byte[] byteImage = ms.ToArray();
            //                imgstr += Convert.ToBase64String(byteImage) + "~";
            //            }
            //        }
            //        return imgstr;
            //    }
            //    else
            //    {
            //        return "";
            //    }
            //}

        }

        public static bool Delete(Data.DataPost.Delete del)//string table, string mmyy, string id, string maql
        {
            try
            {
                switch (del.table)
                {
                    case "chidinh":
                        {
                            if (del.id != "" && del.maql != "")
                            {
                                string sql = "Delete medibv" + del.mmyy + ".v_chidinh where id=" + del.id;
                                return dbHelper.ExecuteQuery(sql);
                            }
                            else
                            {
                                string sql = "Select * from medibv" + del.mmyy + ".v_chidinh where done=0 and paid=0 and maql=" + del.maql;
                                if (dbHelper.ExecuteQuery(sql))
                                {
                                    return false;
                                }
                                else
                                {
                                    sql = "Delete medibv" + del.mmyy + ".v_chidinh where maql=" + del.id;
                                    return dbHelper.ExecuteQuery(sql);
                                }
                            }
                        }
                    default:
                        return false;
                }

            }
            catch
            {
                return false;
            }
        }

        public class Insert
        {
            public string table { get; set; }
            public string value { get; set; }

            public static bool Insertdata(Data.Insert insert)
            {
                NpgsqlConnection con = new NpgsqlConnection();
                try
                {
                    if (insert.table == "benhanpk")
                    {
                        JObject value = ConvertJsonStringToJsonObject(insert.value);
                        Data.Benhanpk benhanpk = new Data.Benhanpk();
                        string conn = (string)value["conn"];
                        string m_mabn = (string)value["mabn"] == null ? "" : (string)value["mabn"];
                        decimal m_mavaovien = Convert.ToDecimal((string)value["mavaovien"] == null ? "" : (string)value["mavaovien"]);
                        decimal m_maql = Convert.ToDecimal((string)value["maql"] == null);
                        string m_makp = (string)value["makp"];
                        string m_ngay = (string)value["ngayvao"];
                        int m_madoituong = Convert.ToInt32((string)value["madoituong"] == null ? "" : (string)value["madoituong"]);
                        string m_mabs = (string)value["mabs"] == null ? "" : (string)value["mabs"];
                        string trieuchung = (string)value["trieuchung"];
                        string m_sovaovien = (string)value["sovaovien"] == null ? "" : (string)value["trieuchung"];
                        int m_userid = Convert.ToInt32((string)value["userid"]);
                        string ipcomputer = (string)value["ipcomputer"] == null ? "" : (string)value["ipcomputer"];
                        //string m_chandoan = "";
                        int i_maxlength_makp = 2;
                        int m_dentu = 2;
                        int m_nhantu = 2;
                        string m_maicd = "0";
                        int m_ttlucrv = 0;
                        int m_bienchung = 0;
                        int m_taibien = 0;
                        int m_giaiphau = 0;
                        int m_mangtr = 0;
                        if (conn != null || conn != "")
                        {
                            con = new NpgsqlConnection(conn);
                        }
                        //NpgsqlTransaction trans = null;
                        //trans = con.BeginTransaction();
                        con = new NpgsqlConnection(dbHelper.ConnectionStringDefault);
                        con.Open();
                        if (upd_trieuchung(m_ngay, m_maql, trieuchung, con))
                        {
                            upd_benhanpk(m_mabn, m_mavaovien, m_maql, m_makp, m_ngay, m_dentu, m_nhantu, m_madoituong, trieuchung, m_maicd, m_ttlucrv, m_mabs,
                            m_sovaovien, m_bienchung, m_taibien, m_giaiphau, m_mangtr, m_userid, con, i_maxlength_makp);
                        }
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    con.Close(); con.Dispose();
                }
            }

            private static void upd_benhanpk(string m_mabn, decimal m_mavaovien, decimal m_maql, string m_makp, string m_ngay, int m_dentu, int m_nhantu,
                int m_madoituong, string m_chandoan, string m_maicd, int m_ttlucrv, string m_mabs, string m_sovaovien, int m_bienchung, int m_taibien,
                int m_giaiphau, decimal m_mangtr, int m_userid, NpgsqlConnection con, int i_maxlength_makp)
            {
                try
                {
                    string schema = "medibv" + funcmmyy(m_ngay) + ".upd_benhanpk";
                    NpgsqlTransaction trans = null;
                    trans = con.BeginTransaction();
                    NpgsqlCommand cmd = new NpgsqlCommand(schema, con);
                    cmd.Transaction = trans;
                    cmd.CommandTimeout = 300000;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("m_mabn", NpgsqlDbType.Varchar).Value = m_mabn;
                    cmd.Parameters.Add("m_mavaovien", NpgsqlDbType.Numeric).Value = m_mavaovien;
                    cmd.Parameters.Add("m_maql", NpgsqlDbType.Numeric).Value = m_maql;
                    cmd.Parameters.Add("m_makp", NpgsqlDbType.Varchar, i_maxlength_makp).Value = m_makp;
                    cmd.Parameters.Add("m_ngay", NpgsqlDbType.Varchar, 16).Value = m_ngay;
                    cmd.Parameters.Add("m_dentu", NpgsqlDbType.Numeric).Value = m_dentu;
                    cmd.Parameters.Add("m_nhantu", NpgsqlDbType.Numeric).Value = m_nhantu;
                    cmd.Parameters.Add("m_madoituong", NpgsqlDbType.Numeric).Value = m_madoituong;
                    cmd.Parameters.Add("m_chandoan", NpgsqlDbType.Text).Value = m_chandoan;
                    cmd.Parameters.Add("m_maicd", NpgsqlDbType.Varchar, 9).Value = m_maicd;
                    cmd.Parameters.Add("m_ttlucrv", NpgsqlDbType.Numeric).Value = m_ttlucrv;
                    cmd.Parameters.Add("m_mabs", NpgsqlDbType.Varchar, 4).Value = m_mabs;
                    cmd.Parameters.Add("m_sovaovien", NpgsqlDbType.Varchar, 10).Value = m_sovaovien;
                    cmd.Parameters.Add("m_bienchung", NpgsqlDbType.Numeric).Value = m_bienchung;
                    cmd.Parameters.Add("m_taibien", NpgsqlDbType.Numeric).Value = m_taibien;
                    cmd.Parameters.Add("m_giaiphau", NpgsqlDbType.Numeric).Value = m_giaiphau;
                    cmd.Parameters.Add("m_mangtr", NpgsqlDbType.Numeric).Value = m_mangtr;
                    cmd.Parameters.Add("m_userid", NpgsqlDbType.Numeric).Value = m_userid;
                    cmd.ExecuteNonQuery();
                    trans.Commit();
                }
                catch
                {
                }
            }


            private static bool upd_trieuchung(string ngay, decimal m_maql, string m_ten, NpgsqlConnection con)
            {
                string sql = "update medibv" + funcmmyy(ngay) + ".trieuchung set ten=:m_ten where maql=:m_maql";
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("m_ten", NpgsqlDbType.Text).Value = m_ten;
                    cmd.Parameters.Add("m_maql", NpgsqlDbType.Numeric).Value = m_maql;
                    int irec = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    if (irec == 0)
                    {
                        sql = "insert into medibv" + funcmmyy(ngay) + ".trieuchung(maql,ten) values ";
                        sql += "(:m_maql,:m_ten)";
                        cmd = new NpgsqlCommand(sql, con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("m_maql", NpgsqlDbType.Numeric).Value = m_maql;
                        cmd.Parameters.Add("m_ten", NpgsqlDbType.Text).Value = m_ten;
                        irec = cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        return true;
                    }
                    return true;
                }
                catch (NpgsqlException ex)
                {
                    return false;
                }
            }
        }

        public static JObject ConvertJsonStringToJsonObject(string json)
        {
            try
            {
                JObject jsonObj = JObject.Parse(json);
                return jsonObj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public class DataPost
        {
            public class Chidinh
            {
                public string maql { get; set; }
                public string ngay { get; set; }

                public Chidinh()
                {
                }

                public Chidinh(string _maql, string _ngay)
                {
                    if (_ngay.Length > 4)
                    {
                        _ngay = _ngay.Split('/')[1].ToString() + _ngay.Split('/')[2].ToString();
                    }
                    this.maql = _maql;
                    this.ngay = _ngay;
                }
            }

            public class Bacsi
            {
                public string mabs { get; set; }
                public string makp { get; set; }
                public string hoten { get; set; }

                public Bacsi()
                {
                }

                public Bacsi(string _mabs, string _makp, string _hoten)
                {
                    this.mabs = _mabs;
                    this.makp = _makp;
                    this.hoten = _hoten;
                }
            }

            public class Khoaphong
            {
                public string makp { get; set; }
                public string ten { get; set; }
                public string search { get; set; }

                public Khoaphong()
                {
                }

                public Khoaphong(string _makp, string _ten, string _search)
                {
                    this.makp = _makp;
                    this.makp = _makp;
                    this.search = _search;
                }
            }

            public class Delete
            {
                public string mmyy { get; set; }
                public string table { get; set; }
                public string id { get; set; }
                public string maql { get; set; }

                public Delete()
                {
                }

                public Delete(string _mmyy, string _table, string _id, string _maql)
                {
                    this.mmyy = _mmyy;
                    this.table = _table;
                    this.id = _id;
                    this.maql = _maql;
                }
            }

            public class Chandoanhinhanh
            {
                public string mabn { get; set; }
                public string tungay { get; set; }
                public string denngay { get; set; }
                public string check { get; set; }
                public decimal count_cdha { get; set; }
                public string makt { get; set; }
                public string ngaycdha { get; set; }

                public Chandoanhinhanh()
                {
                }

                public Chandoanhinhanh(string _mabn, string _tungay, string _denngay, string _check, decimal _count_cdha, string _makt, string _ngaycdha)
                {
                    this.mabn = _mabn;
                    this.tungay = _tungay;
                    this.denngay = _denngay;
                    this.check = check;
                    this.count_cdha = _count_cdha;
                    this.makt = _makt;
                    this.ngaycdha = _ngaycdha;
                }
            }
        }

        public static string funcmmyy(string ngay)
        {
            if (ngay.Length == 4) return ngay;
            else return ngay.Substring(3, 2) + ngay.Substring(8, 2);
        }

        public class Search
        {
            public string table { get; set; }
            public string value { get; set; }
        }
    }
}
