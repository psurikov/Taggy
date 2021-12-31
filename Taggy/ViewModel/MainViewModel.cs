using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taggy.Model;

namespace Taggy.ViewModel
{
    public class MainViewModel
    {
        #region Fields

        private ObservableCollection<FileReference> fileReferences = new ObservableCollection<FileReference>();
        private string selectedTag = "";
        private ObservableCollection<FileReference> selected = new ObservableCollection<FileReference>();

        #endregion

        #region Constructors

        public MainViewModel()
        {

        }

        #endregion

        #region Properties

        #endregion
    }
}
