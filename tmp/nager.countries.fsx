#r "nuget: Nager.Country"
#r "nuget: Nager.Country.Translation"

open Nager.Country
open Nager.Country.Translation

let countryProvider = new CountryProvider();
let countries = 
    countryProvider.GetCountries()
    |> Seq.filter (fun c -> c.Region = Region.Europe)

let translationProvider = new TranslationProvider();
let translatedCountryName = translationProvider.GetCountryTranslatedName(Alpha2Code.DE, LanguageCode.KO);
let languages = translationProvider.GetLanguages();

for c in countries do
  printfn " ### %s ### " c.CommonName
  for l in languages do
    let translation = translationProvider.GetCountryTranslatedName(c.Alpha2Code, l.LanguageCode)
    if translation <> null && translation.Length > 0 then
      printf "%s (%A), " translation l.CommonName
  printfn ""
  printfn ""