using System;

namespace DLUProject.Domain
{
    public enum GalleryTypeEnum
    {
        Image = 1,
        Folder = 2,
        Flickr = 3
    }
    public enum DisplayFlagMenu
    {
        Top = 1,
        Left = 2,
        Right = 4,
        Bottom = 8,
        SubMenu = 16,
        CPanel = 32
    }
    public enum DisplayFlagSlider
    {
        Top = 1,
        Left = 2,
        Right = 4,
        Bottom = 8,
        SlideShow = 16,
        All = 32
    }
    public enum DisplayFlagMenuEnum
    {
        MainMenu = 1,
        Top = 2,
        Bottom = 4,
        Left = 8,
        Right = 16,
        Footer1 = 32,
        Footer2 = 64,
        Footer3 = 128,
        Footer4 = 256
    }
    public enum DisplayFlagContent
    {
        FontPage = 1,
        Featured = 2,
        Breaking = 4,
        HotNews = 8,
        SpecialNews = 16,
        AdvertiseNews = 32
    }
    public enum ContentStateEnum
    {
        Author = 0,
        Editor = 1,
        Publisher = 2,
        Approved = 3
    }
    public enum DisplayFlagModule
    {
        HOME_PAGE = 1,
        CONTACT_PAGE = 2,
        DETAIL_PAGE = 4,
        Bottom = 8,
        SubMenu = 16,
        CPanel = 32
    }
  
    public enum DisplayFlagBannerPosition
    {
        Top_Banner = 1,     // Vi tri duoi menu
        User1_Banner = 2,
        User2_Banner = 4,
        User3_Banner = 8,
        User4_Banner = 16,
        User5_Banner = 32,
        User6_Banner = 64,
        User7_Banner = 128,
        User8_Banner = 256,
        Bottom_Banner = 384, // Bottom
        Header_Banner = 512, // Header_Logo
        Left_Banner = 1024,   // Left
        Right_Banner = 2048,  // Right
        Above_Content = 4096, // Tren cung trang noi dung
        Below_Content = 8192, // Duoi cung trang noi dug
        Footer_Banner = 16384  // Footer
    }
    public enum BannerTypeEnum
    {
             
        Image = 1,
        TextLink = 2,
        Flash = 4
    }
}