using System.ComponentModel.DataAnnotations;

namespace IronBookStoreAuthJWT.Core.Dtos
{
    public class BookAbstractBase
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "required|Name is required.")]
        [MaxLength(100, ErrorMessage = "maxLength|Name is too long")]
        public string Name { get; set; }

        [MaxLength(512, ErrorMessage = "maxLength|Description is too long")]
        public string Description { get; set; }
    }
}
