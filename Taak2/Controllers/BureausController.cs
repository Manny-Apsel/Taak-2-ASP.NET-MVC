using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taak2.Models;
using Evaluatie_2.Business.Interfaces;
using Evaluatie_2.Model;
using QRCoder;
using System.Collections.Generic;
using System.Drawing;
using Evaluatie_2.Models;

namespace Taak2.Controllers
{
    public class BureausController : Controller
    {
        private readonly IBureauLogica bureauLogica;
        private readonly IBureauLocatieLogica locatieLogica;
        private readonly IBureauTypeLogica typeLogica;


        public BureausController(IBureauLogica _bureauLogica, IBureauLocatieLogica _locatieLogica, IBureauTypeLogica _typeLogica)
        {
            bureauLogica = _bureauLogica;
            locatieLogica = _locatieLogica;
            typeLogica = _typeLogica;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var vm = new BureausLijstViewModel();
                vm.Bureaus = await bureauLogica.BureausOphalen();
                var qrCodeGenerator = new QRCodeGenerator();
                vm.Bureaus.ForEach(x =>
                {

                    var qrCodeData = qrCodeGenerator.CreateQrCode($"{x.QrCode}", QRCodeGenerator.ECCLevel.Q);
                    var qrCode = new Base64QRCode(qrCodeData);
                    string qrCodeAfbeeldingInBase64;
                    if (x.Gereserveerd == true)
                    {
                        qrCodeAfbeeldingInBase64 = qrCode.GetGraphic(20, Color.ForestGreen, Color.White, true);
                    }
                    else
                    {
                        qrCodeAfbeeldingInBase64 = qrCode.GetGraphic(20, Color.IndianRed, Color.White, true);
                    }
                    x.QrCode = $"data:image/png;base64,{qrCodeAfbeeldingInBase64}";
                });
                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(Error));
            }
        }

        public async Task<IActionResult> Toevoegen()
        {
            try
            {
                var bureauType = await typeLogica.BureauTypesOphalen();
                var bureauLocatie = await locatieLogica.BureauLocatiesOphalen();
                var vm = new BureauDetailsViewModel
                {
                    Type = bureauType,
                    Locatie = bureauLocatie
                };
                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(Error));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Toevoegen(BureauDetailsViewModel vm)
        {
            try
            {
                if (Validatie(vm) == true)
                {
                    var bureauType = await typeLogica.BureauType1Ophalen(vm.GeselecteerdBureauType);
                    var bureauLocatie = await locatieLogica.BureauLocatie1Ophalen(vm.GeselecteerdBureauLocatie);
                    var bureau = new Bureaus
                    {
                        Omschrijving = vm.Omschrijving,
                        Identificatie = vm.Identificatie,
                        QrCode = vm.QrCode,
                        Gereserveerd = vm.Gereserveerd,
                        Type = bureauType,
                        Locatie = bureauLocatie
                    };
                    await bureauLogica.BureauToevoegen(bureau);
                }
                else
                {
                    var bureauType = await typeLogica.BureauTypesOphalen();
                    var bureauLocatie = await locatieLogica.BureauLocatiesOphalen();
                    vm.Type = bureauType;
                    vm.Locatie = bureauLocatie;
                    return View(vm);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Error));
            }
        }

        public async Task<IActionResult> Wijzigen(int nummer)
        {
            try
            {
                var bureauType = await typeLogica.BureauTypesOphalen();
                var bureauLocatie = await locatieLogica.BureauLocatiesOphalen();
                var bureaus = await bureauLogica.BureauOphalen(nummer);
                var vm = new BureauDetailsViewModel
                {
                    Omschrijving = bureaus.Omschrijving,
                    Identificatie = bureaus.Identificatie,
                    QrCode = bureaus.QrCode,
                    Gereserveerd = bureaus.Gereserveerd,
                    GeselecteerdBureauType = bureaus.Type.Code,
                    GeselecteerdBureauLocatie = bureaus.Locatie.Code,
                    Type = bureauType,
                    Locatie = bureauLocatie
                };
                return View(vm);
            }
            catch
            {
                return RedirectToAction(nameof(Error));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Wijzigen(int nummer, BureauDetailsViewModel vm)
        {
            try
            {
                var bureaus = await bureauLogica.BureauOphalen(nummer);
                if (bureaus != null)
                {
                    if (Validatie(vm) == true)
                    {
                        var bureauType = await typeLogica.BureauType1Ophalen(vm.GeselecteerdBureauType);
                        var bureauLocatie = await locatieLogica.BureauLocatie1Ophalen(vm.GeselecteerdBureauLocatie);
                        if (bureauType != null && bureauLocatie != null)
                        {
                            bureaus.Omschrijving = vm.Omschrijving;
                            bureaus.Identificatie = vm.Identificatie;
                            bureaus.QrCode = vm.QrCode;
                            bureaus.Gereserveerd = vm.Gereserveerd;
                            bureaus.Type = bureauType;
                            bureaus.Locatie = bureauLocatie;
                            await bureauLogica.BureauWijzigen(bureaus);
                        }
                    }
                    else
                    {
                        var bureauType = await typeLogica.BureauTypesOphalen();
                        var bureauLocatie = await locatieLogica.BureauLocatiesOphalen();
                        vm.Type = bureauType;
                        vm.Locatie = bureauLocatie;
                        return View(vm);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Error));
            }
        }

        public async Task<IActionResult> Verwijderen(int nummer)
        {
            try
            {
                var bureau = await bureauLogica.BureauOphalen(nummer);
                if (bureau != null)
                {
                    await bureauLogica.BureauVerwijderen(bureau);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Error));
            }
        }

        public async Task<IActionResult> Error(ErrorViewModel vm)
        {
            return View(vm);
        }

        private bool Validatie(BureauDetailsViewModel vm)
        {
            bool resultaat = true;
            vm.Foutboodschap.Clear();
            if (string.IsNullOrWhiteSpace(vm.Identificatie))
            {
                vm.Foutboodschap.Add("Geef correcte identificatie.");
                resultaat = false;
            }

            if (string.IsNullOrWhiteSpace(vm.Omschrijving))
            {
                vm.Foutboodschap.Add("Geef correcte omschrijving.");
                resultaat = false;
            }            

            if (vm.GeselecteerdBureauLocatie == 0)
            {
                vm.Foutboodschap.Add("Geef correcte bureaulocatie.");
                resultaat = false;
            }

            if (vm.GeselecteerdBureauType == 0)
            {
                vm.Foutboodschap.Add("Geef correcte bureautype.");
            }

            return resultaat;
        }
    }
}