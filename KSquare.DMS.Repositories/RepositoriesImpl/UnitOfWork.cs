using KSquare.DMS.Repositories.Context;
using KSquare.DMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;

namespace KSquare.DMS.Repositories.RepositoriesImpl
{
    public class UnitOfWork : IUnitOfWork
    {
        private IUserRepository _userRepository;
        private ITokenRepository _tokenRepository;
        private IAdminRepository _adminRepository;


        private readonly KSquareContext _context;

        public UnitOfWork(KSquareContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_context);
                }

                return _userRepository;
            }
        }

        public ITokenRepository TokenRepository
        {
            get
            {
                if (_tokenRepository == null)
                {
                    _tokenRepository = new TokenRepository(_context);
                }

                return _tokenRepository;
            }
        }

        public IAdminRepository AdminRepository
        {
            get
            {
                if (_adminRepository == null)
                {
                    _adminRepository = new AdminRepository(_context);
                }

                return _adminRepository;
            }
        }

        public void DiscardChanges()
        {
            foreach (var Entry in _context.ChangeTracker.Entries())
            {
                Entry.State = EntityState.Unchanged;
            }
        }

        public async Task<bool> SaveChangesAsync(bool overwriteDbChangesInCaseOfConcurrentUpdates = true)
        {
            bool saveFailed = false;
            do
            {
                saveFailed = false;

                try
                {
                    int count = await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    if (overwriteDbChangesInCaseOfConcurrentUpdates)
                    {
                        foreach (var Entry in ex.Entries)
                        {
                            foreach (var property in Entry.Entity.GetType().GetTypeInfo().DeclaredProperties)
                            {
                                //var originalValue = Entry.Property(property.Name).OriginalValue;
                                var currentValue = Entry.Property(property.Name).CurrentValue;
                                Entry.Property(property.Name).OriginalValue = currentValue;
                            }
                        }
                    }
                    else
                    {
                        foreach (var Entry in ex.Entries)
                        {
                            foreach (var property in Entry.Entity.GetType().GetTypeInfo().DeclaredProperties)
                            {
                                var originalValue = Entry.Property(property.Name).OriginalValue;
                                //var currentValue = Entry.Property(property.Name).CurrentValue;
                                Entry.Property(property.Name).CurrentValue = originalValue;
                            }
                        }
                    }
                }
            } while (saveFailed);
            return await Task.FromResult(!saveFailed);
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
