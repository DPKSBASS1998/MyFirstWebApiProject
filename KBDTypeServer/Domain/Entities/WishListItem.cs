using KBDTypeServer.Domain.Entities.ProductEntity;
using KBDTypeServer.Domain.Entities.UserEntity;

namespace KBDTypeServer.Domain.Entities
{
    public class WishListItem
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }

}
