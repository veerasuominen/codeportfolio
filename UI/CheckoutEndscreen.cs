using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace HexKeyGames
{
    public class CheckoutEndscreen : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text headerTMP, receiptItemsTMP, timeSpentTMP, footerTMP;

        private readonly string[] won = new string[]
        {
            "* YOU DID IT! *",
            "* YOU CHECKED OUT IN TIME! *"
        };
        private readonly string[] failed = new string[]
        {
            "* YOU MESSED UP! *",
            "* YOU MESSED UP! *",
            "* MISSION FAILED SUCCESFULLY! *",
            "* TRY AGAIN! *"
        };
        private readonly string[] thanks = new string[]
        {
            "* THANKS FOR YOUR MONEY! *",
            "* THANKS FOR YOUR MONEY! *",
            "* THANKS FOR SHOPPING! *",
            "* THANKS! NOW GET OUT! *"
        };

        public void PrintReceipt(IEnumerable<ICheckoutable> bought, IEnumerable<ICheckoutable> required)
        {
            var comparer = new CheckoutableEqualityComparer();
            var correct = bought.Intersect(required, comparer);
            var extra = bought.Except(required, comparer);
            var missing = required.Except(bought, comparer);

            static string CheckoutableToString(ICheckoutable item) =>
                item == null ?
                " - DATABASE_ERROR! $4294967296":
                $" - {item.ProductName} {item.ShortDescription} {item.Price}";
            static string CheckoutablesToString(IEnumerable<ICheckoutable> items) =>
                string.Join("\n", items.Select(CheckoutableToString));

            string correctString = correct.Any() ?
                $"You correctly purchased:\n{CheckoutablesToString(correct)}\n\n" :
                "You successfully purchased NOTHING THAT WAS ON YOUR SHOPPING LIST!\n\n";
            string missingString = missing.Any() ?
                $"You forgot to buy:\n{CheckoutablesToString(missing)}\n\n" :
                "You didn't forget anything.\n\n";
            string extraString = extra.Any() ?
                $"You shouldn't have bought:\n{CheckoutablesToString(extra)}\n\n" :
                "You didn't buy anything extra.\n\n";

            receiptItemsTMP.text = correctString + missingString + extraString;
            timeSpentTMP.text = $"Time left: {Timer.Instance.RemainingTimeString.Replace(":", " minutes ")} seconds";
            headerTMP.text = extra.Any() || missing.Any() ? failed.RandomElement() : won.RandomElement();
            footerTMP.text = thanks.RandomElement();
        }
    }
}
