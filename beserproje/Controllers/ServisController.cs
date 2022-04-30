using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using beserproje.Models;
using beserproje.ViewModel;
namespace beserproje.Controllers
{
    public class ServisController : ApiController
    {
        DB01Entities db = new DB01Entities();
        SonucModel sonuc = new SonucModel();

        #region Kategoriler

        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategorilerModel> KategorilerListe()
        {
            List<KategorilerModel> liste = db.Kategoriler.Select(x => new KategorilerModel()
            {
                KategoriId = x.KategoriId,
                KategoriAd = x.KategoriAd,
                KatHaberSay = x.Haberler.Count()
            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/kategoribyid/{KategoriId}")]
        public KategorilerModel KategoriById(int KategoriId)
        {
            KategorilerModel kayit = db.Kategoriler.Where(s => s.KategoriId == KategoriId).Select(x => new KategorilerModel()
            {
                KategoriId = x.KategoriId,
                KategoriAd = x.KategoriAd,
                KatHaberSay = x.Haberler.Count()

            }).FirstOrDefault();

            return kayit;
        }


        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategorilerModel model)
        {
            if (db.Kategoriler.Count(s => s.KategoriAd == model.KategoriAd) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Adı Kayıtlıdır!";
                return sonuc;
            }
            Kategoriler yeni = new Kategoriler();
            yeni.KategoriAd = model.KategoriAd;
            db.Kategoriler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;
        }


        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategorilerModel model)
        {
            Kategoriler kayit = db.Kategoriler.Where(s => s.KategoriId == model.KategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.KategoriAd = model.KategoriAd;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";
            return sonuc;
        }




        [HttpDelete]
        [Route("api/kategorisil/{KategoriId}")]
        public SonucModel KategoriSil(int KategoriId)
        {
            Kategoriler kayit = db.Kategoriler.Where(s => s.KategoriId == KategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            if (db.Haberler.Count(s => s.HaberKatId == KategoriId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Haber Kaydı Olan Kategori Silinemez!";
                return sonuc;
            }
            db.Kategoriler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";
            return sonuc;
        }
        #endregion

        #region Haberler

        [HttpGet]
        [Route("api/haberlistele")]
        public List<HaberlerModel> HaberListe()
        {
            List<HaberlerModel> liste = db.Haberler.Select(x => new HaberlerModel()
            {
                HaberId = x.HaberId,
                HaberBaslik = x.HaberBaslik,
                HaberIcerik = x.HaberIcerik,
                HaberTarih = x.HaberTarih,
                HaberFoto = x.HaberFoto,
                HaberKatId = x.HaberKatId,
                HaberUyeId = x.HaberUyeId
            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/haberlistelebykatid/{KategoriId}")]
        public List<HaberlerModel> HaberListeByKatId(int KategoriId)
        {
            List<HaberlerModel> liste = db.Haberler.Where(s => s.HaberKatId == KategoriId).Select(x =>
           new HaberlerModel()
           {
               HaberId = x.HaberId,
               HaberBaslik = x.HaberBaslik,
               HaberIcerik = x.HaberIcerik,
               HaberTarih = x.HaberTarih,
               HaberFoto = x.HaberFoto,
               HaberKatId = x.HaberKatId,
               HaberUyeId = x.HaberUyeId

           }).ToList();
            return liste;
        }


        [HttpPost]
        [Route("api/haberekle")]
        public SonucModel HaberEkle(HaberlerModel model)
        {
            if (db.Haberler.Count(s => s.HaberBaslik == model.HaberBaslik && s.HaberKatId == model.HaberKatId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Haber İlgili Kategoride Kayıtlıdır!";
                return sonuc;
            }
            Haberler yeni = new Haberler();
            yeni.HaberBaslik = model.HaberBaslik;
            yeni.HaberIcerik = model.HaberIcerik;
            yeni.HaberTarih = model.HaberTarih;
            yeni.HaberFoto = model.HaberFoto;
            yeni.HaberUyeId = model.HaberUyeId;
            db.Haberler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Haber Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/haberduzenle")]
        public SonucModel HaberDuzenle(HaberlerModel model)
        {
            Haberler kayit = db.Haberler.Where(s => s.HaberId == model.HaberId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.HaberBaslik = model.HaberBaslik;
            kayit.HaberIcerik = model.HaberIcerik;
            kayit.HaberTarih = model.HaberTarih;
            kayit.HaberFoto = model.HaberFoto;
            kayit.HaberUyeId = model.HaberUyeId;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Haber Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/habersil/{HaberId}")]
        public SonucModel HaberSil(int HaberId)
        {
            Haberler kayit = db.Haberler.Where(s => s.HaberId == HaberId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Haber Bulunamadı!";
                return sonuc;
            }

            db.Haberler.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Haber Silindi";
            return sonuc;
        }
        #endregion


        #region Uyeler

        [HttpGet]
        [Route("api/uyelistele")]

        public List<UyelerModel> UyeListe()
        {
            List<UyelerModel> liste = db.Uyeler.Select(x => new UyelerModel()
            {
                UyeID = x.UyeID,
                UyeAdSoyad = x.UyeAdSoyad,
                UyeMail = x.UyeMail,
                UyeYas = x.UyeYas,
                UyeParola = x.UyeParola,
                UyeTarih = x.UyeTarih,
                UyeYetki = x.UyeYetki,


            }).ToList();
            return liste;
        }


        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyelerModel UyeById(int UyeID)
        {
            UyelerModel kayit = db.Uyeler.Where(s => s.UyeID == UyeID).Select(x => new UyelerModel()
            {
                UyeID = x.UyeID,
                UyeAdSoyad = x.UyeAdSoyad,
                UyeMail = x.UyeMail,
                UyeYas = x.UyeYas,
                UyeParola = x.UyeParola,
                UyeTarih = x.UyeTarih,
                UyeYetki = x.UyeYetki
            }).SingleOrDefault();
            return kayit;
        }



        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyelerModel model)
        {
            if (db.Uyeler.Count(s => s.UyeMail == model.UyeMail) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "E-Posta Adresi Zaten Kayıtlıdır!";
                return sonuc;
            }
            Uyeler yeni = new Uyeler();
            yeni.UyeAdSoyad = model.UyeAdSoyad;
            yeni.UyeMail = model.UyeMail;
            yeni.UyeYas = model.UyeYas;
            yeni.UyeParola = model.UyeParola;
            yeni.UyeTarih = model.UyeTarih;
            yeni.UyeYetki = model.UyeYetki;
            db.Uyeler.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/uyesil/{UyeID}")]
        public SonucModel UyeSil(int UyeID)
        {
            Uyeler kayit = db.Uyeler.Where(s => s.UyeID == UyeID).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı!";
                return sonuc;
            }

            db.Uyeler.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }


        #endregion
    }
}
