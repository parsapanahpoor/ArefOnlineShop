using System.IO;

namespace Application.StaticTools
{
    public static class PathTools
    {
        #region Site

        public static string SiteFarsiName = "دکتر فم";
        public static string SiteAddress = "https://doctorfam.com";
        public static string merchant = "300608fa-d6d7-40cc-b70c-7229d28299c6";

        public static readonly string SiteLogo = "/content/images/site/logo/main/";
        public static readonly string SiteLogoServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/site/logo/main/");

        public static readonly string DefaultSiteLogo = "/content/images/site/logo/default/logo.png";
        public static readonly string SiteLogoThumb = "/content/images/site/logo/thumb/";
        public static readonly string SiteLogoThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/site/logo/thumb/");

        public static readonly string EmailBanner = "/content/images/site/emailBanner/main/";
        public static readonly string EmailBannerServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/site/emailBanner/main/");

        public static readonly string EmailBannerThumb = "/content/images/site/emailBanner/thumb/";
        public static readonly string EmailBannerThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/site/emailBanner/thumb/");

        #endregion

        #region Drug Prescription

        public static readonly string DrugPrescriptionPath = "/content/images/DrugPrescription/main/";
        public static readonly string DrugPrescriptionPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/DrugPrescription/main/");

        public static readonly string DrugPrescriptionPathThumb = "/content/images/DrugPrescription/thumb/";
        public static readonly string DrugPrescriptionPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/DrugPrescription/thumb/");

        #endregion

        #region Online Visit Request File

        public static readonly string OnlineVisitRequestFilePath = "/content/images/OnlineVisitRequestFile/main/";
        public static readonly string OnlineVisitRequestFilePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/OnlineVisitRequestFile/main/");

        #endregion

        #region Ticket File

        public static readonly string TicketFilePath = "/content/images/Ticket/main/";
        public static readonly string TicketFilePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Ticket/main/");

        #endregion

        #region UserAvatar

        public static readonly string UserAvatarPath = "/content/images/user/main/";
        public static readonly string UserAvatarPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/user/main/");

        public static readonly string UserAvatarPathThumb = "/content/images/user/thumb/";
        public static readonly string UserAvatarPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/user/thumb/");

        public static readonly string DefaultUserAvatar = "/content/images/user/DefaultAvatar.png";

        #endregion

        #region Ckeditor

        public static readonly string UploadCkEditorImagePath = "/content/upload/ckeditor/images/";
        public static readonly string UploadCkEditorImagePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/upload/ckeditor/images/");

        #endregion

        #region Doctor Information

        public static readonly string DefaultMediacalFile = "/content/images/MediacalFile/DefaultMediacalFile.png";

        public static readonly string MediacalFilePath = "/content/images/MediacalFile/main/";
        public static readonly string MediacalFilePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/MediacalFile/main/");

        public static readonly string MediacalFilePathThumb = "/content/images/MediacalFile/thumb/";
        public static readonly string MediacalFilePathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/MediacalFile/thumb/");

        #endregion

        #region Lab Prescription

        public static readonly string LabPrescriptionPath = "/content/images/LabPrescription/main/";
        public static readonly string LabPrescriptionPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/LabPrescription/main/");

        public static readonly string LabPrescriptionPathThumb = "/content/images/LabPrescription/thumb/";
        public static readonly string LabPrescriptionPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/LabPrescription/thumb/");

        #endregion

        #region Products

        public static readonly string ProductsPath = "/content/images/Products/main/";
        public static readonly string ProductsPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Products/main/");

        public static readonly string ProductsPathThumb = "/content/images/Products/thumb/";
        public static readonly string ProductsPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Products/thumb/");

        public static readonly string DefaultProductAvatar = "/content/images/no-image.png";

        #endregion

        #region Product Gallery

        public static readonly string ProductsGalleryPath = "/content/images/Products/Gallery/main/";
        public static readonly string ProductsGalleryPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Products/Gallery/main/");

        public static readonly string ProductsGalleryPathThumb = "/content/images/Products/Gallery/thumb/";
        public static readonly string ProductsGalleryPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Products/Gallery/thumb/");

        #endregion

        #region Health Information  Attachment Files

        public static readonly string HealthInformationAttachmentFilesServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/HealthInformationAttachmentFiles/Files/");
        //public static readonly string PodcastsForLandingPageFilesServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/HealthInformationAttachmentFiles/PodcastsForLandingPage/");
        public static readonly string HealthInformationAttachmentFilesChunkServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/HealthInformationAttachmentFiles/Chunks/");

        public static readonly string HealthInformationAttachmentFilesPath = "/content/images/HealthInformationAttachmentFiles/Files/";
        //public static readonly string PodcastsForLandingPageFilesPath =  "/content/images/HealthInformationAttachmentFiles/PodcastsForLandingPage/";

        public static readonly string HealthInformationAttachmentFilesImagePath = "/content/images/HealthInformationAttachmentFiles/main/";
        public static readonly string HealthInformationAttachmentFilesImageServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/HealthInformationAttachmentFiles/main/");

        public static readonly string HealthInformationAttachmentFilesImagePathThumb = "/content/images/HealthInformationAttachmentFiles/thumb/";
        public static readonly string HealthInformationAttachmentFilesImagePathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/HealthInformationAttachmentFiles/thumb/");

        #endregion

        #region Honor Image

