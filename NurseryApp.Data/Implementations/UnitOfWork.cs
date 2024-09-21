using Microsoft.EntityFrameworkCore.Storage;
using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NurseryAppContext _context;
        private IDbContextTransaction _transaction;

        public IAttenDanceRepository attenDanceRepository { get; private set; }
        public IFeeRepository feeRepository { get; private set; }
        public IGroupRepository groupRepository { get; private set; }
        public IHomeWorkRepository homeWorkRepository { get; private set; }
        public IHomeWorkSubmissionRepository homeWorkSubmissionRepository { get; private set; }
        public INotificationRepository notificationRepository { get; private set; }
        public IParentRepository parentRepository { get; private set; }
        public IStudentRepository studentRepository { get; private set; }
        public ITeacherRepository teacherRepository { get; private set; }
        public IBlogRepository blogRepository { get; private set; }
        public IBannerRepository bannerRepository { get; private set; }
        public ISettingRepository settingRepository { get; private set; }
        public UnitOfWork(NurseryAppContext context)
        {
            _context = context;
            attenDanceRepository = new AttenDanceRepository(context);
            feeRepository = new FeeRepository(context);
            groupRepository = new GroupRepository(context);
            homeWorkRepository = new HomeWorkRepository(context);
            homeWorkSubmissionRepository = new HomeWorkSubmissionRepository(context);
            notificationRepository = new NotificationRepository(context);
            parentRepository = new ParentRepository(context);
            studentRepository = new StudentRepository(context);
            teacherRepository = new TeacherRepository(context);
            blogRepository = new BlogRepository(context);
            bannerRepository = new BannerRepository(context);
            settingRepository = new SettingRepository(context);
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
                throw new InvalidOperationException("There is already an open transaction.");

            _transaction = await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
