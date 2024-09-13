﻿using NurseryApp.Core.Repositories;

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
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }

}
