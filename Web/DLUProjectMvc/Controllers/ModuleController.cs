using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DLUProject.Services;
using DLUProject.Domain;
using ColorLife.Core.Helper;
namespace DLUProjectMvc.Controllers
{
    public class ModuleController : Controller
    {
        IServices<GallerySlider> _gallerySlider;
        public ModuleController(IServices<GallerySlider>gallerySlider)
        {
            this._gallerySlider = gallerySlider;
        }
        
        [ChildActionOnly]
        public PartialViewResult _ModuleHomePageSlider()
        {
            var model = _gallerySlider.All().Where(c => c.IsDisplayFlag(DisplayFlagSlider.SlideShow));
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult _ModuleCategory()
        {
            var  model = _gallerySlider.All() ;
            return PartialView(model);
        }
    }
}