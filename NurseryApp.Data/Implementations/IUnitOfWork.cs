using NurseryApp.Core.Repositories;

namespace NurseryApp.Data.Implementations
{
    public interface IUnitOfWork : IDisposable
    {
        public IAttenDanceRepository attenDanceRepository { get; }
        public IFeeRepository feeRepository { get; }
        public IGroupRepository groupRepository { get; }
        public IHomeWorkRepository homeWorkRepository { get; }
        public IHomeWorkSubmissionRepository homeWorkSubmissionRepository { get; }
        public INotificationRepository notificationRepository { get; }
        public IParentRepository parentRepository { get; }
        public IStudentRepository studentRepository { get; }
        public ITeacherRepository teacherRepository { get; }
        public IBlogRepository blogRepository { get; }
        public IBannerRepository bannerRepository { get; }
        public ISettingRepository settingRepository { get; }
        public IContatctRepository contatctRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }

}

