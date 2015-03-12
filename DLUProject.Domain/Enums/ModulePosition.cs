using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DLUProject.Domain
{
    public enum ModulePosition
    {
        Logo = 1,               // Vị trí Logo
        Header = 2,             // Vị trí Head (Quảng cáo nếu có)
        Left = 4,               // Vị trí Left
        Right = 8,              // Vị trí Right
        TopContent = 16,         // Vị trí trên bài viết
        BottomContent = 32,      // Vị trí dưới bài viết
        User1 = 64,              // Vị trí User1 (Phụ thuộc vào giao diện trang web)
        User2 = 128,              // User2 nt
        User3 = 256,              // User3 nt
        User4 = 512,              // User4 nt
        Footer = 1024,            // Vị trí Footer
        Breadcrumb = 2048,        // Vị trí thanh điều hướng (VD: BreakNews, SiteMap..)
        SlideShow = 4096          // SlideShow bài viết
    }
}