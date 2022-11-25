using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Constants
{
    public class Mail
    {
        public string ContentMail(string mail)
        {
            string content= $"<div style=\"box-sizing:border-box;width:60%;margin-bottom:30px;background:#ffffff;border:1px solid #f0f0f0; margin: auto;\">\r\n" +
                "        <table style=\"box-sizing:border-box;width:100%;border-spacing:0;border-collapse:separate!important\" width=\"100%\">\r\n" +
                "            <tbody>\r\n" +
                "                <tr>\r\n" +
                "                    <td style=\"box-sizing:border-box;font-family:'Open Sans','Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;font-size:16px;vertical-align:top;padding:30px\" valign=\"top\">\r\n" +
                "                    <table style=\"box-sizing:border-box;width:100%;border-spacing:0;border-collapse:separate!important\" width=\"100%\">\r\n" +
                "                        <tbody>\r\n" +
                "                            <tr>\r\n" +
                "                                <td style=\"box-sizing:border-box;padding:0;font-family:'Open Sans','Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;font-size:16px;vertical-align:top\" valign=\"top\">\r\n" +
                "                                <h2 style=\"margin:0;margin-bottom:30px;font-family:'Open Sans','Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;font-weight:300;line-height:1.5;font-size:24px;color:#294661!important\">Let's verify your single sender so you can start sending email.</h2>\r\n\r\n" +
                "                                <p style=\"margin:0;margin-bottom:30px;color:#294661;font-family:'Open Sans','Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;font-size:16px;font-weight:300\"><strong><a href=\"mailto:{mail}\" target=\"_blank\">{mail}</a></strong></p>\r\n\r\n" +
                "                                <p style=\"margin:0;margin-bottom:30px;color:#294661;font-family:'Open Sans','Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;font-size:16px;font-weight:300\"><small>Your link is active for 48 hours. After that, you will need to resend the verification email.</small></p>\r\n" +
                "                                </td>\r\n" +
                "                            </tr>\r\n" +
                "                            <tr>\r\n" +
                "                                <td style=\"box-sizing:border-box;padding:0;font-family:'Open Sans','Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;font-size:16px;vertical-align:top\" valign=\"top\">\r\n" +
                "                                <table cellpadding=\"0\" cellspacing=\"0\" style=\"box-sizing:border-box;border-spacing:0;width:100%;border-collapse:separate!important\" width=\"100%\">\r\n" +
                "                                    <tbody>\r\n" +
                "                                        <tr>\r\n" +
                "                                            <td align=\"center\" style=\"box-sizing:border-box;padding:0;font-family:'Open Sans','Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;font-size:16px;vertical-align:top;padding-bottom:15px\" valign=\"top\">\r\n" +
                "                                            </td>\r\n" +
                "                                        </tr>\r\n" +
                "                                    </tbody>\r\n" +
                "                                </table>\r\n" +
                "                                </td>\r\n" +
                "                            </tr>\r\n" +
                "                        </tbody>\r\n" +
                "                    </table>\r\n" +
                "                    </td>\r\n" +
                "                </tr>\r\n" +
                "            </tbody>\r\n" +
                "        </table>\r\n" +
                "        </div>";
            return content;
        }
    }
}
