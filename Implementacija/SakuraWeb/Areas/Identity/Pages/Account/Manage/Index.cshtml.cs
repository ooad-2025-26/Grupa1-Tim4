// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SakuraWeb.Data;

namespace SakuraWeb.Areas.Identity.Pages.Account.Manage;

public class IndexModel : PageModel
{
    private readonly UserManager<Models.Korisnik> _userManager;
    private readonly SignInManager<Models.Korisnik> _signInManager;

    public IndexModel(
        UserManager<Models.Korisnik> userManager,
        SignInManager<Models.Korisnik> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    //public string? Username { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string? StatusMessage { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; } = default!;

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class InputModel
    {
        [Required]
        [Display(Name = "Korisničko ime")]
        public string KorisnickoIme { get; set; } = default!;
        [Display(Name = "Pretplatite se na newsletter")]
        public bool PretplatiteSeNaNewsletter { get; set; }
    }

    private async Task LoadAsync(Models.Korisnik user)
    {
        var userName = await _userManager.GetUserNameAsync(user);
        var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        var pretplacen = user.jePretplacenNaNewsletter;

        Input = new InputModel
        {
            KorisnickoIme = userName,
            PretplatiteSeNaNewsletter = pretplacen
        };
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        await LoadAsync(user);
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
        }

        if (!ModelState.IsValid)
        {
            await LoadAsync(user);
            return Page();
        }



        /*var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        if (Input.PhoneNumber != phoneNumber)
        {
            var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            if (!setPhoneResult.Succeeded)
            {
                StatusMessage = "Unexpected error when trying to set phone number.";
                return RedirectToPage();
            }
        }*/
        if (Input.KorisnickoIme != user.korisnickoIme)
        {
            var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.KorisnickoIme);
            if (!setUserNameResult.Succeeded)
            {
                StatusMessage = "Greška pri ažuriranju korisničkog imena.";
                return RedirectToPage();
            }
        }
        if (Input.PretplatiteSeNaNewsletter != user.jePretplacenNaNewsletter)
        {
            user.promjeniStanjePretplate(Input.PretplatiteSeNaNewsletter);
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                StatusMessage = "Greška pri ažuriranju stanja pretplate na newsletter.";
                return RedirectToPage();
            }
        }

        await _signInManager.RefreshSignInAsync(user);
        StatusMessage = "Vaš profil je uspješno ažuriran.";
        return RedirectToPage();
    }
}