        public static readonly string HonorPath = "/content/images/Honor/main/";
        public static readonly string HonorPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Honor/main/");

        public static readonly string HonorPathThumb = "/content/images/Honor/thumb/";
        public static readonly string HonorPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Honor/thumb/");

        #endregion

        #region Certificate Image

        public static readonly string CertificatePath = "/content/images/Certificate/main/";
        public static readonly string CertificatePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Certificate/main/");

        public static readonly string CertificatePathThumb = "/content/images/Certificate/thumb/";
        public static readonly string CertificatePathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Certificate/thumb/");

        #endregion

        #region Gallery Image

        public static readonly string ResumeGalleryPath = "/content/images/ResumeGallery/main/";
        public static readonly string ResumeGalleryPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/ResumeGallery/main/");

        public static readonly string ResumeGalleryPathThumb = "/content/images/ResumeGallery/thumb/";
        public static readonly string ResumeGalleryPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/ResumeGallery/thumb/");

        #endregion

        #region Excel File

        public static readonly string RequestExcelFilePath = "/content/images/RequestForUploadExcelFile/";

        public static readonly string RequestExcelFilePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/RequestForUploadExcelFile/");

        #endregion

        #region Customer Advertisement File 

        public static readonly string CustomerAdvertisementPath = "/content/images/CustomerAdvertisement/main/";
        public static readonly string CustomerAdvertisementPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/CustomerAdvertisement/main/");

        public static readonly string CustomerAdvertisementPathThumb = "/content/images/CustomerAdvertisement/thumb/";
        public static readonly string CustomerAdvertisementPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/CustomerAdvertisement/thumb/");

        #endregion

        #region News Image

        public static readonly string NewsPath = "/content/images/News/main/";
        public static readonly string NewsPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/News/main/");

        public static readonly string NewsPathThumb = "/content/images/News/thumb/";
        public static readonly string NewsPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/News/thumb/");

        #endregion

        #region Books Files

        public static readonly string BooksImagePath = "/content/images/Books/main/";
        public static readonly string BooksImagePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Books/main/");

        public static readonly string BooksImagePathThumb = "/content/images/Books/thumb/";
        public static readonly string BooksImagePathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Books/thumb/");

        public static readonly string BookFilePath = "/content/images/Books/Files/";
        public static readonly string BookFilePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Books/Files/");

        public static readonly string BookAttachmentFilesServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Books/Files/");
        public static readonly string BookAttachmentFilesChunkServerPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/Books/Chunks/");

        public static readonly string BookAttachmentFilesPath = "/content/images/Books/Files/";

        #endregion

        #region Diabet Consultant Resume Files

        public static readonly string DiabetConsultantResumeFilesPath = "/content/images/DiabetConsultantResumeFiles/main/";
        public static readonly string DiabetConsultantResumeFilesPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/DiabetConsultantResumeFiles/main/");

        public static readonly string DiabetConsultantResumeFilesPathThumb = "/content/images/DiabetConsultantResumeFiles/thumb/";
        public static readonly string DiabetConsultantResumeFilesPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/DiabetConsultantResumeFiles/thumb/");

        #endregion

        #region Blood Pressure Consultant Resume Files

        public static readonly string BloodPressureConsultantResumeFilesPath = "/content/images/BloodPressureConsultantResumeFiles/main/";
        public static readonly string BloodPressureConsultantResumeFilesPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/BloodPressureConsultantResumeFiles/main/");

        public static readonly string BloodPressureConsultantResumeFilesPathThumb = "/content/images/BloodPressureConsultantResumeFiles/thumb/";
        public static readonly string BloodPressureConsultantResumeFilesPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/BloodPressureConsultantResumeFiles/thumb/");

        #endregion

        #region Status Image 

        public static readonly string StatusImagePath = "/content/images/StatusImage/main/";
        public static readonly string StatusImagePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/StatusImage/main/");

        public static readonly string StatusImagePathThumb = "/content/images/StatusImage/thumb/";
        public static readonly string StatusImagePathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/StatusImage/thumb/");

        #endregion

        #region Chat Images

        public static readonly string ChatImagesPath = "/content/images/user/main/";
        public static readonly string ChatImagesPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/user/main/");

        public static readonly string ChatImagesPathThumb = "/content/images/user/thumb/";
        public static readonly string ChatImagesPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/user/thumb/");

        #endregion

        #region Home LAboratory Invoice

        public static readonly string HomeLaboratoryInvoicePath = "/content/images/HomeLaboratoryInvoice/main/";
        public static readonly string HomeLaboratoryInvoicePathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/HomeLaboratoryInvoice/main/");

        public static readonly string HomeLaboratoryInvoicePathThumb = "/content/images/HomeLaboratoryInvoice/thumb/";
        public static readonly string HomeLaboratoryInvoicePathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/HomeLaboratoryInvoice/thumb/");

        #endregion

        #region Home Laboratory Result

        public static readonly string HomeLaboratoryResultPath = "/content/images/HomeLaboratoryResult/main/";
        public static readonly string HomeLaboratoryResultPathServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/HomeLaboratoryResult/main/");

        public static readonly string HomeLaboratoryResultPathThumb = "/content/images/HomeLaboratoryResult/thumb/";
        public static readonly string HomeLaboratoryResultPathThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/content/images/HomeLaboratoryResult/thumb/");

        #endregion
    }

}

