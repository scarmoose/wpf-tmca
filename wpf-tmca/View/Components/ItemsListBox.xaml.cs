using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_tmca.View.Components
{
    /// <summary>
    /// Interaction logic for ItemsListBox.xaml
    /// </summary>
    public partial class ItemsListBox : ListBox
    {
        public ItemsListBox()
        {
            InitializeComponent();
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new ItemContainer();
        }
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return (item is ItemContainer);
        }

        private Queue<ItemContainer> _chain = new Queue<ItemContainer>();

        public void DrawConnection(ItemContainer shapeItemContainer)
        {
            if (_chain.Count < 1)
            {
                _chain.Enqueue(shapeItemContainer);
            }
            else
            {
                _chain.Enqueue(shapeItemContainer);
                //invoke command

                // then
                _chain.Clear();
            }
        }
    } 
}
