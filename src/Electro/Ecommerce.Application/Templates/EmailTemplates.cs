using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Templates;

public static class EmailTemplates
{
    public static readonly string EmailConfirmationTemplate = @"
    <!DOCTYPE html>
    <html lang=""ar"" dir=""rtl"">
    <head>
      <meta charset=""UTF-8"">
      <title>تأكيد البريد الإلكتروني</title>
      <style>
        body {
          margin: 0;
          padding: 0;
          font-family: Tahoma, sans-serif;
          background-color: #f4f4f4;
        }
        .container {
          background-color: #ffffff;
          border-radius: 6px;
          padding: 20px;
          width: 600px;
        }
        .code-box {
          background-color: #00BBA7;
          color: #ffffff;
          padding: 15px;
          border-radius: 5px;
          font-size: 24px;
          font-weight: bold;
          letter-spacing: 3px;
          text-align: center;
          direction: ltr;
        }
        .footer {
          font-size: 12px;
          color: #999999;
          padding-top: 30px;
        }
      </style>
    </head>
    <body>
      <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""padding: 20px 0;"">
        <tr>
          <td align=""center"">
            <table class=""container"" cellpadding=""0"" cellspacing=""0"">
              <tr>
                <td align=""center"">
                  <img src=""https://ecommerce.markomedhat.com/media/email-confirmation.png"" alt=""تأكيد البريد الإلكتروني"" width=""300"" style=""max-width:100%; display:block;"">
                </td>
              </tr>
              <tr>
                <td align=""center"" style=""padding-bottom: 20px;"">
                  <h1 style=""margin: 0; font-size: 28px; color: #00BBA7; font-weight: bold;"">إلكـــــــتروو</h1>
                </td>
              </tr>
              <tr>
                <td style=""color: #333333; font-size: 16px; line-height: 1.6; padding: 0 20px;"">
                  <h2 style=""margin: 0 0 15px;"">مرحبًا،</h2>
                  <p style=""margin: 0 0 10px;"">شكرًا لإنشاء حسابك معنا.</p>
                  <p style=""margin: 0 0 10px;"">يرجى استخدام رمز التحقق التالي لتأكيد بريدك الإلكتروني:</p>
                  <div class=""code-box"">{CONFIRMATION_CODE}</div>
                  <p style=""margin: 20px 0 10px;"">إذا لم تطلب هذا التسجيل، يمكنك تجاهل هذا البريد.</p>
                  <p style=""margin: 0;"">مع تحياتنا،<br>فريق الدعم</p>
                </td>
              </tr>
              <tr>
                <td align=""center"" class=""footer"">
                  &copy; 2025 جميع الحقوق محفوظة. لا ترد على هذا البريد الإلكتروني.
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </body>
    </html>";
    public static readonly string SetAdminPasswordEmail = @"
    <!DOCTYPE html>
    <html lang=""ar"" dir=""rtl"">
      <head>
        <meta charset=""UTF-8"">
        <title>إعداد كلمة المرور</title>
      </head>
      <body style=""margin:0; padding:0; font-family: Tahoma, sans-serif; background-color:#f4f4f4;"" dir=""rtl"">
        <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#f4f4f4; padding:20px 0;"">
          <tr>
            <td align=""center"">
              <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background-color:#ffffff; border-radius:6px; padding:20px;"">
                <tr>
                  <td align=""center"">
                    <img src=""https://ecommerce.markomedhat.com/media/admin-set.png"" alt=""إعداد كلمة المرور"" width=""300"" style=""max-width:100%; display:block;"">
                  </td>
                </tr>
                <tr>
                  <td align=""center"" style=""padding-bottom:20px;"">
                    <h1 style=""margin:0; font-size:28px; color:#00BBA7; font-weight:bold;"">إلكـــــــتروو</h1>
                  </td>
                </tr>
                <tr>
                  <td style=""color:#333333; font-size:16px; line-height:1.6; padding:0 20px;"">
                    <h2 style=""margin:0 0 15px;"">مرحبًا،</h2>
                    <p style=""margin:0 0 10px;"">لقد طلبت إعداد كلمة مرور جديدة لحسابك.</p>
                    <p style=""margin:0 0 20px;"">يرجى الضغط على الزر أدناه لإعداد كلمة المرور الخاصة بك:</p>
                    <table cellpadding=""0"" cellspacing=""0"" width=""100%"">
                      <tr>
                        <td align=""center"" style=""padding: 20px 0;"">
                          <a href=""{SET_URL}""
                             style=""background-color:#00BBA7; color:#ffffff; text-decoration:none; padding:12px 24px; border-radius:5px; display:inline-block; font-size:16px;"">
                            إعداد كلمة المرور
                          </a>
                        </td>
                      </tr>
                    </table>
                    <p style=""margin:20px 0 10px;"">إذا لم تطلب هذا التغيير، يمكنك تجاهل هذا البريد.</p>
                    <p style=""margin:0;"">مع تحياتنا،<br>فريق الدعم</p>
                  </td>
                </tr>
                <tr>
                  <td align=""center"" style=""font-size:12px; color:#999999; padding-top:30px;"">
                    &copy; 2025 جميع الحقوق محفوظة. لا ترد على هذا البريد الإلكتروني.
                  </td>
                </tr>
              </table>
            </td>
          </tr>
        </table>
      </body>
    </html>";
    public static readonly string PasswordResetEmailTemplate = @"<!DOCTYPE html>
        <html lang=""ar"" dir=""rtl"">
        <head>
          <meta charset=""UTF-8"">
          <title>إعادة تعيين كلمة المرور</title>
          <style>
            body {
              margin: 0;
              padding: 0;
              font-family: Tahoma, sans-serif;
              background-color: #f4f4f4;
            }
            .container {
              background-color: #ffffff;
              border-radius: 6px;
              padding: 20px;
              width: 600px;
            }
            .code-box {
              background-color: #00BBA7;
              color: #ffffff;
              padding: 15px;
              border-radius: 5px;
              font-size: 24px;
              font-weight: bold;
              letter-spacing: 3px;
              text-align: center;
              direction: ltr;
            }
            .footer {
              font-size: 12px;
              color: #999999;
              padding-top: 30px;
            }
          </style>
        </head>
        <body>
          <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""padding: 20px 0;"">
            <tr>
              <td align=""center"">
                <table class=""container"" cellpadding=""0"" cellspacing=""0"">
                  <tr>
                    <td align=""center"">
                      <img src=""https://ecommerce.markomedhat.com/media/password-reset.png"" alt=""إعادة تعيين كلمة المرور"" width=""300"" style=""max-width:100%; display:block;"">
                    </td>
                  </tr>
                  <tr>
                    <td align=""center"" style=""padding-bottom: 20px;"">
                      <h1 style=""margin: 0; font-size: 28px; color: #00BBA7; font-weight: bold;"">إلكـــــــتروو</h1>
                    </td>
                  </tr>
                  <tr>
                    <td style=""color: #333333; font-size: 16px; line-height: 1.6; padding: 0 20px;"">
                      <h2 style=""margin: 0 0 15px;"">مرحبًا،</h2>
                      <p style=""margin: 0 0 10px;"">تم طلب إعادة تعيين كلمة المرور لحسابك.</p>
                      <p style=""margin: 0 0 10px;"">يرجى استخدام رمز التحقق التالي لإعادة تعيين كلمة المرور:</p>
                      <div class=""code-box"">{RESET_CODE}</div>
                      <p style=""margin: 20px 0 10px;""><strong>ملاحظة:</strong> هذا الرمز صالح لمدة 15 دقيقة فقط.</p>
                      <p style=""margin: 0 0 10px;"">إذا لم تطلب إعادة تعيين كلمة المرور، يمكنك تجاهل هذا البريد وستبقى كلمة المرور الخاصة بك آمنة.</p>
                      <p style=""margin: 0;"">مع تحياتنا،<br>فريق الدعم</p>
                    </td>
                  </tr>
                  <tr>
                    <td align=""center"" class=""footer"">
                      &copy; 2025 جميع الحقوق محفوظة. لا ترد على هذا البريد الإلكتروني.
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </body>
        </html>";
    public const string SupplierVerifiedEmailTemplate = @"<!DOCTYPE html>
        <html lang=""ar"" dir=""rtl"">
        <head>
          <meta charset=""UTF-8"">
          <title>تم التحقق من المورد</title>
          <style>
            body {
              margin: 0;
              padding: 0;
              font-family: Tahoma, sans-serif;
              background-color: #f4f4f4;
            }
            .container {
              background-color: #ffffff;
              border-radius: 6px;
              padding: 20px;
              width: 600px;
            }
            .verified-box {
              background-color: #00BBA7;
              color: #ffffff;
              padding: 15px;
              border-radius: 5px;
              font-size: 20px;
              font-weight: bold;
              text-align: center;
              margin: 20px 0;
            }
            .footer {
              font-size: 12px;
              color: #999999;
              padding-top: 30px;
            }
            .supplier-info {
              background-color: #f8f9fa;
              border-radius: 5px;
              padding: 15px;
              margin: 20px 0;
              border-right: 4px solid #00BBA7;
            }
          </style>
        </head>
        <body>
          <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""padding: 20px 0;"">
            <tr>
              <td align=""center"">
                <table class=""container"" cellpadding=""0"" cellspacing=""0"">
                  <tr>
                    <td align=""center"">
                      <img src=""https://ecommerce.markomedhat.com/media/supplier-verified.png"" alt=""تم التحقق من المورد"" width=""300"" style=""max-width:100%; display:block;"">
                    </td>
                  </tr>
                  <tr>
                    <td align=""center"" style=""padding-bottom: 20px;"">
                      <h1 style=""margin: 0; font-size: 28px; color: #00BBA7; font-weight: bold;"">إلكـــــــتروو</h1>
                    </td>
                  </tr>
                  <tr>
                    <td style=""color: #333333; font-size: 16px; line-height: 1.6; padding: 0 20px;"">
                      <h2 style=""margin: 0 0 15px;"">مرحبًا {SUPPLIER_NAME}،</h2>
                      <p style=""margin: 0 0 10px;"">نود أن نهنئك على إتمام عملية التحقق من حسابك كمورد معتمد في منصة إلكتروو.</p>
              
