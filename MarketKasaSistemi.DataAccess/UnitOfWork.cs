using MarketKasaSistemi.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketKasaSistemi.DataAccess
{
    public class UnitOfWork : IDisposable
    {
        private DBContext context;

        private FisRepository _fisRepository;
        private KategoriRepository _kategoriRepository;
        private KullaniciRepository _kullaniciRepository;
        private OdemeTipRepository _odemeTipRepository;
        private PersonelRepository _personelRepository;
        private PersonelTipRepository _personelTipRepository;
        private SatisRepository _satisRepository;
        private UrunRepository _urunRepository;
        private VergiRepository _vergiRepository;

        public FisRepository FisRepository
        {
            get
            {
                if (_fisRepository == null)
                    _fisRepository = new FisRepository(context);
                return _fisRepository;
            }
        }
        public KategoriRepository KategoriRepository
        {
            get
            {
                if (_kategoriRepository == null)
                    _kategoriRepository = new KategoriRepository(context);
                return _kategoriRepository;
            }
        }
        public KullaniciRepository KullaniciRepository
        {
            get
            {
                if (_kullaniciRepository == null)
                    _kullaniciRepository = new KullaniciRepository(context);
                return _kullaniciRepository;
            }
        }
        public OdemeTipRepository OdemeTipRepository
        {
            get
            {
                if (_odemeTipRepository == null)
                    _odemeTipRepository = new OdemeTipRepository(context);
                return _odemeTipRepository;
            }
        }
        public PersonelRepository PersonelRepository
        {
            get
            {
                if (_personelRepository == null)
                    _personelRepository = new PersonelRepository(context);
                return _personelRepository;
            }
        }
        public PersonelTipRepository PersonelTipRepository
        {
            get
            {
                if (_personelTipRepository == null)
                    _personelTipRepository = new PersonelTipRepository(context);
                return _personelTipRepository;
            }
        }
        public SatisRepository SatisRepository
        {
            get
            {
                if (_satisRepository == null)
                    _satisRepository = new SatisRepository(context);
                return _satisRepository;
            }
        }
        public UrunRepository UrunRepository
        {
            get
            {
                if (_urunRepository == null)
                    _urunRepository = new UrunRepository(context);
                return _urunRepository;
            }
        }
        public VergiRepository VergiRepository
        {
            get
            {
                if (_vergiRepository == null)
                    _vergiRepository = new VergiRepository(context);
                return _vergiRepository;
            }
        }

        public UnitOfWork()
        {
            context = new DBContext();
        }

        public void Dispose()
        {
            context?.Dispose();
            _fisRepository?.Dispose();
            _kategoriRepository?.Dispose();
            _kullaniciRepository?.Dispose();
            _odemeTipRepository?.Dispose();
            _personelRepository?.Dispose();
            _personelTipRepository?.Dispose();
            _satisRepository?.Dispose();
            _urunRepository?.Dispose();
            _vergiRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
