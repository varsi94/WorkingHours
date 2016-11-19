namespace WorkingHours.Desktop.ViewModel
{
    public class ProjectMemberViewModel:UserViewModel
    {
        public Shared.Model.Roles RoleInProject
        {
            get { return member.RoleInProjectEnum; }
            set
            {
                member.RoleInProjectEnum = value;
                RaisePropertyChanged();
            }
        }


        public bool CanUpDate
        {
            get { return Role == Shared.Model.Roles.Manager; }
        }

        Shared.Dto.ProjectMemberDto member;
        public ProjectMemberViewModel(Shared.Dto.ProjectMemberDto projectMember) : base(projectMember)
        {
            member = projectMember;
            RoleInProject = projectMember.RoleInProjectEnum;
        }
    }
}