                      <div class=""verified-box"">
                        تم التحقق من حسابك بنجاح
                      </div>
              
                      <div class=""supplier-info"">
                        <p style=""margin: 0 0 10px;""><strong>معلومات المورد:</strong></p>
                        <p style=""margin: 0 0 5px;"">اسم الشركة: {BUSINESS_NAME}</p>
                        <p style=""margin: 0 0 5px;"">اسم المتجر: {STORE_NAME}</p>
                        <p style=""margin: 0 0 5px;"">تاريخ التحقق: {VERIFICATION_DATE}</p>
                      </div>
              
                      <p style=""margin: 20px 0 10px;""><strong>الخطوات التالية:</strong></p>
                      <p style=""margin: 0 0 10px;"">• يمكنك الآن إضافة منتجاتك وبدء البيع على المنصة</p>
                      <p style=""margin: 0 0 10px;"">• يمكنك الوصول إلى جميع أدوات إدارة المبيعات والمخزون</p>
              
                      <p style=""margin: 20px 0 10px;"">إذا كانت لديك أي أسئلة أو تحتاج إلى مساعدة، لا تتردد في التواصل مع فريق الدعم.</p>
                      <p style=""margin: 0;"">مع تحياتنا،<br>فريق إلكتروو</p>
                    </td>
                  </tr>
                  <tr>
                    <td align=""center"" class=""footer"">
                      &copy; 2025 جميع الحقوق محفوظة. لا ترد على هذا البريد الإلكتروني.
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </body>
        </html>";

