using NurseryApp.Core.Repositories;
using NurseryApp.Data.Data;

namespace NurseryApp.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly NurseryAppContext _context;

        public IAttenDanceRepository attenDanceRepository { get; private set; }
        public IFeeRepository feeRepository { get; private set; }
        public IGroupRepository groupRepository { get; private set; }
        public IHomeWorkRepository homeWorkRepository { get; private set; }
        public IHomeWorkSubmissionRepository homeWorkSubmissionRepository { get; private set; }
        public INotificationRepository notificationRepository { get; private set; }
        public IParentRepository parentRepository { get; private set; }
        public IStudentRepository studentRepository { get; private set; }
        public ITeacherRepository teacherRepository { get; private set; }
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
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log or inspect the inner exception
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }
    }
}
