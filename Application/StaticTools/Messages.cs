

using Application.StaticTools;
using System;

namespace DoctorFAM.Application.StaticTools
{
    public static class Messages
{
    //Wellcoming Message
    public static string WellcomingMessage(string userName)
    {
        return
            $"{userName} عزیز خوش آمدید. {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //This is for sample
    public static string GetMessageForSetConsultationDate(string date, string time, string phone)
    {
        return
            $"سلام  {Environment.NewLine}در خواست مشاوره شما برای روز {date + " - ساعت  " + time}.{Environment.NewLine} تنظیم شد. لطفا در تاریخ و زمان مقرر با شماره {phone} تماس بگیرید.{Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Wellcoming For Login 
    public static string SendSMSForLogin()
    {
        return
            $"سلام ،ورود به حساب با موفقیت انجام شده است .{Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send Activation Register Code 
    public static string SendActivationRegisterSms(string activationCode)
    {
        return
            $" سلام . {Environment.NewLine} کد فعال سازی شما : {activationCode} . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //The invoice of your request has been provided by the pharmacy.
    public static string SendSMSForProvideInvoiceFromPharmacy()
    {
        return
            $" سلام . {Environment.NewLine} فاکتور درخواست شما از سمت داروخانه ارائه گردید . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //The invoice of your request has been provided by the pharmacy.
    public static string SendSMSForAccepteDrugRequestFromPharamcy()
    {
        return
            $" سلام . {Environment.NewLine} درخواست سفارش داروی شما از سمت داروخانه قبول شده، لطفا تا ارائه پیش فاکتور صبور باشید. {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Delivery Drug By Courier.
    public static string SendSMSForDeliveryDrugByCourier()
    {
        return
            $" سلام . {Environment.NewLine} بسته سفارشی شما تحویل پیک داده شده و درحال ارسال به شماست. {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Accept Family Doctor Request 
    public static string SendSMSForAcceptFamilyDoctorRequest()
    {
        return
            $" سلام . {Environment.NewLine} درخواست پزشک خانواده شما، توسط پزشک موردنظر تایید شد . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Decline Family Doctor Request 
    public static string SendSMSForDeclineFamilyDoctorRequest()
    {
        return
            $" سلام . {Environment.NewLine} درخواست پزشک خانواده شما، توسط پزشک موردنظر رد شد . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Accepted Request From Nurse 
    public static string SendSMSForAcceptedRequestFromNurse()
    {
        return
            $" سلام . {Environment.NewLine} درخواست شما برای پرستار از سمت پرستار تایید شد. . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Accept Online Visit Request From Doctor  
    public static string SendSMSForAcceptOnlineVisitRequestFromDoctor()
    {
        return
            $" سلام . {Environment.NewLine} درخواست ویزیت آنلاین شما توسط پزشک تایید شد . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Accept Consultant Request 
    public static string SendSMSForAcceptConsultantRequest()
    {
        return
            $" سلام . {Environment.NewLine} درخواست مشاوره شما توسط مشاور، موردنظر تایید شد . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Decline Consultant Request 
    public static string SendSMSForDeclineConsultantRequest()
    {
        return
            $" سلام . {Environment.NewLine} درخواست مشاوره شما توسط مشاور موردنظر رد شد . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Accept Home Visit Request From Doctor  
    public static string SendSMSForAcceptHomeVisitRequestFromDoctor()
    {
        return
            $" سلام . {Environment.NewLine} درخواست ویزیت درمنزل شما توسط پزشک تایید شد . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send Activation Register Code 
    public static string SendSMSForLinkOfHomeVisitRequestFromDoctor(string link)
    {
        return
            $"لطفا از طریق لینک زیر درخواست ویزیت در منزل خود را تایید کنید :{Environment.NewLine} {link} . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Accept Home Visit Request From User   
    public static string SendSMSForAcceptHomeVisitRequestFromUser(int price)
    {
        return
            $"سلام . {Environment.NewLine} اعلام آمادگی شما توسط کاربر تایید و مبلغ - ریال به کیف پول شما در سایت واریز شد. {Environment.NewLine} لطفا در زمان اعلام شده در محل درخواست کاربر حاضر شوید. {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Decline Home Visit Request From User   
    public static string SendSMSForDeclineHomeVisitRequestFromUser()
    {
        return
            $" سلام . {Environment.NewLine} درخواست ویزیت درمنزل همراه با اطلاعات شما توسط کاربر تایید نگردید .   {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Cancelation Home Visit Request From User   
    public static string SendSMSForCancelationHomeVisitRequestFromUser()
    {
        return
            $" سلام . {Environment.NewLine} درخواست ویزیت درمنزل توسط کاربر لغو شد . {Environment.NewLine} از همکاری شما متشکریم. {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Death Certificate Request From Doctor  
    public static string SendSMSForAcceptDeathCertificateRequestFromDoctor()
    {
        return
            $" سلام . {Environment.NewLine} درخواست صدورگواهی فوت شما توسط پزشک تایید شد . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send Show Doctor Details 
    public static string SendSMSForLinkOfDeathCertificateRequestFromDoctor(string link)
    {
        return
            $"شما میتوانید از طریق لینک زیر اطلاعات پزشک را مشاهده فرمایید : {link} . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS From Doctor To The Users That Income From Parsa System
    public static string SendSMSFromDoctorToTheUsersThatIncomeFromParsaSystem(string smsBody)
    {
        return
            $"{smsBody} {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS To The Master Of Population Cover
    public static string SendSMSToTheMasterOfPopulationCover(string nationalId, string mobile)
    {
        return
                 $"فردی از جمعیت تحت پوشش شما با شماره موبایل : {mobile} و کدملی : {nationalId} عضو سایت شده است.{ Environment.NewLine} این کاربر از جمعیت تحت پوشش شما خارج می گردد . {Environment.NewLine} {PathTools.SiteFarsiName}" ;
    }

    //Send SMS For Weekly Usage Of Drug 
    public static string SendSMSForWeeklyUsageOfDrug(string DrugName)
    {
        return
                 $" سلام . داروهای مصرفی امروز شما : {DrugName} . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Priodic Test Alarm 
    public static string SendSMSForPriodicTestAlarm(string priodicTestName)
    {
        return
                 $" سلام . فردا نوبت آزمایش {priodicTestName} شما می باشد. {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS For Medical Examination Alarm 
    public static string SendSMSForMedicalExaminationAlarm(string medicalExaminationName)
    {
        return
                 $" سلام. فردا نوبت معاینه {medicalExaminationName} شما می باشد. {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS To The Doctor For Accept His Send SMS Request 
    public static string SendSMSToTheDoctorForAcceptHisSendSMSRequest(string dateTime)
    {
        return
         $"سلام .{Environment.NewLine} درخواست ارسال پیامک شما برای کاربران سایت تایید و پیامک مورد نظر شما در تاریخ {dateTime} ارسال گردید. {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS To The Doctor For Reject His Send SMS Request 
    public static string SendSMSToTheDoctorForRejectHisSendSMSRequest(string rejectDescription)
    {
        return
         $"سلام .{Environment.NewLine}  درخواست ارسال پیامک شما به دلیل '{rejectDescription}' رد شده است . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Send SMS From Doctor To The Patient 
    public static string SendSMSFromDoctorToThePatient(string textBody)
    {
        return
         $" {textBody} {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //The invoice of your Home LAboratory request has been provided by the Laboratory.
    public static string SendSMSForAccepteHomeLaboratoryRequestFromLaboratory()
    {
        return
            $"سلام . {Environment.NewLine} درخواست آزمایشگاه در منزل شما از سمت آزمایشگاه قبول شده است . لطفا تا ارائه پیش فاکتور صبور باشید. {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Waiting For Confitm Invoice From Patient
    public static string WaitingForConfitmInvoiceFromPatient(string link)
    {
        return
            $" سلام . {Environment.NewLine}  پیش فاکتور درخواست شما ایجاد گردید، " +
            $" لطفا جهت تایید یا رد آن از طریق لینک زیر اقدام فرمایید. " +
            $"{link}" +
            $"{Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Finalize Home Laboratory From User
    public static string FinalizeHomeLaboratoryFromUser()
    {
        return
            $" سلام . {Environment.NewLine}  پیش فاکتور شما از سمت درخواست کننده تایید گردید، لطفا اقدامات مورد نیاز را انجام دهید . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Request For Edit Home Laboratory From User
    public static string RequestFortEditHomeLaboratoryFromUser()
    {
        return
            $" سلام . {Environment.NewLine}  کاربر مورد نظر درخواست بازنگری در پیش فاکتور صادر شده از سمت شما را داده است.  {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Reject Home Laboratory From User
    public static string RejectHomeLaboratoryFromUser()
    {
        return
            $" سلام . {Environment.NewLine}  پیش فاکتور شما از سمت درخواست کننده رد گردیده است  . {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Confirm Online Visit Request From Doctor  
    public static string ConfirmOnlineVisitRequestFromDoctor(string link)
    {
        return
            $"سلام . {Environment.NewLine}" +
            $"درخواست ویزیت آنلاین شما از طرف پزشک تایید شد. {Environment.NewLine}" +
            $"لطفا در زمان تعیین شده از طریق پیام رسان دکترفم با پزشک ذر ارتباط باشید . {Environment.NewLine}" +
            $"لینک پیام رسان : {link}" +
            $"{Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Sending Sampler From Laboratory
    public static string SendingSamplerFromLaboratory()
    {
        return
            $"سلام . {Environment.NewLine}  درخواست آزمایشگاه در منزل شما در مرحله اعزام نمونه گیر است، لطفا در تاریخ و ساعت درخواستی، منتظر باشید. {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Preparing The Order For Home Laboratory Request From Laboratory
    public static string PreparingTheOrderForHomeLaboratoryRequestFromLaboratory()
    {
        return
            $"سلام . {Environment.NewLine}  نمونه توسط آزمایشگاه دریافت شد، لطفا تا اعلام نتیجه توسط آزمایشگاه شکیبا باشید. {Environment.NewLine} {PathTools.SiteFarsiName}";
    }

    //Finalize Home Laboratory Request Result
    public static string FinalizeHomeLaboratoryRequestResult()
    {
        return
            $"سلام . {Environment.NewLine}  نتیجه آزمایش شما آماده و براساس روش انتخابی برای شما ارسال میگردد. {Environment.NewLine} {PathTools.SiteFarsiName}";
    }
}
}

