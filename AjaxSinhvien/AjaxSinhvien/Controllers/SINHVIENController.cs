using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AjaxSinhvien.Models;

namespace AjaxSinhvien.Controllers
{
    public class SV
    {
        public string ID { get; set; }
        public string ten { get; set; }
        public string lop { get; set; }
        public string ngaysinh { get; set; }
        public string que { get; set; }
        public string dantoc { get; set; }
    }
    public class SINHVIENController : Controller
    {
        private SVDbContext db = new SVDbContext();

        // GET: SINHVIEN
        public ActionResult Index()
        {
            var sINHVIENs = db.SINHVIENs.Include(s => s.DANTOC).Include(s => s.LOP);
            return View(sINHVIENs.ToList());
        }

        [HttpGet]
        public JsonResult ListAll()
        {
            List<SINHVIEN> sv = db.SINHVIENs.Include(s => s.DANTOC).Include(s => s.LOP).ToList();
            List<SV> sinhvien = new List<SV>();
            foreach(var s in sv)
            {
                SV student = new SV();
                student.ID = s.ID.ToString();
                student.ten = s.TENSV;
                student.lop = s.LOP.TENLOP;
                student.ngaysinh = s.NGAYSINH.ToString().Split(' ')[0];
                student.que = s.QUEQUAN;
                student.dantoc = s.DANTOC.TENDT;
                sinhvien.Add(student);
            }
            return Json(sinhvien, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Search(string searchString)
        {
            //List<SINHVIEN> sv = db.SINHVIENs.Include(s => s.DANTOC).Include(s => s.LOP).ToList();
            var sv = from a in db.SINHVIENs.Include(s => s.DANTOC).Include(s => s.LOP) select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                sv = db.SINHVIENs.Include(s => s.DANTOC).Include(s => s.LOP).Where(a => a.TENSV.ToUpper().Contains(searchString.ToUpper()) || a.LOP.TENLOP.ToUpper().Contains(searchString.ToUpper()));
            }
            List<SV> sinhvien = new List<SV>();
            foreach (var s in sv.ToList())
            {
                SV student = new SV();
                student.ID = s.ID.ToString();
                student.ten = s.TENSV;
                student.lop = s.LOP.TENLOP;
                student.ngaysinh = s.NGAYSINH.ToString().Split(' ')[0];
                student.que = s.QUEQUAN;
                student.dantoc = s.DANTOC.TENDT;
                sinhvien.Add(student);
            }
            return Json(sinhvien, JsonRequestBehavior.AllowGet);
        }

        // GET: SINHVIEN/Details/5
        [HttpPost]
        public JsonResult Details(int? id)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            List<string> sv = new List<string>();
            sv.Add(sINHVIEN.TENSV);
            sv.Add(sINHVIEN.LOP.ID.ToString());
            sv.Add(sINHVIEN.NGAYSINH.ToString().Split(' ')[0]);
            sv.Add(sINHVIEN.QUEQUAN);
            sv.Add(sINHVIEN.DANTOC.ID.ToString());
            return Json(sv);
        }

        // POST: SINHVIEN/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult Create(string ten, int lop, string ngaysinh, string que, int dantoc)
        {
            SINHVIEN sINHVIEN = new SINHVIEN();
            sINHVIEN.TENSV = ten;
            sINHVIEN.MALOP = lop;
            sINHVIEN.NGAYSINH = Convert.ToDateTime(ngaysinh);
            sINHVIEN.QUEQUAN = que;
            sINHVIEN.MADANTOC = dantoc;
            db.SINHVIENs.Add(sINHVIEN);
            db.SaveChanges();
            SV sv = new SV();
            var student = db.SINHVIENs.Include(s => s.DANTOC).Include(s => s.LOP).Where(s => s.ID == sINHVIEN.ID).ToList()[0];
            sv.ID = student.ID.ToString();
            sv.ten = student.TENSV;
            sv.lop = student.LOP.TENLOP;
            sv.ngaysinh = student.NGAYSINH.ToString().Split(' ')[0];
            sv.que = student.QUEQUAN;
            sv.dantoc = student.DANTOC.TENDT;
            return Json(sv);
            //ViewBag.MADANTOC = new SelectList(db.DANTOCs, "ID", "TENDT", sINHVIEN.MADANTOC);
            //ViewBag.MALOP = new SelectList(db.LOPs, "ID", "TENLOP", sINHVIEN.MALOP);
        }

        [HttpPost]
        // GET: SINHVIEN/Edit/5
        public JsonResult Edit(int id, string ten, int lop, string ngaysinh, string que, int dantoc)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            //ViewBag.MADANTOC = new SelectList(db.DANTOCs, "ID", "TENDT", sINHVIEN.MADANTOC);
            //ViewBag.MALOP = new SelectList(db.LOPs, "ID", "TENLOP", sINHVIEN.MALOP);
            sINHVIEN.TENSV = ten;
            sINHVIEN.MALOP = lop;
            sINHVIEN.NGAYSINH = Convert.ToDateTime(ngaysinh);
            sINHVIEN.QUEQUAN = que;
            sINHVIEN.MADANTOC = dantoc;
            if (ModelState.IsValid)
            {
                db.Entry(sINHVIEN).State = EntityState.Modified;
                db.SaveChanges();
            }
            SV sv = new SV();
            var student = db.SINHVIENs.Include(s => s.DANTOC).Include(s => s.LOP).Where(s => s.ID == sINHVIEN.ID).ToList()[0];
            sv.ID = student.ID.ToString();
            sv.ten = student.TENSV;
            sv.lop = student.LOP.TENLOP;
            sv.ngaysinh = student.NGAYSINH.ToString().Split(' ')[0];
            sv.que = student.QUEQUAN;
            sv.dantoc = student.DANTOC.TENDT;
            return Json(sv);
        }

        // POST: SINHVIEN/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,TENSV,NGAYSINH,QUEQUAN,MADANTOC,MALOP")] SINHVIEN sINHVIEN)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(sINHVIEN).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MADANTOC = new SelectList(db.DANTOCs, "ID", "TENDT", sINHVIEN.MADANTOC);
        //    ViewBag.MALOP = new SelectList(db.LOPs, "ID", "TENLOP", sINHVIEN.MALOP);
        //    return View(sINHVIEN);
        //}

        // POST: SINHVIEN/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public JsonResult Delete(int id)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            try
            {
                db.SINHVIENs.Remove(sINHVIEN);
                db.SaveChanges();
                var sINHVIENs = db.SINHVIENs.Include(s => s.DANTOC).Include(s => s.LOP);
                return Json(id);
            }
            catch
            {
                return Json(new { message = "Không xóa được!" });
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
