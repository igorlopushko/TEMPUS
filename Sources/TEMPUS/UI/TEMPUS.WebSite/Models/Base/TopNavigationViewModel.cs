using System;

namespace TEMPUS.WebSite.Models.Base
{
    [Serializable]
    public class TopNavigationViewModel
    {
        public string CurrentProjectName { get; private set; }
        public bool HasMoreThanOneProject { get; private set; }

        public TopNavigationViewModel(string currentProjectName,
            bool hasMoreThanOneProject)
        {
            CurrentProjectName = currentProjectName;
            HasMoreThanOneProject = hasMoreThanOneProject;
        }
    }
}