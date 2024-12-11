using Microsoft.AspNetCore.Identity;

namespace BerberYonetimSistemi.Models
{
    // IdentityRole'u int Id ile kullanmak için bu sınıfı özelleştirin
    public class Role : IdentityRole<int>
    {
        // Ek özellikler eklemek isterseniz burada yapabilirsiniz
    }
}