    public static readonly string SupplierRejectedEmailTemplate = @"
        <!DOCTYPE html>
        <html lang=""ar"" dir=""rtl"">
        <head>
          <meta charset=""UTF-8"">
          <title>لم يتم التحقق من المورد</title>
          <style>
            body {
              margin: 0;
              padding: 0;
              font-family: Tahoma, sans-serif;
              background-color: #f4f4f4;
            }
            .container {
              background-color: #ffffff;
              border-radius: 6px;
              padding: 20px;
              width: 600px;
            }
            .rejected-box {
              background-color: #e74c3c;
              color: #ffffff;
              padding: 15px;
              border-radius: 5px;
              font-size: 20px;
              font-weight: bold;
              text-align: center;
              margin: 20px 0;
            }
            .footer {
              font-size: 12px;
              color: #999999;
              padding-top: 30px;
            }
            .supplier-info {
              background-color: #f8f9fa;
              border-radius: 5px;
              padding: 15px;
              margin: 20px 0;
              border-right: 4px solid #e74c3c;
            }
          </style>
        </head>
        <body>
          <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""padding: 20px 0;"">
            <tr>
              <td align=""center"">
                <table class=""container"" cellpadding=""0"" cellspacing=""0"">
                  <tr>
                    <td align=""center"">
                      <img src=""https://ecommerce.markomedhat.com/media/supplier-rejected.png"" alt=""لم يتم التحقق من المورد"" width=""300"" style=""max-width:100%; display:block;"">
                    </td>
                  </tr>
                  <tr>
                    <td align=""center"" style=""padding-bottom: 20px;"">
                      <h1 style=""margin: 0; font-size: 28px; color: #e74c3c; font-weight: bold;"">إلكـــــــتروو</h1>
                    </td>
                  </tr>
                  <tr>
                    <td style=""color: #333333; font-size: 16px; line-height: 1.6; padding: 0 20px;"">
                      <h2 style=""margin: 0 0 15px;"">مرحبًا {SUPPLIER_NAME}،</h2>
                      <p style=""margin: 0 0 10px;"">نأسف لإبلاغك بأن عملية التحقق من حسابك كمورد لم تكتمل بنجاح.</p>
              
