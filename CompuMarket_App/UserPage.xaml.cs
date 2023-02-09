using CompuMarket_App.DB;
using System.Linq;
using System.Windows.Controls;

namespace CompuMarket_App
{
   
    public partial class UserPage : Page
    {
        private Users _currentUser;
        public UserPage(Users authUser)
        {
            InitializeComponent();
            _currentUser = authUser;
            DataContext = _currentUser;

            var _order = IlyasovaFriendEntitieser.GetContext().PaidFor.ToList();
            _order = _order.Where(p => p.Users.UserId == _currentUser.UserId).ToList();
            LViewProduct.ItemsSource = _order;
        }
    }
}
