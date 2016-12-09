using wpf_tmca.Model;

namespace wpf_tmca.ViewModel.Items
{
    public class ClassViewModel : ItemViewModel
    {
        

        public ClassViewModel():this(new Item() { Type = EItem.Class }) { }
        public ClassViewModel(Item item) : base(item)
        {
            Height = 100;
            Width = 100;
        }
    }



}
