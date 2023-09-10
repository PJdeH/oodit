using Microsoft.AspNetCore.Mvc.RazorPages;
using NumberSelector.Models;
using System.Text.RegularExpressions;

namespace NumberSelector.Pages;

public class IndexModel : PageModel
{
    public Input Values { get; set; }
    public void OnGet()
    {
    }

    public void OnPostSubmit(Input values)
    {
        if (ModelState.IsValid)
        {
            string valueList = values.Values;
            string respons = GetUniqueValues(valueList);
            ViewData["Values"] = respons;
        }
    }

    private static string GetUniqueValues(string valueList)
    {
        string response;
        Regex regex = new Regex(@"^\[(\d*\,)*\d*\]$");

        if (regex.IsMatch(valueList))
        {

            List<int> intList = valueList.Substring(1, valueList.Length - 2).Split(",").ToList().ConvertAll(int.Parse);
            List<TotalNumbers> uniqueList = new List<TotalNumbers>();

            foreach (var number in intList)
            {
                if (uniqueList.Exists(x => x.Value == number))
                {
                    uniqueList.First(x => x.Value == number).Count++;
                }
                else
                {
                    var newNumber = new TotalNumbers()
                    {
                        Value = number,
                        Count = 1
                    };

                    uniqueList.Add(newNumber);
                }
            }

            var uniqueNumbers = uniqueList.Where(x => x.Count > 2).OrderByDescending(x => x.Value);
            response = string.Join(",", uniqueNumbers.Select(n => n.Value.ToString()).ToArray());

            response = "[" + response + "]";
        }
        else
        {
            response = "Please give input into the following format: [x,y,z,..,......] where x, y,z , .. are all integers, example:[1,23,3,3,89]";
        }

        return response;
    }
}