                      <div class=""rejected-box"">
                        لم يتم التحقق من الحساب
                      </div>

                      <div class=""supplier-info"">
                        <p style=""margin: 0 0 10px;""><strong>معلومات المورد:</strong></p>
                        <p style=""margin: 0 0 5px;"">اسم الشركة: {BUSINESS_NAME}</p>
                        <p style=""margin: 0 0 5px;"">اسم المتجر: {STORE_NAME}</p>
                        <p style=""margin: 0 0 5px;"">تاريخ المراجعة: {REVIEW_DATE}</p>
                      </div>

                      <p style=""margin: 20px 0 10px;""><strong>سبب الرفض (إن وجد):</strong></p>
                      <p style=""margin: 0 0 10px;"">{REJECTION_REASON}</p>

                      <p style=""margin: 20px 0 10px;"">نرجو مراجعة البيانات المقدمة والتأكد من استيفاء جميع المتطلبات، ثم إعادة تقديم طلب التحقق.</p>

                      <p style=""margin: 0 0 10px;"">إذا كنت بحاجة إلى توضيحات إضافية أو دعم، يرجى التواصل مع فريق الدعم الفني.</p>
                      <p style=""margin: 0;"">مع تحياتنا،<br>فريق إلكتروو</p>
                    </td>
                  </tr>
                  <tr>
                    <td align=""center"" class=""footer"">
                      &copy; 2025 جميع الحقوق محفوظة. لا ترد على هذا البريد الإلكتروني.
                    </td>
                  </tr>
                </table>
              </td>
            </tr>
          </table>
        </body>
        </html>";

    public static string OrderItemStatusChangedEmailTemplate = @"
<!DOCTYPE html>
<html lang=""ar"" dir=""rtl"">

<head>
    <meta charset=""UTF-8"">
    <title>تحديث حالة المنتج في طلبك</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: Tahoma, sans-serif;
            background-color: #f4f4f4;
        }

        .container {
            background-color: #ffffff;
            border-radius: 6px;
            padding: 20px;
            width: 600px;
        }

        .status-box {
            background-color: #00BBA7;
            color: #ffffff;
            padding: 15px;
            border-radius: 5px;
            font-size: 18px;
            font-weight: bold;
            text-align: center;
            margin: 20px 0;
        }

        .footer {
            font-size: 12px;
            color: #999999;
            padding-top: 30px;
        }

        .order-info {
            background-color: #f8f9fa;
            border-radius: 5px;
            padding: 15px;
            margin: 20px 0;
            border-right: 4px solid #00BBA7;
        }

        a.button {
            display: inline-block;
            background-color: #00BBA7;
            color: #fff;
            text-decoration: none;
            padding: 10px 20px;
            border-radius: 4px;
            font-weight: bold;
        }
    </style>
</head>

<body>
    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""padding: 20px 0;"">
        <tr>
            <td align=""center"">
                <table class=""container"" cellpadding=""0"" cellspacing=""0"">
                    <tr>
                        <td align=""center"">
                            <img src=""https://ecommerce.markomedhat.com/media/{IMAGE_NAME}""
                                alt=""تحديث حالة الطلب"" width=""300"" style=""max-width:100%; display:block;"">
                        </td>
                    </tr>
                    <tr>
                        <td style=""color: #333333; font-size: 16px; line-height: 1.6; padding: 0 20px;"">
                            <h2 style=""margin: 0 0 15px;"">مرحبًا {CUSTOMER_NAME}،</h2>

                            <div class=""order-info"">
                                <p style=""margin: 0 0 10px;""><strong>تفاصيل المنتج:</strong></p>
                                <p style=""margin: 0 0 5px;"">اسم المنتج: {PRODUCT_NAME}</p>
                                <p style=""margin: 0 0 5px;"">تاريخ التحديث: {DATE}</p>
                            </div>

                            <p style=""margin: 20px 0 10px;"">لمتابعة الطلب أو التحقق من حالة الشحن، اضغط على الرابط
                                أدناه:</p>
                            <p><a href=""{TRACKING_LINK}"" class=""button"">تتبع الطلب</a></p>

                            <p style=""margin: 20px 0 10px;"">نحن هنا دائمًا إذا كنت بحاجة لأي مساعدة.</p>
                            <p style=""margin: 0;"">مع تحياتنا،<br>فريق إلكتروو</p>
                        </td>
                    </tr>
                    <tr>
                        <td align=""center"" class=""footer"">
                            &copy; 2025 جميع الحقوق محفوظة. لا ترد على هذا البريد الإلكتروني.
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>

</html>
";
}
