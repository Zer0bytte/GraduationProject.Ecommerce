using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Templates;

public static class EmailTemplates
{
    public static readonly string WelcomeEmail = @"
<!DOCTYPE html>
<html lang=""ar"">
  <head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>Email</title>
  </head>
  <body style=""margin: 0; padding: 0"">
    <div
      style=""
        margin: 0;
        padding: 0;
        width: 100%;
        background-color: #b8b8b8;
        font-family: Arial, sans-serif;
        direction: rtl;
        display: flex;
        justify-items: center;
        justify-content: center;
      ""
    >
      <table
        width=""100%""
        cellpadding=""0""
        cellspacing=""0""
        border=""0""
        style=""
          background-color: #f0f4f8;
          padding: 20px;
          display: flex;
          justify-items: center;
          justify-content: center;
        ""
      >
        <tr>
          <td align=""center"">
            <table
              width=""500""
              cellpadding=""0""
              cellspacing=""0""
              border=""0""
              style=""
                background-color: white;
                border-radius: 8px;
                padding: 40px;
                box-shadow: 0 1px 6px rgb(219, 218, 218);
                text-align: center;
              ""
            >
              <!-- Image -->
              <tr>
                <td align=""center"" style=""padding-bottom: 20px"">
                  <img
                    src=""https://ecommerce.zerobytetools.com/media/6325251.jpg""
                    alt=""Electro Welcome Banner""
                    style=""
                      width: 50%;
                      height: auto;
                      border-radius: 6px;
                      display: block;
                      margin: 0 auto;
                    ""
                  />
                </td>
              </tr>

              <!-- Title -->
              <tr>
                <td align=""center"" style=""padding-bottom: 20px"">
                  <h1 style=""font-size: 24px; font-weight: bold; margin: 0"">
                    الكترو
                  </h1>
                </td>
              </tr>

              <!-- Content -->
              <tr>
                <td
                  align=""center""
                  style=""font-size: 16px; line-height: 1.5; color: #000000""
                >
                  <p style=""margin: 20px 0 0; text-align: center"">
                    مرحبًا {UserName}،<br /><br />
                    شكرًا لتسجيلك في موقع الكترو، المتخصص في بيع الإلكترونيات
                    بأفضل الأسعار والجودة العالية! لإكمال عملية تسجيل حسابك،
                    يرجى إدخال الكود التالي:
                  </p>
                  <!-- Centered Verification Code -->
                  <h2
                    style=""
                      font-size: 32px;
                      font-weight: bold;
                      margin: 20px 0;
                      text-align: center;
                    ""
                  >
                    {VerificationCode}
                  </h2>
                  <p
                    style=""
                      font-size: 14px;
                      color: #b0b0b0;
                      margin: 0;
                      text-align: center;
                    ""
                  >
                    هذا الكود صالح لمدة 15 دقيقة فقط.
                  </p>
                </td>
              </tr>
            </table>
          </td>
        </tr>
      </table>
    </div>
  </body>
</html>";
}
