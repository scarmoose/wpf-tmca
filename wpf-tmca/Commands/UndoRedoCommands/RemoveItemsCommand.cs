﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf_tmca.ViewModel;
using wpf_tmca.ViewModel.Associations;

namespace wpf_tmca.Commands.UndoRedoCommands
{
    class RemoveItemsCommand : IUndoRedoCommand
    {
        private ObservableCollection<ItemViewModel> items;
        private ObservableCollection<AssociationViewModel> associations;
        private List<ItemViewModel> itemsToRemove;
        private List<AssociationViewModel> associationsToRemove;

        public RemoveItemsCommand(ObservableCollection<ItemViewModel> _items, 
            ObservableCollection<AssociationViewModel> _associations, List<ItemViewModel> _itemsToRemove)
        {
            items = _items;
            associations = _associations;
            itemsToRemove = _itemsToRemove;
            associationsToRemove = _associations.Where(x => _itemsToRemove.Any(y => y.ItemNumber == x.From.ItemNumber ||
                y.ItemNumber == x.To.ItemNumber)).ToList();
        }
        public void Execute()
        {
            itemsToRemove.ForEach(x => items.Remove(x));
            associationsToRemove.ForEach(x => associations.Remove(x));
        }

        public void Unexecute()
        {
            itemsToRemove.ForEach(x => items.Add(x));
            associationsToRemove.ForEach(x => associations.Add(x));
        }
    }
}
