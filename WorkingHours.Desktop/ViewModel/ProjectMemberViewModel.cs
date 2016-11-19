using WorkingHours.Shared.Model;

namespace WorkingHours.Desktop.ViewModel
{
    public class ProjectMemberViewModel:UserViewModel
    {
        public Roles RoleInProject
        {
            get { return member.RoleInProjectEnum; }
            set
            {
                if (member.RoleInProjectEnum == value) { return; }
                IsChanged = !IsChanged;
                member.RoleInProjectEnum = value;
                RaisePropertyChanged();
            }
        }
        
        public bool IsReadonly
        {
            get { return Role == Roles.Employee; }
        }

        public override Roles Role
        {
            get { return base.Role; }

            set
            {
                base.Role = value;
                RaisePropertyChanged(nameof(IsReadonly));
            }
        }

        Shared.Dto.ProjectMemberDto member;
        public ProjectMemberViewModel(Shared.Dto.ProjectMemberDto projectMember) : base(projectMember)
        {
            member = projectMember;
            RoleInProject = projectMember.RoleInProjectEnum;
        }
    }
}