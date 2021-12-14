using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhotosApp.Controllers
{
    public class PassportController : Controller
    {
        // NOTE: ��������������������� ������������ ����� ������������ �� ���� � DefaultChallengeScheme,
        // � ����� ������������ ���� � ������ ���������������� �� �������� �������� �� returnUrl.
        public IActionResult Login(bool rememberMe, string returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // NOTE: � ������� properties ����� ������ ��������� ��������� ������� ������.
                // �������� �� ��������� ������ ����� ������� ������� ����������� ��� ��������� Challenge.
                var properties = rememberMe
                    ? new AuthenticationProperties
                    {
                        // NOTE: ���� ����� ����������� ��� �������� ��������
                        IsPersistent = true,
                        // NOTE: ���� ����� ����������� 7 �����
                        ExpiresUtc = DateTime.UtcNow.AddDays(7),
                    }
                    : null;

                return Challenge(properties);
            }

            return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
        }

        // NOTE: ����� �� ������� ����� �������������� � ����������� ��������������
        [Authorize]
        public IActionResult Logout()
        {
            return SignOut(new AuthenticationProperties
            {
                RedirectUri = "/"
            }, "Cookie", "Passport");
        }
    }
}