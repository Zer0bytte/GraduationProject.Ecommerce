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

}
