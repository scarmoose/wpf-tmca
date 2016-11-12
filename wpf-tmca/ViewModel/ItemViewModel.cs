using System.Windows;
using wpf_tmca.Model;

namespace wpf_tmca.ViewModel
{
    class ItemViewModel : BaseViewModel, IItem
    {
        private bool _isSelected;
        private Point _initialMousePostion;
        private bool _isMoving;
        private bool _isConnectingShapes;
        private Point _initialShapePostion;


    }
}
