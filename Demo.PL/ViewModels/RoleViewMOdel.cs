using System;

namespace Demo.PL.ViewModels
{
    public class RoleViewMOdel
    {
        public string Id { get; set; }

        public string  RoleName { get; set; }

        public RoleViewMOdel()
        {
            Id = Guid.NewGuid().ToString(); 
        }
    }
}
