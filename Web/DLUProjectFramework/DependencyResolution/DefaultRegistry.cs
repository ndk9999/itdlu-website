// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace DLUProjectFramework.DependencyResolution
{
    using DLUProject.Data;
    using DLUProject.Domain;
    using DLUProject.Services;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;

    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                    scan.Assembly("DLUProject.Domain");
                    scan.Assembly("DLUProject.Data");
                    scan.Assembly("DLUProject.Services");
                    scan.With(new ControllerConvention());
                });
            // For<IExample>().Use<Example>();
            For<BLDataContext>().Use(() => new BLDataContext("ConnectionString"));
            For<DLUDataContext>().Use(() => new DLUDataContext("ConnectionString2")); 
  
             
            // Account
            For<IRepository<Account>>().Use<BLRepository<Account>>();
            For<IServices<Account>>().Use<AccountService>();
            For<IRepository<AccountLog>>().Use<BLRepository<AccountLog>>();
            For<IServices<AccountLog>>().Use<AccountLogService>();
            For<IRepository<AccountInGroup>>().Use<BLRepository<AccountInGroup>>();
            For<IServices<AccountInGroup>>().Use<AccountInGroupService>();

            For<IRepository<AccountGroup>>().Use<BLRepository<AccountGroup>>();
            For<IServices<AccountGroup>>().Use<AccountGroupService>();

            For<IRepository<AccountGroupFunction>>().Use<BLRepository<AccountGroupFunction>>();
            For<IServices<AccountGroupFunction>>().Use<AccountGroupFunctionService>();

            For<IRepository<Function>>().Use<BLRepository<Function>>();
            For<IServices<Function>>().Use<FunctionService>();

            For<IRepository<WorkGroup>>().Use<BLRepository<WorkGroup>>();
            For<IServices<WorkGroup>>().Use<WorkGroupService>();

            For<IRepository<Systems>>().Use<BLRepository<Systems>>();
            For<IServices<Systems>>().Use<SystemsService>();
             
            // End Account

            For<IRepository<Category>>().Use<BLRepository<Category>>();

            For<IServices<Category>>().Use<CategoryService>();
            For<IRepository<NoticeCategory>>().Use<BLRepository<NoticeCategory>>();
            For<IServices<NoticeCategory>>().Use<NoticeCategoryService>();
            For<IRepository<MediaCategory>>().Use<BLRepository<MediaCategory>>();
            For<IServices<MediaCategory>>().Use<MediaCategoryService>();
            For<IRepository<GallerySlider>>().Use<BLRepository<GallerySlider>>();
            For<IServices<GallerySlider>>().Use<GallerySliderService>();
            For<IRepository<Setting>>().Use<BLRepository<Setting>>();
            For<IServices<Setting>>().Use<SettingService>();
            For<IRepository<Pages>>().Use<BLRepository<Pages>>();
            For<IServices<Pages>>().Use<PagesService>();


            For<IRepository<Menus>>().Use<BLRepository<Menus>>();

            For<IServices<Menus>>().Use<MenusService>();


            For<IRepository<Contact>>().Use<BLRepository<Contact>>();
            For<IServices<Contact>>().Use<ContactService>();
            For<IRepository<Content>>().Use<BLRepository<Content>>();
            For<IServices<Content>>().Use<ContentService>();
          
            For<IRepository<Notice>>().Use<BLRepository<Notice>>();
            For<IServices<Notice>>().Use<NoticeService>();
            For<IRepository<NoticeFileAttachment>>().Use<BLRepository<NoticeFileAttachment>>();
            For<IServices<NoticeFileAttachment >>().Use<NoticeFileAttachmentService>();

            For<IRepository<Department>>().Use<BLRepository<Department>>();
            For<IServices<Department>>().Use<DepartmentService>();
            For<IRepository<Portal>>().Use<BLRepository<Portal>>();
            For<IServices<Portal>>().Use<PortalService>();
            For<IRepository<Media>>().Use<BLRepository<Media>>();
            For<IServices<Media>>().Use<MediaService>();
            For<IRepository<MediaAlbum>>().Use<BLRepository<MediaAlbum>>();
            For<IServices<MediaAlbum>>().Use<MediaAlbumService>();
            For<IRepository<Files>>().Use<BLRepository<Files>>();
            For<IServices<Files>>().Use<FilesService>();
            For<IRepository<Folders>>().Use<BLRepository<Folders>>();
            For<IServices<Folders>>().Use<FoldersService>();
            For<IRepository<Staff>>().Use<BLRepository<Staff>>();
            For<IServices<Staff>>().Use<StaffService>();
            For<IRepository<StaffDepartment>>().Use<BLRepository<StaffDepartment>>();
            For<IServices<StaffDepartment>>().Use<StaffDepartmentService>();

            For<IRepository<AdvBanner>>().Use<BLRepository<AdvBanner>>();
            For<IServices<AdvBanner>>().Use<AdvBannerService>();

            For<IRepository<GalleryCategory>>().Use<BLRepository<GalleryCategory>>();
            For<IServices<GalleryCategory>>().Use<GalleryCategoryService>();

            For<IRepository<Gallery>>().Use<BLRepository<Gallery>>();
            For<IServices<Gallery>>().Use<GalleryService>();

            For<IRepository<Photo>>().Use<BLRepository<Photo>>();
            For<IServices<Photo>>().Use<PhotoService>();

            For<IRepository<DocType>>().Use<BLRepository<DocType>>();
            For<IServices<DocType>>().Use<DocTypeService>();

            For<IRepository<DocCategory>>().Use<BLRepository<DocCategory>>();
            For<IServices<DocCategory>>().Use<DocCategoryService>();

            For<IRepository<DocFileAttachment>>().Use<BLRepository<DocFileAttachment>>();
            For<IServices<DocFileAttachment>>().Use<DocFileAttachmentService>();

            For<IRepository<Document>>().Use<BLRepository<Document>>();
            For<IServices<Document>>().Use<DocumentService>();

            For<IRepository<Video>>().Use<BLRepository<Video>>();
            For<IServices<Video>>().Use<VideoService>();

            For<IRepository<VideoCategory>>().Use<BLRepository<VideoCategory>>();
            For<IServices<VideoCategory>>().Use<VideoCategoryService>();

            For<IRepository<ResourceCategory>>().Use<BLRepository<ResourceCategory>>();
            For<IServices<ResourceCategory>>().Use<ResourceCategoryService>();

            For<IRepository<ResourceFileAttachment>>().Use<BLRepository<ResourceFileAttachment>>();
            For<IServices<ResourceFileAttachment>>().Use<ResourceFileAttachmentService>();

            For<IRepository<Resource>>().Use<BLRepository<Resource>>();
            For<IServices<Resource>>().Use<ResourceService>();

           
        }
        #endregion
    }
}