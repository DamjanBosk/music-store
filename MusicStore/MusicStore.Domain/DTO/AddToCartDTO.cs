using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Domain.DTO
{
    public class AddToCartDTO
    {
        public Guid SelectedAlbumId { get; set; }
        public string? SelectedAlbumName { get; set; }
        public int Quantity { get; set; }
    }

}
